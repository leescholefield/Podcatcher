using System.Collections.Generic;
using Podcatcher.Models;
using Podcatcher.Models.Database;

namespace Podcatcher.ViewModels.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionDb Db;

        public SubscriptionService(SubscriptionDb db)
        {
            Db = db;
        }

        public List<Podcast> GetSubscriptions()
        {
            return Db.GetSubscriptions();
        }

        public void Subscribe(Podcast podcast)
        {
            Db.Subscribe(podcast);
        }

        public void Unsubscribe(Podcast podcast)
        {
            Db.Delete(podcast);
        }
    }
}
