namespace IHI.Server.Plugins.Cecer1.Subscriptions
{
    [CompatibilityLock(36)]
    public class SubscriptionCore : Plugin
    {
        public override void Start()
        {
            CoreManager.ServerCore.GetHabboDistributor().OnHabboLogin += PacketHandlers.RegisterHandlers;
        }
    }
}