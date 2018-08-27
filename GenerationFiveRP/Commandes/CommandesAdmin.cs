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
using System.Diagnostics;

namespace GenerationFiveRP
{
    public class Commandes : Script
    {
        //Les commandes admins

        /*[Command("creercoffreetitem")]
        public void ACmd_creercoffreetitem(Client player)
        {
            Coffre moneys = new Coffre(Constante.Coffre_Type_Maison);
            ItemInfo.AddItem(Constante.Coffre_Item_Cannabis, moneys, 5);
            ItemInfo.AddItem(Constante.Coffre_Item_Coke, moneys, 2);
            ItemInfo.AddItem(Constante.Coffre_Item_Pelle, moneys, 1);
            API.sendChatMessageToPlayer(player, "Coffre id " + moneys.Id +" créé");
            return;
        }

        [Command("printcoffre", "~r~ADMIN: ~s~/printoffre [id du coffre]")]
        public void Acmd_printcoffre(Client player, int coffreid)
        {
            ItemInfo.PrintCoffreContent(player, coffreid);
            return;
        }*/

        /*[Command("anim")]
        public void ACmd_anim(Client player, String animDict, String animName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.playPlayerAnimation(player, 0, animDict, animName);
            }
        }*/
        [Command("test")]
        public void Test(Client player)
        {
            API.triggerClientEvent(player, "showATM");
        }
        [Command("testsonvehicule")]
        public void Testson(Client player, int IDVeh)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(IDVeh);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.sendNativeToAllPlayers(Hash.START_VEHICLE_HORN, objveh.handle, 25, API.getHashKey("HELDDOWN"), false);
                API.sleep(1000);
                API.sendNativeToAllPlayers(Hash.START_VEHICLE_HORN, objveh.handle, 25, API.getHashKey("HELDDOWN"), false);
            }
        }

        [Command("ReloadRestartDev")]
        public void ReloadRestartDev(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.stopResource("Restartdev");
                API.startResource("Restartdev");
            }
        }

        [Command("debug")]
        public void Debug(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                int joueursbug = 0;
                foreach (Client playerbug in API.getAllPlayers())
                {
                    PlayerInfo objtarget = PlayerInfo.GetPlayerInfoObject(playerbug);
                    if(objtarget == null)
                    {
                        API.kickPlayer(playerbug);
                        joueursbug += 1;
                    }
                }
                API.sendChatMessageToPlayer(player, joueursbug + " joueurs ont été kick.");
            }
        }

        [Command("getvehdbid")]
        public void Getvehdbid(Client player, int ID)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(ID);
                if (objveh == null)
                {
                    API.sendChatMessageToPlayer(player, "~r~Ce véhicule n'éxiste pas.");
                    return;
                }
                else
                {
                    API.sendChatMessageToPlayer(player, String.Format("L'ID bdd du véhicule ~b~{0}~s~, est ~g~{1}~s~.", ID, objveh.dbid));
                    return;
                }
            }
        }

        [Command("getvehid")]
        public void Getvehid(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoByObject(player.vehicle);
                if (objveh == null)
                {
                    API.sendChatMessageToPlayer(player, "~r~Ce véhicule n'éxiste pas.");
                    return;
                }
                else
                {
                    API.sendChatMessageToPlayer(player, String.Format("L'ID du véhicule est ~g~{0}~s~.", objveh.ID));
                    return;
                }
            }
        }

        [Command("setadminlevel")]
        public void Setadminlevel(Client player, string IdOrName, int level)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(IdOrName);
                if(objtarget == null)
                {
                    API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                    return;
                }
                else
                {
                    if(level >= 0 && level <=7)
                    {
                        int oldlevel = objtarget.adminlvl;
                        objtarget.adminlvl = level;
                        API.sendChatMessageToPlayer(objtarget.Handle, "Ton ~g~niveau admin~s~ est passé de ~r~" + oldlevel + "~s~ à ~b~" + level + "~s~.");
                        API.sendChatMessageToPlayer(player, "Le ~g~niveau admin~s~ de ~g~" + Fonction.RemoveUnderscore(objtarget.PlayerName) + " ~s~est passé de ~r~" + oldlevel + "~s~ à ~b~" + level + "~s~.");
                        return;
                    }
                    else
                    {
                        API.sendChatMessageToPlayer(player, "~r~Le niveau admin doit être compris entre 0 et 7.");
                    }
                }
            }
        }

        [Command("addcheckpointautoecole")]
        public void Addcheckpointautoecole(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if(player.isInVehicle)
                {
                    API.exported.database.executeQuery("INSERT INTO CheckpointAutoEcole VALUES ('','" + player.vehicle.position.X + "', '"+ player.vehicle.position.Y +"', '"+ player.vehicle.position.Z +"')");
                    API.sendChatMessageToPlayer(player, "~g~Checkpoint ajouté.");
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu dois être dans un véhicule.");
                }
                
            }
        }

        [Command("resetcheckpointautoecole")]
        public void Resetcheckpointautoecole(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.exported.database.executeQuery("TRUNCATE TABLE CheckpointAutoEcole");
                API.sendChatMessageToPlayer(player, "~r~Tous les Checkpoints de l'Auto-Ecole ont étés supprimés.");
            }
        }

        [Command("addipl")]
        public void Addipl(Client player, String iplName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.requestIpl(iplName);
            }
        }

        [Command("creerstationessence")]
        public void Creerstationessence(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.exported.database.executeQuery("INSERT INTO StationsEssences VALUES ('', '" + player.position.X + "', '" + player.position.Y + "', '" + player.position.Z + "', '10000', '-1', '0')");
                DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM StationsEssences WHERE PosX = '" + player.position.X + "' AND PosY = '" + player.position.Y + "' AND PosZ = '" + player.position.Z + "'");
                if (result.Rows.Count != 0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        StationsEssencesInfo stationobj = new StationsEssencesInfo(Convert.ToInt32(row["ID"]), float.Parse(Convert.ToString(row["PosX"])), float.Parse(Convert.ToString(row["PosY"])), float.Parse(Convert.ToString(row["PosZ"])), Convert.ToInt32(row["Stockage"]), Convert.ToInt32(row["Proprio"]), Convert.ToInt32(row["Argents"]));
                    }
                }
            }
        }

        [Command("creerpompeessence")]
        public void Creerpompeessence(Client player, int Stationid)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                StationsEssencesInfo stationobj = StationsEssencesInfo.GetStationInfoByID(Stationid);
                if (stationobj == null)
                {
                    API.sendChatMessageToPlayer(player, "~r~Cette station n'existe pas.");
                    return;
                }
                else
                {
                    API.exported.database.executeQuery("INSERT INTO PompesEssences VALUES ('', '" + stationobj.IDBDD + "', '" + player.position.X + "', '" + player.position.Y + "', '" + player.position.Z + "')");
                    DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM PompesEssences WHERE PosX = '" + player.position.X + "' AND PosY = '" + player.position.Y + "' AND PosZ = '" + player.position.Z + "'");
                    if (result.Rows.Count != 0)
                    {
                        foreach (DataRow row in result.Rows)
                        {
                            /*PompesEssencesInfo pompeobj = */new PompesEssencesInfo(Convert.ToInt32(row["ID"]), stationobj.IDBDD, player.position.X, player.position.Y, player.position.Z);
                        }
                    }
                }
            }
        }

        [Command("removeipl")]
        public void Removeipl(Client player, String iplName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.removeIpl(iplName);
            }
        }

        [Command("dl", "~r~ADMIN: ~s~/dl true/false")]
        public void Cmd_dl(Client player, bool value)
        {
            if (PlayerInfo.GetPlayerInfoObject(player).adminlvl <= 0)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            VehiculeInfo.ActivateDL(value);
            return;
        }

        [Command("creermaison", "~r~ADMIN: ~s~/creermaison [nom de l'int] [prix]")]
        public void Creermaison(Client player, String app, int prix)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                Vector3 jpos = API.getEntityPosition(player);
                API.exported.database.executeQuery("INSERT INTO Logements VALUES ('', '" + app + "', '" + jpos.X + "', '" + jpos.Y + "', '" + jpos.Z + "', 'Aucun', '" + prix + "', '0', '-1', '-1','0','0')");
                API.delay(1000, true, () =>
                {
                    DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE PosX = '" + jpos.X + "' AND PosY = '" + jpos.Y + "' AND PosZ = '" + jpos.Z + "'");
                    if (result.Rows.Count != 0)
                    {
                        foreach (DataRow row in result.Rows)
                        {
                            Vector3 poslogement = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                            //String textdraw = String.Format("Maison ID : {0}", row["ID"]);
                            //API.createTextLabel(textdraw, poslogement, 25, 1);
                            API.createMarker(1, poslogement, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), 150, 255, 255, 0, 0);
                        }
                    }
                });
            }
        }

        [Command("getessence")]
        public void Getessence(Client player, int IDVehicule)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.sendChatMessageToPlayer(player, String.Format("" + VehiculeInfo.GetVehicleInfoById(IDVehicule).essence));
            }
        }

        [Command("setessence", "~r~ADMIN: ~s~/setessence [IdVehicule] [niveau]")]
        public void Setessence(Client player, int IDVehicule, int niveau)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                VehiculeInfo.SetVehiculeEssence(VehiculeInfo.GetVehicleInfoById(IDVehicule), niveau);
            }
        }

        [Command("ban", "~r~ADMIN: ~s~/ban [Id/PartieDuNom] [Raison]")]
        public void Ban(Client player, String idOrName, String raison)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (objtarget == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                API.exported.database.executeQuery("UPDATE Utilisateur SET IsBan='1' WHERE ID = '" + objtarget.dbid + "'");
                API.kickPlayer(objtarget.Handle, String.Format("Tu viens d'etre ~r~banni ~s~pour raison : ~r~{0}", raison));
                API.sendChatMessageToPlayer(player, "~g~Le joueur a bien été banni.");
            }
        }

        [Command("deban", "~r~ADMIN: ~s~/deban [Nom Social Club]")]
        public void Deban(Client player, String NomSocialClub)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Utilisateur WHERE SCN = '" + NomSocialClub + "' AND IsBan ='1'");
                if (result.Rows.Count != 0)
                {
                    API.shared.exported.database.executeQuery("UPDATE Utilisateur SET IsBan='0' WHERE SCN = '" + NomSocialClub + "'");
                    API.sendChatMessageToPlayer(player, "Le joueur a bien été débanni");
                    return;
                }
                API.sendChatMessageToPlayer(player, "~r~Ce compte n'existe pas ou n'est pas banni");
            }
        }

        [Command("kick", "~r~ADMIN: ~s~/kick [Id/PartieDuNom] [Raison]")]
        public void Kick(Client player, String idOdName, String Raison)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOdName);
                if (objtarget == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                API.kickPlayer(objtarget.Handle, Raison);
                API.sendChatMessageToPlayer(player, "~g~Le joueur a bien été kick.");
            }
        }

        [Command("freeze", "~r~ADMIN: ~s~/freeze [Id/PartieDuNom]", Alias = "3")]
        public void Freeze(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (objtarget == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                if (objtarget.freeze != true)
                {
                    API.freezePlayer(objtarget.Handle, true);
                    objtarget.freeze = true;
                    API.sendChatMessageToPlayer(objtarget.Handle, "Tu viens d'etre ~r~freeze ~s~par un admin.");
                    API.sendChatMessageToPlayer(player, String.Format("Le joueur ~b~{0} ~s~a bien été ~r~freeze~s~.", Fonction.RemoveUnderscore(objtarget.PlayerName)));
                    return;
                }
                else
                {
                    API.freezePlayer(objtarget.Handle, false);
                    objtarget.freeze = false;
                    API.sendChatMessageToPlayer(objtarget.Handle, "Tu viens d'etre ~g~defreeze ~s~par un admin.");
                    API.sendChatMessageToPlayer(player, String.Format("Le Joueur ~b~{0} ~s~a bien été ~g~defreeze~s~.", Fonction.RemoveUnderscore(objtarget.PlayerName)));
                }
            }
        }

        [Command("editvehicule", "~r~ADMIN: ~s~/editvehicule [idvehicule] [option] [valeur]", GreedyArg = true)]
        public void Editvehicule(Client player, int IDVehicule, String option, string valeur)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                VehiculeInfo objvehicule = VehiculeInfo.GetVehicleInfoById(IDVehicule);
                switch (option)
                {
                    case "factionid":
                        objvehicule.factionid = Convert.ToInt32(valeur);
                        API.exported.database.executeQuery("UPDATE Vehicules SET factionid='" + objvehicule.factionid + "' WHERE ID = '" + objvehicule.dbid + "'");
                        break;
                    case "jobid":
                        objvehicule.jobid = Convert.ToInt32(valeur);
                        API.exported.database.executeQuery("UPDATE Vehicules SET jobid='" + objvehicule.jobid + "' WHERE ID = '" + objvehicule.dbid + "'");
                        break;
                    case "proprio":
                        PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(valeur);
                        if (objtarget == null)
                        {
                            API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                            break;
                        }
                        else
                        {
                            objvehicule.IDBDDProprio = objtarget.dbid;
                            API.exported.database.executeQuery("UPDATE Vehicules SET proprio='" + objtarget.dbid + "' WHERE ID = '" + objvehicule.dbid + "'");
                        }
                        break;
                    case "color1":
                        objvehicule.color1 = Convert.ToInt32(valeur);
                        API.setVehiclePrimaryColor(objvehicule.handle, Convert.ToInt32(valeur));
                        API.exported.database.executeQuery("UPDATE Vehicules SET color1='" + objvehicule.color1 + "' WHERE ID = '" + objvehicule.dbid + "'");
                        break;
                    case "color2":
                        objvehicule.color2 = Convert.ToInt32(valeur);
                        API.setVehicleSecondaryColor(objvehicule.handle, Convert.ToInt32(valeur));
                        API.exported.database.executeQuery("UPDATE Vehicules SET color2='" + objvehicule.color2 + "' WHERE ID = '" + objvehicule.dbid + "'");
                        break;
                    case "spawnpos":
                        Vector3 vpos = API.getEntityPosition(objvehicule.handle);
                        Vector3 vrot = API.getEntityRotation(objvehicule.handle);
                        API.exported.database.executeQuery("UPDATE Vehicules SET PosX='" + vpos.X + "',PosY='" + vpos.Y + "',PosZ='" + vpos.Z + "',RotX='" + vrot.X + "',RotY='" + vrot.Y + "',RotZ='" + vrot.Z + "' WHERE ID = '" + objvehicule.dbid + "'");
                        break;
                    case "plaque":
                        objvehicule.plaque = valeur;
                        API.setVehicleNumberPlate(objvehicule.handle, valeur);
                        API.exported.database.executeQuery("UPDATE Vehicules SET Plaque='" + valeur + "' WHERE ID = '" + objvehicule.dbid + "'");
                        break;
                }
            }

        }

        [Command("creervehicule", "~r~ADMIN: ~s~/creervehicule [model] [factionid] [jobid]")]
        public void Creervehicule(Client player, VehicleHash model, int factionid, int jobid)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if (factionid == 0 && jobid == 0)
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu dois obligatoirement mettre un id de job ou de faction.");
                }
                else
                {
                    Vector3 PlayerPos = API.getEntityPosition(player);
                    Vector3 PlayerRot = API.getEntityRotation(player);
                    VehiculeInfo vehicleobj = new VehiculeInfo(model, PlayerPos, PlayerRot, 0, 0, GenerationFiveRP.SpawnVehicule.CreateNewVehiclePlate());
                    API.setVehicleEngineStatus(vehicleobj.handle, true);
                    API.setPlayerIntoVehicle(player, vehicleobj.handle, -1);
                    vehicleobj.factionid = factionid;
                    vehicleobj.jobid = jobid;
                    vehicleobj.essence = 100;
                    API.sendChatMessageToPlayer(player, "~g~N'oublie pas de /savevehicule a la position souhaitée.");
                }
            }
        }

        [Command("savevehicule", "~r~ADMIN: ~s~/savevehicule")]
        public void Savevehicule(Client player)
        {
            VehiculeInfo objvehicule = VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if (!API.isPlayerInAnyVehicle(player))
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu n'es pas dans un vehicule");
                    return;
                }
                else
                {
                    if (objvehicule.factionid == 0 && objvehicule.jobid == 0)
                    {
                        API.sendChatMessageToPlayer(player, "~r~Ce vehicule ne fais pas partie d'une faction ou d'un job");
                    }
                    else
                    {
                        Vector3 vpos = API.getEntityPosition(player.vehicle);
                        Vector3 vrot = API.getEntityRotation(player.vehicle);
                        API.exported.database.executeQuery("INSERT INTO Vehicules VALUES ('','" + objvehicule.factionid + "','" + objvehicule.jobid + "','" + API.getEntityModel(player.vehicle) + "','" + API.getVehiclePrimaryColor(player.vehicle) + "','" + API.getVehicleSecondaryColor(player.vehicle) + "','" + vpos.X + "','" + vpos.Y + "','" + vpos.Z + "','" + vrot.X + "','" + vrot.Y + "','" + vrot.Z + "', '0', '0')");
                    }

                }
            }
        }

        [Command("giveweapon", "~r~ADMIN: ~s~/giveweapon [id/PartieDuNom] [arme] [munitions]")]
        public void GiveWeaponCommand(Client player, String idOrName, WeaponHash weapon, int ammo)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                API.givePlayerWeapon(target.Handle, weapon, ammo, true, true);
            }
        }

        [Command("spawnvehicule", "~r~ADMIN: ~s~/spawnvehicule [ID/NomDuVehicule]")]
        public void SpawnVehicule(Client player, VehicleHash model)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                Vector3 PlayerPos = API.getEntityPosition(player);
                Vector3 PlayerRot = API.getEntityRotation(player);
                VehiculeInfo vehicleobj = new VehiculeInfo(model, PlayerPos, PlayerRot, 0, 0, "Admin", 0);
                vehicleobj.essence = 100;
                API.setVehicleEngineStatus(vehicleobj.handle, true);
                API.setPlayerIntoVehicle(player, vehicleobj.handle, -1);
            }
        }

        [Command("setdimension", "~r~ADMIN: ~s~/setdimension [Id/PartieDuNom] [dimension]")]
        public void Setdimension(Client player, String idOrName, int dimension)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (objtarget == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                API.setEntityDimension(objtarget.Handle, dimension);
                objtarget.dimension = dimension;
            }
        }

        [Command("adonnerlead", "~r~ADMIN: ~s~/adonnerlead [ID/Nom] [Num faction]")]
        public void Donnerleadfaction(Client player, String idOrName, int team)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    target.factionid = team;
                    target.rangfaction = 6;
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été promu leader de la faction ~g~" + team + " ~s~par un admin.");
                }
            }
            return;
        }

        [Command("adonnerentreprise", "~r~ADMIN: ~s~/adonnerentreprise [ID/Nom] [Num entreprise]")]
        public void Donnerleadentreprise(Client player, String idOrName, int entreprise)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    target.entrepriseid = entreprise;
                    target.rangentreprise = 4;
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été promu leader de l'entreprise ~g~" + entreprise + " ~s~par un admin.");
                }
            }
            return;
        }

        [Command("ainvite", "~r~ADMIN: ~s~/ainvite [ID/Nom] [Num faction]")]
        public void Ainvite(Client player, String idOrName, int team)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 4)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    target.factionid = team;
                    target.rangfaction = 1;
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été invité dans la faction ~g~" + team + " ~s~par un admin.");
                }
            }
            return;
        }

        [Command("ainviteentreprise", "~r~ADMIN: ~s~/ainviteentreprise [ID/Nom] [Num entreprise]")]
        public void Ainviteentreprise(Client player, String idOrName, int entreprise)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 4)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    target.entrepriseid = entreprise;
                    target.rangentreprise = 1;
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été invité dans l'entreprise ~g~" + entreprise + " ~s~par un admin.");
                }
            }
            return;
        }

        [Command("ainvitejob", "~r~ADMIN: ~s~/ainvitejob [ID/Nom] [Num job]")]
        public void Ainvitejob(Client player, String idOrName, int job)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 4)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    target.jobid = job;
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été invité au job ~g~" + job + " ~s~par un admin.");
                }
            }
            return;
        }

        [Command("avirer", "~r~ADMIN: ~s~/avirer [IdOuPartieDuNom] [Job/Faction/Entreprise]")]
        public void Avirer(Client player, String idOrName, string option)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.adminlvl < 4)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if (option == "job")
                {
                    target.jobid = 0;
                    API.sendChatMessageToPlayer(player, "Tu viens de virer ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~de son job.");
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été retirer de ton job par un admin.");
                }

                if (option == "faction")
                {
                    target.factionid = 0;
                    target.rangfaction = 0;
                    API.sendChatMessageToPlayer(player, "Tu viens de virer ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~de sa faction.");
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été retirer de ta faction par un admin.");
                }

                if (option == "entreprise")
                {
                    target.entrepriseid = 0;
                    target.rangentreprise = 0;
                    API.sendChatMessageToPlayer(player, "Tu viens de virer ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~de son entreprise.");
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été retirer de ton entreprise par un admin.");
                }
            }
        }

        [Command("adonnerrang", "~r~ADMIN: ~s~/adonnerrang [IdOuPartieDuNom] [Entreprise/Faction] [Rang]")]
        public void Adonnerrang(Client player, String idOrName, string option, int rang)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.adminlvl < 4)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if ((option == "entreprise") & (rang <= 0 || rang >= 5))
                {
                    target.rangentreprise = rang;
                    API.sendChatMessageToPlayer(player, "Tu viens de mettre ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~rang ~g~" + rang + " ~s~de son entreprise.");
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été mis rang ~g~" + rang + " ~s~de ton entreprise par un admin.");
                }

                if ((option == "faction") & (rang <= 0 || rang >= 7))
                {
                    target.rangfaction = rang;
                    API.sendChatMessageToPlayer(player, "Tu viens de virer ~g~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~de sa faction.");
                    API.sendChatMessageToPlayer(target.Handle, "Tu as été mis rang ~g~" + rang + " ~s~de ta faction par un admin.");
                }
            }
        }

        [Command("prendrearmes", "~r~ADMIN: ~s~/prendrearmes [Id/PartieDuNom]")]
        public void PrendreArmes(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 4)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    API.removeAllPlayerWeapons(target.Handle);
                    API.sendChatMessageToPlayer(target.Handle, "~r~Tes armes ont été confisquées par un admin !");
                }
            }
        }

        [Command("vr")]
        public void Vr(Client player, int id = -1)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 4)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if(player.isInVehicle && id == -1)
                {                    
                    player.vehicle.repair();
                    API.sendNotificationToPlayer(player, "~g~Vehicule réparé.");
                }
                else
                {
                    if(id == -1)
                    {
                        API.sendNotificationToPlayer(player, "~r~/vr [id]");
                        return;
                    }
                    API.repairVehicle(VehiculeInfo.GetVehicleById(id));
                    API.sendNotificationToPlayer(player, "~g~Le vehicule: " + id + " a été réparé.");
                }
            }
        }

        [Command("sethp")]
        public void Sethp(Client player, String idOrName, int vie)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 3)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    if (vie >= 101)
                    {
                        API.sendChatMessageToPlayer(player, "Tu ne peux pas donner plus de 100hp.");
                    }
                    else
                    {
                        API.setPlayerHealth(target.Handle, vie);
                        API.sendNotificationToPlayer(target.Handle, "~g~Ta vie a été modifiée");
                    }
                }
            }
            return;
        }

        [Command("setarmor", "~r~ADMIN: ~s~/setarmor [id/PartieDuNom] [Armure]")]
        public void Setarmor(Client player, String idOrName, int armor)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 3)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    if (armor >= 101)
                    {
                        API.sendChatMessageToPlayer(player, "Tu ne peux pas donner plus de 100 de gilet.");
                    }
                    else
                    {
                        API.setPlayerArmor(target.Handle, armor);
                        API.sendNotificationToPlayer(target.Handle, "~g~Ton armure a été modifiée");
                    }
                }
            }
            return;
        }

        [Command("gotopos")]
        public void GoToPos(Client player, float x, float y, float z)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 2)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.setEntityPosition(player.handle, new Vector3(x, y, z));
            }
        }

        [Command("getcar", "~r~ADMIN: ~s~/getcar [IdDuVehicule]")]
        public void Getcar(Client player, int id)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 2)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                {
                    API.setEntityPosition(VehiculeInfo.GetVehicleById(id), player.position);
                    API.setEntityDimension(VehiculeInfo.GetVehicleById(id), player.dimension);
                    API.sendNotificationToPlayer(player, "~g~Le vehicule " + id + " a été téléporté sur ta position.");
                }
            }
        }

        [Command("goto", "~r~ADMIN: ~s~/goto [id/PartieDuNom]")]
        public void Goto(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    API.setEntityPosition(player, target.Handle.position);
                    API.setEntityDimension(player, target.Handle.dimension);
                }
            }
            return;
        }

        [Command("tp", "~r~ADMIN: ~s~/tp [id/PartieDuNom] [id/PartieDuNom]")]
        public void Tp(Client player, String idOrName, String idOrName2)
        {
            if (PlayerInfo.GetPlayerInfoObject(player).adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                PlayerInfo objtarget2 = PlayerInfo.GetPlayerInfotByIdOrName(idOrName2);
                if (objtarget == null || objtarget2 == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    API.setEntityPosition(objtarget.Handle, objtarget2.Handle.position);
                    API.setEntityDimension(objtarget.Handle, objtarget2.Handle.dimension);
                    API.sendNotificationToPlayer(player, String.Format("Tu viens de tp ~g~{0}~s~ à ~g~{1}~s~.", objtarget.PlayerName, objtarget2.PlayerName));
                }
            }
            return;
        }

        [Command("gethere", "~r~ADMIN: ~s~/gethere [id/PartieDuNom]")]
        public void Gethere(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    API.setEntityPosition(target.Handle, player.position);
                    API.setEntityDimension(target.Handle, player.dimension);
                }
            }
            return;
        }

        [Command("spec", "~r~ADMIN: ~s~/spec [Id/PartieDuNom]")]
        public void Spec(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
                if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                else
                {
                    API.setEntityTransparency(player.handle, 0);
                    API.setEntityPosition(player, API.getEntityPosition(target.Handle));
                    API.setPlayerToSpectatePlayer(player, target.Handle);
                }
            }
            return;
        }

        [Command("unspec", "~r~ADMIN: ~s~/unspec")]
        public void Unspec(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.unspectatePlayer(player);
                API.setEntityTransparency(player.handle, 255);
            }
        }

        [Command("gotocar", "~r~ADMIN: ~s~/gotocar [IdDuVehicule]")]
        public void Gotocar(Client player, String id)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                int idveh = Int32.Parse(id);
                {
                    Vector3 vpos = API.getEntityPosition(VehiculeInfo.GetVehicleById(idveh));
                    API.setEntityPosition(player, vpos);
                    API.sendNotificationToPlayer(player, "~g~Tu viens de te téléporter sur le vehicule: " + idveh);
                }
            }
        }

        [Command("getincar", "~r~ADMIN: ~s~/getincar [IdDuVehicule]")]
        public void Getincar(Client player, String id)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                int idveh = Int32.Parse(id);
                {
                    API.setPlayerIntoVehicle(player, VehiculeInfo.GetVehicleById(idveh), -1);
                    API.sendNotificationToPlayer(player, "~g~Tu viens de te téléporter dans le vehicule: " + idveh);
                }
            }
        }

        [Command("lspd")]
        public void Lspd(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.setEntityPosition(player, new Vector3(447.1f, -984.21f, 30.69f));
            }
        }

        [Command("airc", "~r~ADMIN: ~s~/airc")]
        public void Airc(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if (objplayer.IrcActif != true)
                {
                    objplayer.IrcActif = true;
                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'activer ton chat IRC.");
                    return;
                }
                else
                {
                    objplayer.IrcActif = false;
                    API.sendChatMessageToPlayer(player, "~r~Tu viens de désactiver ton chat IRC.");
                }
            }
        }

        [Command("apm", GreedyArg = true)]
        public void Command_apm(Client player, String idOrName, string message)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            else
            {
                var msg_to_sender = "~r~PMADMIN envoyé à " + Fonction.RemoveUnderscore(target.PlayerName) + ":~s~ " + message;
                API.sendChatMessageToPlayer(player, msg_to_sender);
                var msg = "~r~PMADMIN reçu de " + Fonction.RemoveUnderscore(player.name) + ":~s~ " + message;
                API.sendChatMessageToPlayer(target.Handle, msg);
            }
            return;
        }

        [Command("areanimer", "~p~ADMIN: ~s~/areanimer [IdOuPartieDuNom]")]
        public void Areanimer(Client player, string idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                API.stopPlayerAnimation(target.Handle);
                API.sendNotificationToPlayer(target.Handle, "~g~Tu viens d'être réanimé par un Admin.");
                API.sendNotificationToPlayer(player, String.Format("~g~Tu as réanimé {0}", target.PlayerName));
                target.IsDead = false;
                API.setPlayerHealth(target.Handle, 50);
            }
        }
    }
}