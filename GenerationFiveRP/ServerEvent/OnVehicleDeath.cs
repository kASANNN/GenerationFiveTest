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
    class OnVehicleDeath : Script
    {
        public OnVehicleDeath()
        {
            API.onVehicleDeath += OnVehicleDeathHandler;
        }
        private void OnVehicleDeathHandler(NetHandle vehicle)
        {
            VehiculeInfo.Delete(vehicle);
        }
    }
}
