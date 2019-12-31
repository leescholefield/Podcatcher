using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Podcatcher.Models.Deserialization.Tests
{
    [TestClass()]
    [DeploymentItem("revolutions.xml")]
    public class XmlConverterTests
    {
        private XmlConverter Converter { get; set; }

        private static string xmlString;

        [ClassInitialize]
        public static void ClassSetup(TestContext _)
        {
            xmlString = File.ReadAllText("revolutions.xml");
        }

        [TestInitialize()]
        public void Setup()
        {
            Converter = new XmlConverter();
        }

        [TestMethod()]
        public void Convert_Deserializes_All_Podcast_Properties()
        {
            var podcast = Converter.Convert(xmlString);

            Assert.AreEqual("Revolutions", podcast.Title);
            Assert.AreEqual("Mike Duncan", podcast.Author);
            Assert.AreEqual("http://static.libsyn.com/p/assets/3/4/5/f/345fbd6a253649c0/RevolutionsLogo_V2.jpg", podcast.ImageUrl);
            Assert.AreEqual("http://revolutionspodcast.libsyn.com/rss/", podcast.FeedUrl);

            // check first episode
            var ep = podcast.Episodes[0];
            Assert.AreEqual("10.27- Coming Together Drifting Apart", ep.Title);
            Assert.AreEqual("Mike Duncan", ep.Author);
            Assert.AreEqual("http://traffic.libsyn.com/revolutionspodcast/10.27-_Coming_Together_Drifting_Apart_Master.mp3?dest-id=159998", ep.StreamUrl);
        }
    }
}