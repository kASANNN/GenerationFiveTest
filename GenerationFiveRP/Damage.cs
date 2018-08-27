using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

namespace GenerationFiveRP
{
    public class Damage : Script
    {
        public Damage()
        {
            API.onClientEventTrigger += ClientTrigger;
        }
        public void ClientTrigger(Client sender, string eventName, object[] args)
        {
            if (eventName == "dmg")
            {
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(sender);
                objplayer.sante = sender.health;
                objplayer.armure = sender.armor;
            }
        }
    }
}
