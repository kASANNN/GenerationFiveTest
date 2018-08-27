using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

namespace GenerationFiveRP
{
    class OnPlayerEnterVehicle : Script
    {
        public OnPlayerEnterVehicle()
        {
            API.onPlayerEnterVehicle += OnPlayerEnterVehicleHandle;
        }

        public void OnPlayerEnterVehicleHandle(Client player, NetHandle vehicle, int targetSeat)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            VehiculeInfo objvehicule = VehiculeInfo.GetVehicleInfoByNetHandle(vehicle);
            if (objvehicule.jobid == Constante.Job_Convoyeur)
            {
                if (!Fonction.IsPlayerInFaction(objplayer, "Gardien", true))
                    return;
            }
            if (objvehicule.EnVente == true)
            {
                API.triggerClientEvent(player, "MenuVehConcess");
            }
        }
    }
}
