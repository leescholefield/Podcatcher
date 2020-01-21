using System.Collections.Generic;

namespace Podcatcher.Models
{
    public class Podcast
    {

        public string Title { get; set; }

        public string Author { get; set; }

        public string FeedUrl { get; set; }

        public string ImageUrl { get; set; }

        public List<Episode> Episodes { get; set; } = new List<Episode>();

        public bool Subscribed { get; set; } = false;
    }
}
