using Podcatcher.Models;
using System.Collections.Generic;

namespace Podcatcher.ViewModels.Services
{
    /// <summary>
    /// Deserializes an RSS feed.
    /// </summary>
    public interface IRssDeserializer
    {

        /// <summary>
        /// Deserializes an Rss feed into a <see cref="Podcast"/> instance.
        /// </summary>
        /// <param name="url">Url of the Rss feed to deserialize.</param>
        /// <returns>A deserialized Podcast instance.</returns>
        Podcast Deserialize(string url);

        List<Episode> DeserializeEpisodes(string url);
    }
}
