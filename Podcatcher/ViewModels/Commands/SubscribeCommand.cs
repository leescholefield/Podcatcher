using Podcatcher.Models;
using Podcatcher.ViewModels.Services;
using System;

namespace Podcatcher.ViewModels.Commands
{
    public class SubscribeCommand : RelayCommand<Podcast>
    {

        private static Action<Podcast> Action => Subscribe_Execute;

        private static ISubscriptionService Service;

        public SubscribeCommand() : this(ServiceLocator.Instance.GetService<ISubscriptionService>()) { }

        public SubscribeCommand(ISubscriptionService service) : base(Action)
        {
            Service = service;
        }

        private static void Subscribe_Execute(Podcast podcast)
        {
            // unsubscibe if already subscribed
            if (podcast.Subscribed)
            {
                Service.Unsubscribe(podcast);
            }
            else
            {
                Service.Subscribe(podcast);
            }
        }
    }
}
