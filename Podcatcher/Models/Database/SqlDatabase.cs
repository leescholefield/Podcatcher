using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Podcatcher.Models.Database
{
    /// <summary>
    /// ISSUES: user must place single-quotes around strings or it will throw a SQL exception. Need to find way to do that automatically 
    /// if a string is passed.
    /// </summary>
    public class SqlDatabase : IDatabase
    {

        private string _connString;

        private DatabaseInfo Info { get; set; }

        /// <summary>
        /// Flag to indicate whether we are positive the tables have already been created.
        /// </summary>
        private bool KnowTablesExist = false;

        private bool CreateTablesIfNotExist { get; set; }

        #region Initialization

        public SqlDatabase(string dbPath, DatabaseInfo info) : this(dbPath, info, true) { }

        public SqlDatabase(string dbPath, DatabaseInfo info, bool createIfNotExist)
        {
            CreateTablesIfNotExist = createIfNotExist;
            Info = info;
            _connString = string.Format("Data Source={0};Version=3;New={1};Compress=True", dbPath, createIfNotExist);
        }

        public void CreateTables()
        {
            if (KnowTablesExist)
                return;
            // don't waste time checking if they exist first. Any query will throw an exception.
            if (!CreateTablesIfNotExist)
                return;

            // create tables if not exist
            using (var conn = new SQLiteConnection(_connString))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'", Info.TableNames[0]);
                var name = comm.ExecuteScalar();
                // create tables
                if (name == null)
                {
                    foreach (string t in Info.TableCreationStatements)
                    {
                        comm.CommandText = t;
                        comm.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
            KnowTablesExist = true;
        }

        #endregion

        #region IDatabase Implementation

        public void Insert(string table, Dictionary<string, object> values)
        {
            CreateTables();
            using (var conn = new SQLiteConnection(_connString))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = CreateInsertStatement(table, values);
                comm.ExecuteNonQuery();

                conn.Close();
            }
        }

        private string CreateInsertStatement(string table, Dictionary<string, object> values)
        {
            AddSingleQuotesToStringValues(values);
            
            return string.Format("INSERT INTO {0}({1}) values({2})", table,
                ToCommaSeperatedList(values.Keys), ToCommaSeperatedList(values.Values));
        }

        private void AddSingleQuotesToStringValues(Dictionary<string, object> values)
        {
            var keys = new List<string>(values.Keys);
            foreach (string k in keys)
            {
                var val = values[k];
                if (val.GetType() == typeof(string))
                {
                    values[k] = "'" + val + "'";
                }
            }
        }

        private string ToCommaSeperatedList<T>(IEnumerable<T> enumarable)
        {
            var list = new List<T>(enumarable);
            return string.Join(",", list.ToArray());
        }

        /// <summary>
        /// Searches <paramref name="table"/> for any records matching <paramref name="values"/>. If you pass null for values this will return 
        /// everything in the table.
        /// </summary>
        /// <param name="table">Table to search.</param>
        /// <param name="values">Values to match for. If null this will return everything in the table.</param>
        /// <returns>A List containing a Dictionary of every result found. If no results were found this will be an empty list.</returns>
        public List<Dictionary<string, object>> Search(string table, Dictionary<string, object> values)
        {
            CreateTables();
            var result = new List<Dictionary<string, object>>();
            using (var conn = new SQLiteConnection(_connString))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = (values == null || values.Count == 0) ? 
                                    string.Format("SELECT * FROM {0}", table) :
                                    string.Format("SELECT * FROM {0} WHERE {1}", table, ToEqualsList(values));
                var reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    var dict = Enumerable.Range(0, reader.FieldCount)
                                .ToDictionary(reader.GetName, reader.GetValue);
                    result.Add(dict);
                }

                reader.Close();
            }

            return result;
        }

        private string ToEqualsList(Dictionary<string, object> values)
        {
            AddSingleQuotesToStringValues(values);
            string result = "";
            foreach(KeyValuePair<string, object> pair in values)
            {
                result += pair.Key + " = " + pair.Value;
            }
            return result;
        }

        public Dictionary<string, object> Search(string table, long id)
        {
            CreateTables();
            Dictionary<string, object> result = null;
            using (var conn = new SQLiteConnection(_connString))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = string.Format("SELECT * FROM {0} WHERE id={1}", table, id);
                var reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    result = Enumerable.Range(0, reader.FieldCount)
                                .ToDictionary(reader.GetName, reader.GetValue);
                }

                reader.Close();
            }

            // create empty dict if no results found
            if (result == null)
            {
                result = new Dictionary<string, object>(0);
            }
            return result;
        }

        public void Delete(string table, long id)
        {
            CreateTables();
            using (var conn = new SQLiteConnection(_connString))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = string.Format("DELETE FROM {0} WHERE id={1}", table, id);
                comm.ExecuteNonQuery();
            }
        }

        public void Delete(string table, Dictionary<string, object> values)
        {
            CreateTables();
            using (var conn = new SQLiteConnection(_connString))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = string.Format("DELETE FROM {0} WHERE {1}", table, ToEqualsList(values));
                comm.ExecuteNonQuery();
            }
        }

        public void DeleteBetween(string table, string columnName, object lowerBound, object upperBound)
        {
            CreateTables();
            using (var conn = new SQLiteConnection(_connString))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = string.Format("Delete FROM {0} WHERE {1} BETWEEN {2} AND {3}", table, columnName, lowerBound, upperBound);
                comm.ExecuteNonQuery();
            }
        }

        #endregion

    }
}
