using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Podcatcher.Models.Deserialization.Tests
{
    [TestClass()]
    public class ItunesResultConverterTests
    {
        private static string json;

        private ItunesResultConverter Converter;

        [ClassInitialize()]
        public static void ClassSetup(TestContext _)
        {
            json = File.ReadAllText("itunes.json");
        }

        [TestInitialize()]
        public void Setup()
        {
            Converter = new ItunesResultConverter();
        }

        /// <summary>
        /// IGNORED - having issues getting a copy of itunes.json
        /// </summary>
        [TestMethod()]
        [Ignore()]
        public void Convert_Sets_All_Podcast_Properties()
        {
            var podList = Converter.Convert(json);

            Assert.AreEqual(50, podList.Count);

            var pod = podList[0];

            Assert.AreEqual("HISTORY", pod.Author);
            Assert.AreEqual("Vikings: The Official Podcast", pod.Title);
            Assert.AreEqual("https://cdn-podcasts.video.aetnd.com/vikings-podcast.xml", pod.FeedUrl);

        }
    }
}