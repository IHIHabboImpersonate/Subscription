using IHI.Server.Habbos;

namespace IHI.Server.Plugins.Cecer1.Subscriptions
{
    public class SubscriptionCore : Plugin
    {
        public override void Start()
        {
            HabboDistributor.OnHabboLogin += PacketHandlers.RegisterHandlers;
        }
    }
}