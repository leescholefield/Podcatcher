using Podcatcher.Models;
using System.Collections.Generic;

namespace Podcatcher.ViewModels.Services
{
    public interface IItunesSearchService
    {

        List<Podcast> Search(string term);
    }
}
