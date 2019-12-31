using System.Collections.Generic;
using System.Xml.Serialization;

namespace Podcatcher.Models
{
    [XmlRoot("channel")]
    public class Podcast
    {

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("itunes:author")]
        public string Author { get; set; }

        public string FeedUrl { get; set; }

        [XmlAttribute("image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string ImageUrl { get; set; }

        public List<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
