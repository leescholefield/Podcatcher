using System.Collections.Generic;
using Podcatcher.Models;
using Podcatcher.Models.Deserialization;

namespace Podcatcher.ViewModels.Services
{
    public class RssDeserializer : IRssDeserializer
    {
        private readonly IPodcastDeserializer Deserializer;

        public RssDeserializer(IPodcastDeserializer deserializer)
        {
            Deserializer = deserializer;
        }

        public Podcast Deserialize(string url)
        {
            return Deserializer.Deserialize(url);
        }

        public List<Episode> DeserializeEpisodes(string url)
        {
            return Deserializer.DeserializeEpisodes(url);
        }
    }
}
