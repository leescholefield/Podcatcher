using System;
using System.Collections.Generic;

namespace Podcatcher.Models.Database
{
    public class SubscriptionDb
    {
        #region Properties

        private static readonly string TABLE_NAME = "subscriptions";
        private static readonly DatabaseInfo DbInfo = new DatabaseInfo();

        private IDatabase Database { get; set; }


        #endregion

        #region Initilization

        public SubscriptionDb()
        {
            Database = DefaultDatabase();
        }

        public SubscriptionDb(IDatabase database)
        {
            Database = database;
        }

        private IDatabase DefaultDatabase()
        {
            return new SqlDatabase("subscriptions", DbInfo);
        }

        /// <summary>
        /// Removes all records from <see cref="DatabaseInfo.UnplayedTable"/> that are older than 30 days.
        /// </summary>
        private void PurgeOldUnplayedTable()
        {
            // subtract 30 days from now
            var upper = DateTime.Now.AddDays(-30).Date;
            var lower = DateTime.MinValue.Date;

            string upperF = upper.ToString("yyyy-MM-dd");
            string lowerF = lower.ToString("yyyy-MM-dd");

            Database.DeleteBetween(DatabaseInfo.UnplayedTable.TABLE_NAME, "pub_date", lowerF, upperF);
        }

        #endregion

        /// <summary>
        /// Adds <paramref name="podcast"/> to the database.
        /// </summary>
        /// <exception cref="NullReferenceException">
        ///     If <see cref="Podcast.FeedUrl"/> is null.
        /// </exception>
        public void Subscribe(Podcast podcast)
        {
            var vals = ConvertPodcastPropertiesToDictionary(podcast);
            Database.Insert(TABLE_NAME, vals);
        }

        public List<Podcast> GetSubscriptions()
        {
            var result = Database.Search(TABLE_NAME, null);

            List<Podcast> pods = new List<Podcast>(result.Count);
            foreach(var dict in result)
            {
                pods.Add(ConvertDictionaryToPodcast(dict));
            }

            return pods;
        }

        public List<Episode> GetUnplayedEpisodes()
        {
            PurgeOldUnplayedTable();

            var result = Database.Search(DatabaseInfo.UnplayedTable.TABLE_NAME, null);
            List<Episode> eps = new List<Episode>(result.Count);
            foreach(var dict in result)
            {
                eps.Add(ConvertDictionaryToEpisode(dict));
            }

            return eps;
        }

        private Podcast ConvertDictionaryToPodcast(Dictionary<string, object> dict)
        {
            return new Podcast
            {
                Title = dict["title"].ToString(),
                Author = dict["author"].ToString(),
                FeedUrl = dict["feed_url"].ToString(),
                ImageUrl = dict["image_url"].ToString(),
                Subscribed = true
            };
        }

        private Episode ConvertDictionaryToEpisode(Dictionary<string, object> dict)
        {
            return new Episode
            {
                Title = dict["title"].ToString(),
                Author = dict["author"].ToString(),
                Description = dict["description"].ToString(),
                StreamUrl = dict["stream_url"].ToString()
            };
        }

        private Dictionary<string, object> ConvertPodcastPropertiesToDictionary(Podcast podcast)
        {
            if (podcast.FeedUrl == null)
            {
                throw new NullReferenceException("FeedUrl is null");
            }

            return new Dictionary<string, object>()
            {
                {"feed_url", podcast.FeedUrl},
                {"title", podcast.Title},
                {"author", podcast.Author},
                {"image_url", podcast.ImageUrl}
            };
        }

        public void Delete(Podcast toDelete)
        {
            // just pass feedUrl since that will also be unique
            Database.Delete(TABLE_NAME, new Dictionary<string, object>()
            {
                {"feed_url", toDelete.FeedUrl}
            });
        }

        public void Delete(long id)
        {
            Database.Delete(TABLE_NAME, id);
        }
    }
}
