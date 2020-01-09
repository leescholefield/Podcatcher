using Podcatcher.Models;
using Podcatcher.ViewModels.Services;
using System;

namespace Podcatcher.ViewModels.Commands
{
    public class SubscribeCommand : RelayCommand<Podcast>
    {

        private static Action<Podcast> Action => Subscribe_Execute;

        public SubscribeCommand() : base(Action)
        {
        }

        private static void Subscribe_Execute(Podcast podcast)
        {
            var ser = ServiceLocator.Instance.GetService<ISubscriptionService>();
            ser.Subscribe(podcast);
        }
    }
}
