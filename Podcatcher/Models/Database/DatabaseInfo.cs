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
                return new string[] { SubscriptionTable.SQL_CREATE, UnplayedTable.SQL_CREATE};
            }
        }

        public virtual string[] TableNames
        {
           get
            {
                return new string[] { SubscriptionTable.TABLE_NAME, UnplayedTable.TABLE_NAME};
            }
        }

        public static class SubscriptionTable
        {
            public static readonly string TABLE_NAME = "subscriptions";
            public static readonly string ID_COLUMN = "id";
            private static readonly string FEED_URL_COLUMN = "feed_url";
            private static readonly string TITLE_COLUMN = "title";
            private static readonly string AUTHOR_COLUMN = "author";
            private static readonly string IMAGE_URL_COLUMN = "image_url";

            public static readonly string SQL_CREATE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME +
                " (" + ID_COLUMN + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                FEED_URL_COLUMN + " TEXT UNIQUE, " +
                TITLE_COLUMN + " TEXT, " + AUTHOR_COLUMN + " TEXT, " + IMAGE_URL_COLUMN + " TEXT)";
        }

        public static class UnplayedTable
        {

            public static readonly string TABLE_NAME = "unplayed";
            private static readonly string ID_COLUMN = "id";
            private static readonly string SUBSCRIPTION_ID_COLUMN = "subscription_id"; // Foreign key.
            private static readonly string TITLE_COLUMN = "title";
            private static readonly string DESCRIPTION_COLUMN = "description";
            private static readonly string STREAM_URL_COLUMN = "stream_url";
            private static readonly string PUB_DATE_COLUMN = "pub_date";

            public static readonly string SQL_CREATE = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1} INTEGER PRIMARY KEY AUTOINCREMENT " +
                ", {2} INTEGER, {3} TEXT, {4} TEXT, {5} TEXT, {6} TEXT, FOREIGN KEY({7}) REFERENCES {8}({9}))",
                TABLE_NAME, ID_COLUMN, SUBSCRIPTION_ID_COLUMN, TITLE_COLUMN, DESCRIPTION_COLUMN, STREAM_URL_COLUMN, PUB_DATE_COLUMN,
                SUBSCRIPTION_ID_COLUMN, SubscriptionTable.TABLE_NAME, SubscriptionTable.ID_COLUMN);
        }
    }
}
