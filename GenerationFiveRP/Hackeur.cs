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
    public class Hackeur : Script
    {
        public Hackeur()
        {
            API.onResourceStart += OnStart;
            API.onPlayerConnected += OnPlayerConnect;
        }

        public static bool isRepaireDehors(Client player)
        {
            if (player.position.DistanceTo(new Vector3(882.7369, -1052.517, 33.00666)) < 2)
            {
                return true;
            }
            return false;
        }

        public static bool isRepaireDedans(Client player)
        {
            if (player.position.DistanceTo(new Vector3(1274.192, -1719.929, 54.77146)) < 2)
            {
                return true;
            }
            return false;
        }

        public static bool isRepairePNJ(Client player)
        {
            if (player.position.DistanceTo(new Vector3(1273.306, -1710.928, 54.77145)) < 1)
            {
                return true;
            }
            return false;
        }

        public void OnStart()
        {
            PNJHackeur();
        }

        public void OnPlayerConnect(Client player)
        {
            API.setEntityData(player, "donneesbancaires", false);
            API.setEntityData(player, "OrdiHack", false);
        }

        public void PNJHackeur()
        {
            API.createPed((PedHash)(1346941736), new Vector3(1273.306, -1710.928, 54.77145), -152, 0);
            API.createPed((PedHash)(1561705728), new Vector3(1272.069, -1712.64, 54.77148), 152, 0);
        }
    }
}