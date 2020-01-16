using Podcatcher.Models.Net;
using Podcatcher.Models.Deserialization;
using System;
using System.Collections.Generic;
using Podcatcher.Models;
using System.Net;

namespace Podcatcher.ViewModels.Services
{
    public class ItunesSearchService : IItunesSearchService
    {

        private NetworkRequest NetRequest;
        private readonly IConverter<List<Podcast>> PodcastConverter;

        private static readonly string BASE_URL = "https://itunes.apple.com/search?media=podcast&term=";

        public ItunesSearchService(NetworkRequest netRequest, IConverter<List<Podcast>> itunesConverter)
        {
            NetRequest = netRequest;
            PodcastConverter = itunesConverter;
        }

        public List<Podcast> Search(string term)
        {
            var url = CreateUrlString(term);
            try
            {
                var contentBody = new NetworkRequest(url).SendRequest();
                return PodcastConverter.Convert(contentBody);
            }
            catch (WebException e)
            {
                throw new Exception("Could not send request", e);
            }
        }

        private string CreateUrlString(string searchTerm)
        {
            var s = searchTerm.Replace(" ", "+");
            return BASE_URL + s;
        }
    }
}
