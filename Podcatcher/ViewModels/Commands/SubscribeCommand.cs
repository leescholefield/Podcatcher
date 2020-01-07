using Podcatcher.Models;
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

        }
    }
}
