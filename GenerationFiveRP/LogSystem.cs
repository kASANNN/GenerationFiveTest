using System;
using System.Data;
using System.Collections.Generic;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Text;

namespace GenerationFiveRP
{
    public class LogSystem : Script
    {
        public LogSystem()
        {
            API.onChatCommand += OnChatCommandHandler;
            API.onChatMessage += OnChatMessageHandler;
        }

        public void OnChatCommandHandler(Client player, string command, CancelEventArgs e)
        {
            API.exported.database.executeQuery("INSERT INTO LogTextJoueur VALUE ('', 'Commande', '" + player.name + "', '" + command + "')");
        }

        public void OnChatMessageHandler(Client player, string message, CancelEventArgs e)
        {
            API.exported.database.executeQuery("INSERT INTO LogTextJoueur VALUE ('', 'Message', '" + player.name + "', '" + message + "')");
        }
    }
}