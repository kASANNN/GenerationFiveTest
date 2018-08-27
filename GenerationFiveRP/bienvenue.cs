using System;
using System.Collections.Generic;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;


namespace GenerationFiveRP
{
    public class Bienvenue : Script
    {
        public Bienvenue()
        {
            API.onPlayerConnected += OnPlayerConnectedHandler;
            API.onPlayerDisconnected += OnPlayerDisconnectedHandler;
        }

        public void OnPlayerConnectedHandler(Client player)
        {
            if (!Fonction.IsRoleplayName(player.name))
            {
                API.sendChatMessageToPlayer(player, "Ton nom n'est pas au format roleplay (Prenom_Nom)");
                API.kickPlayer(player, "Ton nom n'est pas au format roleplay (Prenom_Nom)");
            }
        }

        public void OnPlayerDisconnectedHandler(Client player, string reason)
        {
            //API.call("SaveWeapons", "Save", player);
            sendCloseMessage(player, 7.5f, "~#ffffff~", player.name + "~h~~w~ a quitté le serveur. (" + reason + ")");
        }

        public void sendCloseMessage(Client player, float radius, string sender, string msg)
        {
            {
                API.sendChatMessageToPlayer(player, sender, msg);
            }
        }
    }
}