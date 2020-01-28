namespace Podcatcher.Models.Database
{
    /// <summary>
    /// Defines the tables used to create a SQL-based database.
    /// </summary>
    public class DatabaseInfo
    {

        public virtual string[] TableCreationStatements
        {
            get
            {
                return new string[] { SubscriptionDb.SQL_CREATE };
            }
        }

        public virtual string[] TableNames
        {
           get
            {
                return new string[] { SubscriptionDb.TABLE_NAME };
            }
        }

        private static class SubscriptionDb
        {

            public static readonly string SQL_CREATE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + 
                " (" + ID_COLUMN + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                FEED_URL_COLUMN + " TEXT UNIQUE, " + 
                TITLE_COLUMN + " TEXT, " +  AUTHOR_COLUMN + " TEXT, " + IMAGE_URL_COLUMN + " TEXT)";


            public static readonly string TABLE_NAME = "subscriptions";
            private static readonly string ID_COLUMN = "id";
            private static readonly string FEED_URL_COLUMN = "feed_url";
            private static readonly string TITLE_COLUMN = "title";
            private static readonly string AUTHOR_COLUMN = "author";
            private static readonly string IMAGE_URL_COLUMN = "image_url";
        }
    }
}
