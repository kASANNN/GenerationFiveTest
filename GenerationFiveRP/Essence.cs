using System;
using System.Data;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

namespace GenerationFiveRP
{

    public class Essence : Script
    {
        public Essence()
        {
            essence();
            LoadStationsEssences();
            LoadPompesEssences();
        }
        //event Essence

        public void ScriptEvent(Client sender, PlayerInfo objplayer, string eventName)
        {
            int prixessence = 2;
            if (eventName == "RefuelKeyPressed")
            {
                if (isPumpFuel(sender) && !API.isPlayerInAnyVehicle(sender))
                {
                    VehiculeInfo vehobj = VehiculeInfo.GetVehicleCloserPlayer(sender, 5);
                    PompesEssencesInfo pompeobj = PompesEssencesInfo.GetPompeInfoByPos(sender.position);
                    StationsEssencesInfo stationobj = StationsEssencesInfo.GetStationInfoByIDBDD(pompeobj.IDBDD);
                    if (vehobj == null)
                    {
                        API.sendChatMessageToPlayer(sender, "~r~Pas de véhicule près de toi.");
                        return;
                    }
                    if (vehobj.stopessence != true)
                    {
                        if (stationobj.Stockage <= 0)
                        {
                            API.sendChatMessageToPlayer(sender, "~r~La station essence est en pénurie.");
                            vehobj.stopessence = true;
                            API.delay(5000, true, () =>
                            {
                                vehobj.stopessence = false;
                            });
                            return;
                        }
                        if (vehobj.essence != 100)
                        {
                            API.sendChatMessageToPlayer(sender, "~g~Remplissage en cours ....");
                            VehiculeInfo.SetVehiculeEssence(vehobj, vehobj.essence + 1);
                            objplayer.money = objplayer.money - prixessence;
                            stationobj.Stockage -= 1;
                            API.setTextLabelText(stationobj.textlabel, "Station n°~g~" + stationobj.ID + " ~s~| Stockage :~b~ " + stationobj.Stockage + "~s~L");
                            stationobj.Argents = stationobj.Argents + prixessence;
                            API.triggerClientEvent(sender, "update_money_display", objplayer.money);
                            return;
                        }
                        else
                        {
                            API.sendChatMessageToPlayer(sender, "~r~Le reservoir du vehicule est plein.");
                            vehobj.stopessence = true;
                            API.delay(5000, true, () =>
                            {
                                vehobj.stopessence = false;
                            });
                            return;
                        }
                    }
                }
            }
            if (eventName == "RefuelKeyReleased")
            {
                if (isPumpFuel(sender) && !API.isPlayerInAnyVehicle(sender))
                {
                    API.delay(1000, true, () =>
                    {
                        API.shared.triggerClientEvent(sender, "stop_update_money");
                    });
                    return;
                }
            }
        }

        //liste des pompes a essences

        public static bool isPumpFuel(Client player)
        {
            PompesEssencesInfo pompeobj = PompesEssencesInfo.GetPompeInfoByPos(player.position);
            if (pompeobj == null) return false;
            else return true;

            /*if (player.position.DistanceTo(new Vector3(-71.17614, -1765.23, 29.53241)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-68.22153, -1758.287, 29.39543)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-70.5789, -1757.556, 29.39552)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(-73.19659, -1764.685, 29.3959)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-78.86298, -1762.641, 29.62091)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-76.3435, -1755.595, 29.62621)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-78.57647, -1754.531, 29.79431)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-81.18475, -1761.409, 29.63453)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-325.6121, -1480.877, 30.55037)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-330.8383, -1471.926, 30.5487)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-328.8438, -1471.047, 30.54867)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-323.7588, -1479.728, 30.54871)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-318.304, -1476.775, 30.54871)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-323.1554, -1468.009, 30.54703)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-321.1027, -1466.695, 30.54648)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-316.3491, -1475.443, 30.54871)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-310.8996, -1472.489, 30.54871)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-315.7701, -1463.576, 30.54478)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-314.0054, -1462.371, 30.54419)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(-308.9185, -1471.296, 30.54814)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(168.3866, -1561.152, 29.25944)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(175.1586, -1555.156, 29.2213)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(176.9552, -1556.867, 29.23112)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(170.1949, -1562.966, 29.27028)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(174.0249, -1567.667, 29.29164)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(180.9825, -1561.082, 29.25957)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(182.6538, -1562.53, 29.27095)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(175.5151, -1569.359, 29.29898)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(255.3074, -1268.516, 29.14627)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(255.1659, -1261.417, 29.14534)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(255.2816, -1253.46, 29.17188)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(257.9039, -1253.237, 29.14291)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(257.7348, -1261.418, 29.14291)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(257.8458, -1268.426, 29.14291)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(263.4992, -1268.963, 29.14399)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(263.6506, -1261.594, 29.14289)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(263.7481, -1253.515, 29.14289)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(266.4014, -1253.202, 29.14289)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(266.2807, -1261.437, 29.14289)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(266.3595, -1268.849, 29.14513)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(272.5011, -1268.788, 29.14461)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(272.5864, -1261.317, 29.14289)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(272.5225, -1253.229, 29.14289)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(275.1297, -1253.399, 29.15501)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(275.6634, -1261.046, 29.15994)) < 2)
            {
                return true;
            }
            if (player.position.DistanceTo(new Vector3(275.1314, -1268.241, 29.15605)) < 2)
            {
                return true;
            }
            return false;*/
        }

        public void essence() //Conssomation d'essence -1 toutes les 10sec
        {
            API.delay(10000, false, () =>
            {
                foreach(VehiculeInfo veh in VehiculeInfo.VehiculeList)
                {
                    if (API.getVehicleEngineStatus(veh.handle))
                    {
                        VehiculeInfo.SetVehiculeEssence(veh, veh.essence-1);
                        if (veh.essence <= 0) API.setVehicleEngineStatus(veh.handle, false);
                    }
                }
            });
        }
        public void LoadStationsEssences()
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM StationsEssences");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    StationsEssencesInfo stationobj = new StationsEssencesInfo(Convert.ToInt32(row["ID"]), float.Parse(Convert.ToString(row["PosX"])), float.Parse(Convert.ToString(row["PosY"])), float.Parse(Convert.ToString(row["PosZ"])), Convert.ToInt32(row["Stockage"]), Convert.ToInt32(row["Proprio"]), Convert.ToInt32(row["Argents"]));
                }
            }
        }

        public void LoadPompesEssences()
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM PompesEssences");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    PompesEssencesInfo pompeobj = new PompesEssencesInfo(Convert.ToInt32(row["ID"]), Convert.ToInt32(row["IDBDDStation"]), float.Parse(Convert.ToString(row["PosX"])), float.Parse(Convert.ToString(row["PosY"])), float.Parse(Convert.ToString(row["PosZ"])));
                }
            }
        }
    }
}
