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
            var subscriptionName = message.PopPrefixedString();

            var data = new SubscriptionData(sender, subscriptionName);

            // 86400    = 24 hours in seconds.
            // 2678400  = 31 days in seconds.
            var remainingFullMonths = (byte) (data.GetRemainingSeconds()/2678400);
            var expiredFullMonths = (byte) (data.GetExpiredSeconds()/2678400);
            var expiredMonthDays = (byte) ((data.GetExpiredSeconds()%2678400)/86400);

            //Sender.GetPacketSender().Send_SubscriptionInfo(SubscriptionName, ExpiredMonthDays, ExpiredFullMonths, RemainingFullMonths, Data.IsActive());
        }
    }
}