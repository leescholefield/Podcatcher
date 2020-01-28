using Podcatcher.Models;
using System.Collections.Generic;

namespace Podcatcher.ViewModels.Services
{
    /// <summary>
    /// Service that allows you to add a <see cref="Podcast"/> to your subscriptions, as well as retrieve a list of subscribed Podcasts
    /// </summary>
    public interface ISubscriptionService
    {

        /// <summary>
        /// Adds <paramref name="podcast"/> to the user's subcriptions.
        /// </summary>
        void Subscribe(Podcast podcast);

        /// <summary>
        /// Removes <paramref name="podcast"/> from the user's subscriptions. If it is not saved in the subscriptions this will do nothing.
        /// </summary>
        void Unsubscribe(Podcast podcast);

        /// <summary>
        /// Returns a list of the user's subscribed <see cref="Podcast"/>s. If there is no subribed podcasts this will return an empty list.
        /// </summary>
        /// <returns></returns>
        List<Podcast> GetSubscriptions();

        /// <summary>
        /// Returns a list of unplayed episodes from the users subscriptions. 
        /// </summary>
        /// <param name="max">Maximum number of Episodes to return.</param>
        /// <returns>A list of unplayed Episodes, or an empty list if there are no unplayed episodes.</returns>
        List<Episode> GetLatestEpisodes(int max);
    }
}
