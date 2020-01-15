namespace Podcatcher.Models.Database
{
    /// <summary>
    /// Defines the tables used to create a SQL-based database.
    /// </summary>
    public class DatabaseInfo
    {

        public string[] TableCreationStatements
        {
            get; set;
        }

        public string[] TableNames
        {
            get; set;
        }
    }
}
