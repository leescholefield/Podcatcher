using Podcatcher.Models.Database;
using Podcatcher.ViewModels.Services;
using System;

namespace Podcatcher.ViewModels.Commands
{
    public class ViewSubscriptionsCommand : RelayCommand<object>
    {

        public static Action<object> Action { get; set; } = ActionImp;

        public static SubscriptionDb Database { get; set; }

        public ViewSubscriptionsCommand() : base(Action)
        {

        }

        private static void ActionImp(object _)
        {
            var subs = Database.GetSubscriptions();
            ServiceLocator.Instance.GetService<INavigationService>().NavigateTo<PodcastListViewModel>(subs);
        }

    }
}
