using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Podcatcher.Models.Deserialization
{
    /// <summary>
    /// Converts an xml string into a <see cref="Podcast"/> instance.
    /// </summary>
    public class XmlConverter : IConverter<Podcast>
    {

        public Podcast Convert(string xml)
        {
            if (xml == "")
            {
                return null;
            }

            return ParseFeed(xml);
        }

        public List<Episode> ConvertEpisodes(string xml)
        {
            if (xml == "")
                return null;

            return ParseEpisodes(xml);
        }

        private Podcast ParseFeed(string xml)
        {
            XNamespace itunesNs = "http://www.itunes.com/dtds/podcast-1.0.dtd";
            XNamespace atomNs = "http://www.w3.org/2005/Atom";

            var doc = XDocument.Parse(xml);
            var channelRoot = doc.Element("rss").Element("channel");
            var feed = channelRoot.Element(atomNs + "link").Attribute("href").Value;
            var title = channelRoot.Element("title").Value;
            var author = channelRoot.Element(itunesNs + "author").Value;
            var imageUrl = channelRoot.Element("image").Element("url").Value;
            var episodes = ParseEpisodes(channelRoot);

            return new Podcast
            {
                Title = title,
                Author = author,
                ImageUrl = imageUrl,
                FeedUrl = feed,
                Episodes = episodes
            };
        }

        private List<Episode> ParseEpisodes(string xml)
        {
            var doc = XDocument.Parse(xml);
            var channelRoot = doc.Element("rss").Element("channel");

            return ParseEpisodes(channelRoot);
        }

        private List<Episode> ParseEpisodes(XElement root)
        {
            XNamespace itunesNs = "http://www.itunes.com/dtds/podcast-1.0.dtd";
            var author = root.Element(itunesNs + "author").Value;

            var episodes = (from s in root.Descendants("item")
                            select new Episode()
                            {
                                Title = s.Element("title").Value,
                                Author = author,
                                Description = s.Element("description").Value,
                                StreamUrl = s.GetAttributeOfElement("enclosure", "url", nullValue: "")
                            }).ToList();

            return episodes;
        }
    }


}
