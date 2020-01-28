using Microsoft.VisualStudio.TestTools.UnitTesting;
using Podcatcher.Models.Database;
using System.Data.SQLite;
using System.IO;

namespace PodcatcherTests.Models.Database
{
    /// <summary>
    /// Tests whether the tables defined in <see cref="DatabaseInfo"/> can be successfully created using <see cref="SqlDatabase"/>
    /// </summary>
    [TestClass()]
    public class DatabaseInfoTest
    {

        private static readonly string FILE_LOC = "dbinfo_test";

        private static SqlDatabase Database;
        private static DatabaseInfo Info;

        [ClassInitialize()]
        public static void Setup(TestContext _)
        {
            Info = new DatabaseInfo();
            Database = new SqlDatabase(FILE_LOC, Info);
            Database.CreateTables();
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            File.Delete(FILE_LOC);
        }

        [TestMethod()]
        public void Subscription_Table_Is_Created()
        {
            bool created = false;
            using (var conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;New=False;Compress=True", FILE_LOC)))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'", "subscriptions");
                var result = comm.ExecuteScalar();

                created = result != null;
            }

            Assert.IsTrue(created);
        }

        [TestMethod()]
        public void Unplayed_Table_Is_Created()
        {
            bool created = false;
            using (var conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;New=False;Compress=True", FILE_LOC)))
            using (var comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'", "unplayed");
                var result = comm.ExecuteScalar();

                created = result != null;
            }

            Assert.IsTrue(created);
        }



    }
}
