using System.Collections.Generic;

namespace Podcatcher.Models.Deserialization
{
    public interface IPodcastDeserializer
    {
        Podcast Deserialize(string url);

        List<Episode> DeserializeEpisodes(string url);
    }
}
