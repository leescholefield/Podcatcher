using Microsoft.VisualStudio.TestTools.UnitTesting;
using Podcatcher.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Models.Database.Tests
{
    [TestClass()]
    public class SqlDatabaseTests
    {

        private static readonly string FILE_LOC = "test.db";

        private TestDbInfo Info { get; set; }

        private SqlDatabase Database { get; set; }

        [TestInitialize()]
        public void SetupTest()
        {
            Info = new  TestDbInfo();
            Database = new SqlDatabase(FILE_LOC, Info, true);
            PerformNonQueryOnDb("CREATE TABLE IF NOT EXISTS testing(id INTEGER PRIMARY KEY AUTOINCREMENT, col1 TEXT)");
        }

        [TestCleanup()]
        public void CleanupTest()
        {
            File.Delete("test.db");
        }

        [TestMethod()]
        public void InsertTest()
        {
            var vals = new Dictionary<string, object>
            {
                {"col1", "test value" }
            };
            Database.Insert("testing", vals);

            var conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;New=False;Compress=True", FILE_LOC));
            var reader = SearchDbFile(conn, "SELECT * FROM testing");
            try
            {
                Assert.IsTrue(reader.HasRows);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        [TestMethod()]
        public void Search_With_Dict_Returns_Correct_Result()
        {
            PerformNonQueryOnDb("INSERT INTO testing(col1) values('first row')");
            PerformNonQueryOnDb("INSERT INTO testing(col1) values('second row')");

            var result = Database.Search("testing", new Dictionary<string, object> {
                { "col1", "first row"}
            });

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod()]
        public void Search_With_null_Dict_Returns_All_Records()
        {
            PerformNonQueryOnDb("INSERT INTO testing(col1) values('first row')");
            PerformNonQueryOnDb("INSERT INTO testing(col1) values('second row')");

            var result = Database.Search("testing", null);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod()]
        public void Search_With_No_Matching_Value_Returns_Empty_List()
        {
            var result = Database.Search("testing", new Dictionary<string, object> {
                { "col1", "first row"}
            });

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void Search_With_Id_Returns_Correct_Result()
        {
            PerformNonQueryOnDb("INSERT INTO testing(id, col1) values(7, 'first row')");
            var result = Database.Search("testing", 7);

            Assert.AreEqual("first row", result["col1"]);
        }

        [TestMethod()]
        public void Search_With_No_Matching_Id_Returns_Empty_Dict()
        {
            var result = Database.Search("testing", 2);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void Delete_With_Id_Deletes_Record()
        {
            PerformNonQueryOnDb("INSERT INTO testing(id, col1) values(7, 'first row')");
            Database.Delete("testing", 7);

            var conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;New=False;Compress=True", FILE_LOC));
            var reader = SearchDbFile(conn, "SELECT * FROM testing");
            try
            {
                Assert.IsFalse(reader.HasRows);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        [TestMethod()]
        public void Delete_With_Values_Deletes_Record()
        {
            PerformNonQueryOnDb("INSERT INTO testing(col1) values('first row')");
            Database.Delete("testing", new Dictionary<string, object>()
            {
                {"col1", "first row" }
            });

            var conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;New=False;Compress=True", FILE_LOC));
            var reader = SearchDbFile(conn, "SELECT * FROM testing");
            try
            {
                Assert.IsFalse(reader.HasRows);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void Delete_With_null_For_Values_Throws_Exception()
        {
            Database.Delete("testing", null);
        }

        private void PerformNonQueryOnDb(string q)
        {
            using (var conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;New=False;Compress=True", FILE_LOC)))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = q;
                comm.ExecuteNonQuery();
            }
        }

        private SQLiteDataReader SearchDbFile(SQLiteConnection conn, string search)
        {
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = search;
                return comm.ExecuteReader();
            }

        }
        
    }

    public class TestDbInfo : DatabaseInfo
    {
        public override string[] TableCreationStatements
        {
            get
            {
                return new string[] { "CREATE TABLE IF NOT EXISTS testing (id INTEGER PRIMARY KEY AUTOINCREMENT, col1 TEXT)" };
            }
        }

        public override string[] TableNames
        {
            get
            {
                return new string[] { "testing" };
            }
        }
    }
}