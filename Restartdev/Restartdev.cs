using System;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;

namespace GenerationFiveRP
{
    public class Restartdev : Script
    {
        [Command("restartgf")]
        public void RestartCommand(Client playerid)
        {
            /*PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(playerid);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(playerid, Constante.PasAdmin);
            }
            else
            {*/
                foreach (Client player in API.getAllPlayers())
                {
                    API.kickPlayer(player);
                }
                int mdp = API.getSetting<int>("password");
                Random rndm = new Random();
                int newmdp = rndm.Next(9999999);
                API.setSetting("password", newmdp);
				API.consoleOutput("Mot de passe temporaire : " + newmdp);
                API.delay(5000, true, () =>
                {
                    string[] ressourcename = API.getRunningResources();
                    foreach (string value in ressourcename)
                    {
						if(value != "Restartdev")
						{
							API.consoleOutput("Ressource restart : " + value);
							API.stopResource(value);
							API.startResource(value);
						}
                    }
					API.delay(5000, true, () =>
					{
						API.setSetting("password", mdp);
						API.consoleOutput("Mot de passe rétablie");
					});
                });
            //}
        }

        [Command("startgf")]
        public void Start(Client sender, string resource)
        {
            /*PlayerInfo objplayer = GenerationFiveRP.PlayerInfo.GetPlayerInfoObject(sender);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(sender, Constante.PasAdmin);
            }
            else
            {*/
                if (!API.doesResourceExist(resource))
                {
                    API.sendChatMessageToPlayer(sender, "~r~No such resource found: \"" + resource + "\"");
                }
                else if (API.isResourceRunning(resource))
                {
                    API.sendChatMessageToPlayer(sender, "~r~Resource \"" + resource + "\" is already running!");
                }
                else
                {
                    API.startResource(resource);
                    API.sendChatMessageToPlayer(sender, "~g~Started resource \"" + resource + "\"");
                }
            //}
        }
    }
}