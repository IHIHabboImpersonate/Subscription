using IHI.Server.Habbos;
using IHI.Server.Libraries.Cecer1.Subscriptions;
using IHI.Server.Networking.Messages;

namespace IHI.Server.Plugins.Cecer1.Subscriptions
{
    internal static class PacketHandlers
    {
        internal static void RegisterHandlers(object source, HabboEventArgs args)
        {
            var target = source as Habbo;
            if (target == null)
                return;
            target.
                GetConnection().
                AddHandler(26, PacketHandlerPriority.DefaultAction, ProcessRequestSubscriptionData);
        }

        private static void ProcessRequestSubscriptionData(Habbo sender, IncomingMessage message)
        {
            string subscriptionName = message.PopPrefixedString();

            var data = new SubscriptionData(sender, subscriptionName);

            new MSubscriptionInfo
                {
                    CurrentDay = (data.GetExpiredSeconds() % 2678400) / 86400,
                    ElapsedMonths = data.GetExpiredSeconds() / 2678400,
                    PrepaidMonths = data.GetRemainingSeconds() / 2678400,
                    IsActive = data.IsActive()
                }.Send(sender);
        }
    }
}