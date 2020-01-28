using Podcatcher.Models;
using System.Collections.Generic;

namespace Podcatcher.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {

        #region Properties

        private List<Episode> unfinishedValue;
        private List<Episode> latestSubscriptionsValue;

        /// <summary>
        /// Episodes that the user has started playing but not finished.
        /// </summary>
        public List<Episode> Unfinished
        {
            get
            {
                return unfinishedValue;
            }
            set
            {
                unfinishedValue = value;
                OnPropertyChanged("Unfinished");
            }
        }

        /// <summary>
        /// The latest Episodes from the users subscriptions that they have not listened to yet.
        /// </summary>
        public List<Episode> LatestSubscriptions
        {
            get
            {
                return latestSubscriptionsValue;
            }
            set
            {
                latestSubscriptionsValue = value;
                OnPropertyChanged("LatestSubscriptions");
            }
        }

        #endregion

    }
}
