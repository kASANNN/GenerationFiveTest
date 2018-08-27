var res_X = API.getScreenResolutionMaintainRatio().Width;
var res_Y = API.getScreenResolutionMaintainRatio().Height;

API.onUpdate.connect(function ()
{
    var player = API.getLocalPlayer();
    var inVeh = API.isPlayerInAnyVehicle(player);

    if (inVeh)
        {
            var veh = API.getPlayerVehicle(player);
            var vel = API.getEntityVelocity(veh);
            var etat = API.getVehicleHealth(veh);
            var essence = API.getEntitySyncedData(veh, "essence");
            var etatmax = API.returnNative("GET_ENTITY_MAX_HEALTH", 0, veh);
            var etatpourcent = Math.floor((etat / etatmax) * 100);
            var speed = Math.sqrt(
                vel.X * vel.X +
                vel.Y * vel.Y +
                vel.Z * vel.Z
                );
            var displaySpeedkmh = Math.round(speed * 3.6);
            API.drawText(`${displaySpeedkmh}`, res_X - 100, res_Y - 200, 1, 255, 255, 255, 255, 4, 2, false, true, 0);
            API.drawText(`Kmh`, res_X - 20, res_Y - 180, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
            API.drawText(`Essence:`, res_X - 70, res_Y - 270, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
            API.drawText(`${essence}%`, res_X - 20, res_Y - 270, 0.5, 34, 139, 34, 255, 4, 2, false, true, 0);
            API.drawText(`Etat du moteur:`, res_X - 70, res_Y - 225, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
            API.drawText(`${etatpourcent}%`, res_X - 20, res_Y - 225, 0.5, 34, 139, 34, 255, 4, 2, false, true, 0);
            if (etatpourcent < 60)
            {
                API.drawText(`${etatpourcent}%`, res_X - 20, res_Y - 225, 0.5, 219, 122, 46, 255, 4, 2, false, true, 0);
            }
            if (etatpourcent < 30)
            {
                API.drawText(`${etatpourcent}%`, res_X - 20, res_Y - 225, 0.5, 219, 46, 46, 255, 4, 2, false, true, 0);
            }
        }
});