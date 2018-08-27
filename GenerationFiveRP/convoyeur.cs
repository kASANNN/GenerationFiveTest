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

namespace GenerationFiveRP
{
    public class convoyeur : Script
    {
        public convoyeur()
        {
            //spawncamionconvoyeur();
            loadconvpoint();
            API.onEntityEnterColShape += pointconv;
        }

        public void ScriptEvent(Client sender, PlayerInfo objplayer, string eventName)
        {
            if (eventName == "ConvoyeurKeyPressed" && !API.isPlayerInAnyVehicle(sender))
            {
                VehiculeInfo objvehicule = VehiculeInfo.GetVehicleArroundPlayer(sender);
                if (objvehicule == null)
                    return; /* ??? c bon sa*/
                if (objplayer.sacbanque == true)
                {
                    var sacs = objvehicule.sacs;
                    if(sacs != 5)
                    {
                        var asacs = sacs + 1;
                        objvehicule.sacs = asacs;
                        objplayer.sacbanque = false;
                        API.setPlayerClothes(sender, 5, 0, 0);
                        if(asacs == 5)
                        {
                            API.sendChatMessageToPlayer(sender, "~g~Le camion est plein ! Tu peux y aller !");
                        }
                    }
                }
                else
                {
                    var sacs = objvehicule.sacs;
                    if(sacs != 0)
                    {
                        objplayer.sacbanque = true;
                        var nsacs = sacs - 1;
                        API.setPlayerClothes(sender, 5, 45, 0);
                        objvehicule.sacs = nsacs;
                        if(nsacs == 0)
                        {
                            API.sendChatMessageToPlayer(sender, "~g~Tu viens de prendre le dernier sac ! Remplis l'ATM puis choisis entre recommencer (se rendre au point indiqué) ou arreter (/stopjob) !");
                            API.triggerClientEvent(sender, "pointconv");
                        }
                    }
                }
            }
            if(eventName == "DepotConvKey" && !API.isPlayerInAnyVehicle(sender) && sender.position.DistanceTo(Constante.Pos_DepotConvoyeur) < 2)
            {
                if (Fonction.IsPlayerInFaction(objplayer, "Gardien", false))
                {
                    if (objplayer.IsJobDuty != true)
                    {
                        if(objplayer.sexe == 0)
                        {
                            int[] draw = { 0, 0, -1, 44, 31, 0, 24, 0, 23, 0, 0, 130 };
                            int[] tex = { 0, 0, -1, 0, 0, 0, 0, 0, 2, 0, 0, 0 };
                            for (int i = 0; i < draw.Length; i++)
                            {
                                API.setPlayerClothes(sender, i, draw[i], tex[i]);
                            }
                            API.setPlayerAccessory(sender, 0, 65, 0);
                        }
                        else
                        {
                            int[] draw = { 0, 0, -1, 49, 32, 0, 25, 0, 2, 0, 0, 127 };
                            int[] tex = { 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                            for (int i = 0; i < draw.Length; i++)
                            {
                                API.setPlayerClothes(sender, i, draw[i], tex[i]);
                            }
                            API.setPlayerAccessory(sender, 0, 64, 0);
                        }
                        objplayer.IsJobDuty = true;
                        objplayer.sacbanque = false;
                        API.sendNotificationToPlayer(sender, "~g~Montes dans un ~r~camion ~g~et rends toi au ~r~point ~g~indiqué.");
                        API.triggerClientEvent(sender, "pointconv");
                    }
                    else
                    {
                        API.sendNotificationToPlayer(sender, "~g~Tu viens de te retirer du service.");
                        Connexion lv = new Connexion();
                        API.clearPlayerAccessory(sender, 0);
                        lv.LoadVetements(sender);
                        lv.LoadAccessoires(sender);
                        objplayer.IsJobDuty = false;
                    }
                }
                else
                {
                    API.sendChatMessageToPlayer(sender, "~r~Tu n'es pas Convoyeur de Fonds");
                }
            }
            if(eventName == "ConvoyeurKeyPressed" && API.isPlayerInAnyVehicle(sender) && objplayer.retourconv == true && sender.position.DistanceTo(new Vector3(490.227, -1402.756, 29.32529)) < 4)
            {
                VehiculeInfo objvehicule = VehiculeInfo.GetVehicleInfoByNetHandle(sender.vehicle);
                if (objvehicule.jobid == Constante.Job_Convoyeur)
                {
                    Client[] jo = API.getVehicleOccupants(objvehicule.handle);
                    foreach (Client value in jo)
                    {
                        API.warpPlayerOutOfVehicle(value);
                        objplayer.retourconv = false;
                    }
                    API.setEntityPosition(objvehicule.handle, objvehicule.pos);
                    API.setEntityRotation(objvehicule.handle, objvehicule.rot);
                    objvehicule.sacs = 0;
                    API.setVehicleEngineStatus(objvehicule.handle, false);
                }
            }
        }

        /*public void spawncamionconvoyeur()
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM CamionsConvoyeurs");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string camposx = String.Format("" + row["PosX"]);
                    string camposy = String.Format("" + row["PosY"]);
                    string camposz = String.Format("" + row["PosZ"]);
                    string camrotx = String.Format("" + row["RotX"]);
                    string camroty = String.Format("" + row["RotY"]);
                    string camrotz = String.Format("" + row["RotZ"]);
                    Vector3 rotcam = new Vector3(float.Parse(camrotx), float.Parse(camroty), float.Parse(camrotz));
                    int camid = int.Parse(String.Format("" + row["ID"]));
                    Vector3 poscam = new Vector3(float.Parse(camposx), float.Parse(camposy), float.Parse(camposz));
                    var idcamion = API.exported.vehicleids.CreateVehicleEx(1747439474, poscam, rotcam, 0, 0, 0);
                    API.setEntityData(idcamion, "poscam", poscam);
                    API.setEntityData(idcamion, "rotcam", rotcam);
                    API.setVehicleEngineStatus(idcamion, false);
                    API.setEntityData(idcamion.handle, "iscamconv", true);
                    API.setEntityData(idcamion.handle, "idcamconv", camid);
                    API.setEntityData(idcamion.handle, "sacs", 0);
                    /*string[] datacampos = { "camposx", "camposy", "camposz", "camrotz", "camroty", "camrotz" };
                    string[] datacampos2 = { camposx, camposy, camposz, camrotz, camroty, camrotz };
                    for(int i = 0; i < datacampos.Length; i++)
                    {
                        API.setEntityData(idcamion, datacampos[i], datacampos2[i]);
                    }

                }
            }
        }*/

        public void loadconvpoint()
        {
            //point depot : 
            Vector3 depotconv2 = new Vector3(490.227, -1402.756, 29.32529);
            var b = API.shared.createBlip(depotconv2);
            API.setBlipTransparency(b, 150);
            API.setBlipColor(b, 3);
            API.shared.setBlipSprite(b, 408);
            API.shared.setBlipShortRange(b, true);

            //point banque
            ColShape colbanque = API.shared.createSphereColShape(Constante.Pos_BanqueConvoyeur, 1);
            colbanque.setData("banqueconv", true);
            
        }

        public void pointconv(ColShape colshape, NetHandle entity)
        {
            var player = API.getPlayerFromHandle(entity);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (colshape.getData("banqueconv") == true)
            {
                if (API.isPlayerInAnyVehicle(API.getPlayerFromHandle(entity)))
                {
                    return;
                }
                else
                {
                    if (objplayer.IsJobDuty == true)
                    {
                        if (objplayer.sacbanque == false)
                        {
                            API.setPlayerClothes(player, 5, 45, 0);
                            player.setData("draw5", 45);
                            player.setData("tx5", 0);
                            API.sendNotificationToPlayer(player, "~g~Charges le sac dans le Camion Blindé ! (Touche 'w')");
                            objplayer.sacbanque = true;
                        }
                    }
                    else
                    {
                        API.sendNotificationToPlayer(player, "~r~Tu n'es pas en service ou tu n'es pas convoyeur !");
                    }
                }
            }
        }

        public NetHandle GetClosestVehicle(Client sender, float distance = 5.0f)
        {
            NetHandle handleReturned = new NetHandle();
            foreach (var veh in API.getAllVehicles())
            {
                Vector3 vehPos = API.getEntityPosition(veh);
                float distanceVehicleToPlayer = sender.position.DistanceTo(vehPos);
                if (distanceVehicleToPlayer < distance)
                {
                    distance = distanceVehicleToPlayer;
                    handleReturned = veh;

                }
            }
            return handleReturned;
        }
    }
}