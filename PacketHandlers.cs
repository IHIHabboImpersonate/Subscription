#region GPLv3

// 
// Copyright (C) 2012  Chris Chenery
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

#endregion

#region Usings

using IHI.Server.Habbos;
using IHI.Server.Libraries.Cecer1.Subscriptions;
using IHI.Server.Networking.Messages;

#endregion

namespace IHI.Server.Plugins.Cecer1.Subscriptions
{
    internal static class PacketHandlers
    {
        internal static void RegisterHandlers(object source, HabboEventArgs args)
        {
            Habbo target = source as Habbo;
            if (target == null)
                return;
            target.
                GetConnection().
                AddHandler(26, PacketHandlerPriority.DefaultAction, ProcessRequestSubscriptionData);
        }

        private static void ProcessRequestSubscriptionData(Habbo sender, IncomingMessage message)
        {
            string subscriptionName = message.PopPrefixedString();

            SubscriptionData data = new SubscriptionData(sender, subscriptionName);

            new MSubscriptionInfo
                {
                    CurrentDay = (data.GetExpiredSeconds()%2678400)/86400,
                    ElapsedMonths = data.GetExpiredSeconds()/2678400,
                    PrepaidMonths = data.GetRemainingSeconds()/2678400,
                    IsActive = data.IsActive()
                }.Send(sender);
        }
    }
}