using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Podcatcher.Models.Deserialization
{
    /// <summary>
    /// Converts the JSON returned by Itunes Search Api into a <see cref="Podcast"/> instance.
    /// </summary>
    public class ItunesResultConverter : IConverter<List<Podcast>>
    {
        public List<Podcast> Convert(string contentBody)
        {
            if (contentBody == "")
                return null;
            return ParseJson(contentBody);
        }


        private List<Podcast> ParseJson(string json)
        {
            List<Podcast> result = new List<Podcast>();
            JObject root = JObject.Parse(json);
            var resultArray = (JArray)root["results"];
            foreach (var item in resultArray)
            {
                string title = (string)item["collectionName"];
                string author = (string)item["artistName"];
                string streamUrl = (string)item["feedUrl"];
                string imageUrl = (string)item["artworkUrl60"];

                result.Add(new Podcast
                {
                    Title = title,
                    Author = author,
                    FeedUrl = streamUrl,
                    ImageUrl = imageUrl
                });
            }
            return result;
        }
    }
}
