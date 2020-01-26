using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Podcatcher.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Models.Database.Tests
{
    [TestClass()]
    public class SubscriptionDbTests
    {

        private Mock<IDatabase> MockedDb;
        private SubscriptionDb SubDb;
       
        [TestInitialize()]
        public void TestSetup()
        {
            MockedDb = new Mock<IDatabase>();
            SubDb = new SubscriptionDb(MockedDb.Object);
        }

        [TestMethod()]
        public void Subscribe_Calls_IDatabase_With_Correct_Dictionary()
        {
            var podcast = new Podcast { FeedUrl = "feed", Title = "pod title", Author = "pod author", ImageUrl = "image url"};
            var expected = new Dictionary<string, object>()
            {
                {"feed_url", "feed"},
                {"title", "pod title" },
                {"author", "pod author" },
                {"image_url", "image url" }

            };

            SubDb.Subscribe(podcast);

            MockedDb.Verify(m => m.Insert("subscriptions", expected));
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void Subscribe_Throws_Exception_When_FeedUrl_null()
        {
            SubDb.Subscribe(new Podcast());
        }

        [TestMethod()]
        public void GetSubscriptions_Converts_IDatabase_result_To_Podcast_Instances()
        {
            var returnVal = new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>()
                {
                    {"feed_url", "feed val"},
                    {"title", "pod title"},
                    {"author", "pod author"},
                    {"image_url", "image url" }
                }
            };
            MockedDb.Setup(x => x.Search("subscriptions", null)).Returns(returnVal);

            var result = SubDb.GetSubscriptions();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("feed val", result[0].FeedUrl);
            Assert.AreEqual("pod title", result[0].Title);
            Assert.AreEqual("pod author", result[0].Author);
        }

        [TestMethod()]
        public void Delete_Calls_IDatabase_Delete()
        {
            var mockedExpectedDict = new Dictionary<string, object>()
            {
                {"feed_url", "feed url"}
            };
            SubDb.Delete(new Podcast { FeedUrl = "feed url" });

            MockedDb.Verify(x => x.Delete("subscriptions", mockedExpectedDict));
        }

    }
}