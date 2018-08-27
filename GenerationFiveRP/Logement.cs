using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Data;

namespace GenerationFiveRP
{
    public class Logement : Script
    {
        //public Vector3 app1 = new Vector3(261.4586, -998.8196, -99.00863);
        public Vector3 app1 = new Vector3(266.2844, -1007.04, -100.9328);
        public Vector3 app2 = new Vector3(-35.31277, -580.4199, 88.71221);

        public Logement()
        {
            LoadLogement();
        }

        #region Commandes Logements

        [Command("location", "~y~UTILISATION: ~w~/location [On/Off]")]
        public void Location(Client player, string option)
        {
            if (IsProprietaire(player))
            {
                switch (option)
                {
                    case "On":
                        PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                        SetLocation(GetLogementID(player), true);
                        API.sendChatMessageToPlayer(player, "Tu viens ~g~d'activer ~s~la location de ton logement");
                        RemoveHomeKeyToPlayer(objplayer.dbid, GetLogementID(player));
                        break;
                    case "Off":
                        if (!HasLocataire(GetLogementID(player)))
                        {
                            SetLocation(GetLogementID(player), false);
                            API.sendChatMessageToPlayer(player, "Tu viens ~r~d'activer ~s~la location de ton logement");
                            AddHomeKeyToPlayer(player, GetLogementID(player));
                            return;
                        }
                        else API.sendChatMessageToPlayer(player, "~r~Tu dois d'abord expulser ton locataire avant de désactiver la location.");
                        break;
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'es pas propriétaire d'une maison.");
            }

        }

        [Command("inviterlocataire", "~y~UTILISATION: ~w~/inviterlocataire [id/PartieDuNom]")]
        public void Inviterlocataire(Client player, String IdOrName)
        {
            if (HasLocataire(GetLogementID(player)))
            {
                API.sendChatMessageToPlayer(player, "~r~Tu dois d'abord expulser ton locataire actuel avant d'effectuer cette action.");
                return;
            }
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(IdOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            API.sendChatMessageToPlayer(player, String.Format("Tu viens d'envoyer une demande de locataire a ~b~{0}~s~.", target.PlayerName));
            target.DemandeLocation = true;
            target.IDLogementLocation = GetLogementID(player);
            target.ClientDemandeMaison = player;
            API.sendChatMessageToPlayer(target.Handle, String.Format("Tu viens de recevoir une demande de location de ~b~{0} ~s~(/accepterlocation).", player.name));
            return;
        }

        [Command("prixlocation", "~y~UTILISATION: ~w~/prixlocation [Prix]")]
        public void Prixlocation(Client player, int prix)
        {
            if (!IsProprietaire(player))
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas de logement.");
                return;
            }
            SetPrixLocation(GetLogementID(player), prix);
            API.sendChatMessageToPlayer(player, String.Format("Le prix de ton logement est desormais de : ~g~{0}$~s~.", prix));
        }

        [Command("expulserlocataire")]
        public void Expulserlocataire(Client player, String IdOrName)
        {
            int IDLocataire = GetIDLocataire(GetLogementID(player));
            if (IDLocataire != 0)
            {
                RemoveLocataire(GetLogementID(player), IDLocataire);
                RemoveHomeKeyToPlayer(IDLocataire, GetLogementID(player));
                API.sendChatMessageToPlayer(player, "Tu viens ~g~d'expulser ~s~ton locataire.");
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfoByIdBDD(IDLocataire);
                if (objtarget == null) return;
                else API.sendChatMessageToPlayer(objtarget.Handle, "Tu viens de te faire ~r~expulser ~s~de ton logement.");
                return;
            }
            else API.sendChatMessageToPlayer(player, "~r~Tu n'as pas de locataire.");
        }

        [Command("delouermaison")]
        public void Delouermaison(Client player)
        {
            if (IsLocataire(player))
            {
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                API.sendChatMessageToPlayer(player, "Tu viens de ~g~delouer ~s~ton logement.");
                RemoveLocataire(GetIdLogementLocataire(player), objplayer.dbid);
                return;
            }
            else API.sendChatMessageToPlayer(player, "~g~Tu ne loue aucun logement.");
        }

        [Command("accepterlocation")]
        public void Accepterlocation(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.DemandeLocation == true)
            {
                objplayer.DemandeLocation = false;
                API.sendChatMessageToPlayer(player, "Tu viens ~g~d'accepter~s~ la demande de location.");
                SetLocataire(objplayer.IDLogementLocation, player);
                API.sendChatMessageToPlayer(objplayer.ClientDemandeMaison, "~g~Ta demande de location a été acceptée.");
            }
            else
            {
                API.sendChatMessageToPlayer(objplayer.ClientDemandeMaison, "~r~Tu n'as pas de demande de location.");
            }
        }

        [Command("invitercolocataire", "~y~UTILISATION: ~w~/invitercolocataire [id/PartieDuNom]")]
        public void Invitercolocataire(Client player, String IdOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(IdOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            if (IsProprietaire(player))
            {
                if (HasColocataire(GetLogementID(player)))
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu dois d'abord expulser ton colocataire actuel avant d'effectuer cette action.");
                }
                else
                {
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens d'envoyer une demande de colocataire a ~b~{0}~s~.", target.PlayerName));
                    target.DemandeColocation = true;
                    target.IDLogementColocation = GetLogementID(player);
                    target.ClientDemandeMaison = player;
                    API.sendChatMessageToPlayer(target.Handle, String.Format("Tu viens de recevoir une demande de colocation de ~b~{0} ~s~(/acceptercolocation).", player.name));
                    return;
                }
            }
            if (IsLocataire(player))
            {
                if (HasColocataire(GetIdLogementLocataire(player)))
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu dois d'abord expulser ton colocataire actuel avant d'effectuer cette action.");
                }
                else
                {
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens d'envoyer une demande de colocataire a ~b~{0}~s~.", target.PlayerName));
                    target.DemandeColocation = true;
                    target.IDLogementColocation = GetIdLogementLocataire(player);
                    target.ClientDemandeMaison = player;
                    API.sendChatMessageToPlayer(target.Handle, String.Format("Tu viens de recevoir une demande de colocation de ~b~{0} ~s~(/acceptercolocation).", player.name));
                    return;
                }
            }
        }

        [Command("acceptercolocation")]
        public void Acceptercolocation(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.DemandeColocation == true)
            {
                objplayer.DemandeColocation = false;
                API.sendChatMessageToPlayer(player, "Tu viens ~g~d'accepter~s~ la demande de colocation.");
                SetColocataire(objplayer.IDLogementColocation, player);
                API.sendChatMessageToPlayer(objplayer.ClientDemandeMaison, "~g~Ta demande de colocation a été acceptée.");
            }
            else
            {
                API.sendChatMessageToPlayer(objplayer.ClientDemandeMaison, "~r~Tu n'as pas de demande de colocation.");
            }
        }

        [Command("expulsercolocataire")]
        public void Expulsercolocataire(Client player)
        {
            if (IsProprietaire(player))
            {
                if (GetIDLocataire(GetLogementID(player)) != 0)
                {
                    RemoveLocataire(GetLogementID(player), GetIDLocataire(GetLogementID(player)));
                    API.sendChatMessageToPlayer(player, "Tu viens ~g~d'expulser ~s~ton colocataire.");
                    PlayerInfo objtarget = PlayerInfo.GetPlayerInfoByIdBDD(GetIDColocataire(GetLogementID(player)));
                    if (objtarget == null) return;
                    else API.sendChatMessageToPlayer(objtarget.Handle, "Tu viens de te faire ~r~expulser ~s~de ton logement.");
                    return;
                }
                else API.sendChatMessageToPlayer(player, "~r~Tu n'as pas de colocataire.");
                return;
            }
            if (IsLocataire(player))
            {
                if (GetIDColocataire(GetIdLogementLocataire(player)) != 0)
                {
                    RemoveColocataire(GetIdLogementLocataire(player), GetIDColocataire(GetIdLogementLocataire(player)));
                    API.sendChatMessageToPlayer(player, "Tu viens ~g~d'expulser ~s~ton colocataire.");
                    PlayerInfo objtarget = PlayerInfo.GetPlayerInfoByIdBDD(GetIDColocataire(GetIdLogementLocataire(player)));
                    if (objtarget == null) return;
                    else API.sendChatMessageToPlayer(objtarget.Handle, "Tu viens de te faire ~r~expulser ~s~de ton logement.");
                    return;
                }
                else API.sendChatMessageToPlayer(player, "~r~Tu n'as pas de colocataire.");
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Tu ne peux pas effectuer cette action.");
            }
        }

        #endregion

        #region Commandes Garage

        [Command("creergarage")]
        public void Creergarage(Client player, int IDMaison)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDMaison + "'");
                if (result.Rows.Count != 0)
                {
                    API.exported.database.executeQuery("INSERT INTO Garages VALUE ('', '" + IDMaison + "', '" + player.position.X + "', '" + player.position.Y + "', '" + player.position.Z + "', '', '', '')");
                    API.sendChatMessageToPlayer(player, "~g~Le garage a bien été créé.");
                    return;
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "~r~Ce logement n'éxiste pas.");
                    return;
                }
            }
        }

        #endregion

        /*[Command("definirplace", "~y~JOUEUR: ~s~/definirplace")]
        public void Definirplace(Client player)
        {
            if (!player.isInVehicle)
            {
                API.sendChatMessageToPlayer(player, "Tu n'es pas dans un ~r~Vehicule~s~.");
                return;
            }
            if(!SpawnVehicule.VerifDispoPlaceParking(player.vehicle.position))
            {
                API.sendChatMessageToPlayer(player, "Cette ~r~place ~s~est déjà ~r~utilisée ~s~par un autre vehicule.");
                return;
            }
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objveh.IDBDDProprio != objplayer.dbid)
            {
                API.sendChatMessageToPlayer(player, "Tu n'es pas ~r~propriétaire ~s~de ce ~r~Vehicule~s~.");
                return;
            }
            if (IsProprietaire(player) && player.position.DistanceTo(GetLogementPos(GetLogementID(player))) < 30)
            {
                SpawnVehicule.SetVehSpawnPoint(player.vehicle, player.vehicle.position, player.vehicle.rotation);
                API.sendChatMessageToPlayer(player, "La ~g~place de parking ~s~de ton ~g~Vehicule ~s~a bien été définis.");
                return;
            }
            if (IsLocataire(player) && player.position.DistanceTo(GetLogementPos(GetIdLogementLocataire(player))) < 30)
            {
                SpawnVehicule.SetVehSpawnPoint(player.vehicle, player.vehicle.position, player.vehicle.rotation);
                API.sendChatMessageToPlayer(player, "La ~g~place de parking ~s~de ton ~g~Vehicule ~s~a bien été définis.");
                return;
            }
            else
            {
                SpawnVehicule.SetVehSpawnPoint(player.vehicle, player.vehicle.position, player.vehicle.rotation);
                objplayer.money = objplayer.money - 500;
                API.shared.triggerClientEvent(player, "update_money_display", objplayer.money);
                API.sendChatMessageToPlayer(player, "La ~g~place de parking ~s~de ton ~g~Vehicule ~s~a bien été définis pour 500$.");
                return;
            }
            
        }*/

        #region ScriptEvent

        public void ScriptEvent(Client sender, PlayerInfo objplayer)
        {
            if (!API.isPlayerInAnyVehicle(sender))
            {
                #region AccesGarageVersLogement
                if (!sender.isInVehicle & sender.position.DistanceTo(new Vector3(179.0791, -1000.706, -98.99995)) < 2)
                {
                    DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + objplayer.dimension + "'");
                    foreach (DataRow row in result.Rows)
                    {
                        String app = Convert.ToString(row["model"]);
                        if (app == "app1")
                        {
                            Logement test = new Logement();
                            API.setEntityPosition(sender, test.app1);
                            objplayer.IsOnInt = true;
                        }
                        if (app == "app2")
                        {
                            Logement test = new Logement();
                            API.setEntityPosition(sender, test.app2);
                            objplayer.IsOnInt = true;
                        }
                    }
                    return;
                }
                #endregion

                #region AccesGarageVersExterieur
                if (!sender.isInVehicle & sender.position.DistanceTo(new Vector3(172.8933, -1007.904, -98.99995)) < 2)
                {
                    DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Garages WHERE ID = '" + objplayer.dimension + "'");
                    foreach (DataRow row in result.Rows)
                    {
                        API.triggerClientEvent(sender, "MenuGarageToExt");
                        /*API.setEntityPosition(sender, new Vector3(Convert.ToDouble(row["PosX"]), Convert.ToDouble(row["PosY"]), Convert.ToDouble(row["PosZ"])));
                        API.setEntityDimension(sender, 0);
                        objplayer.dimension = 0;
                        objplayer.IsOnInt = false;*/
                        return;
                    }
                }
                #endregion

                #region AccesLogementVersExterieur
                if (objplayer.IsOnInt == true)
                {
                    int IDlog = API.getEntityDimension(sender);
                    DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlog + "'");
                    foreach (DataRow row in result.Rows)
                    {
                        String app = Convert.ToString(row["model"]);
                        if (app == "app1")
                        {
                            Vector3 pos = new Vector3(266.2844, -1007.04, -100.9328);
                            if (sender.position.DistanceTo(pos) < 2)
                            {
                                API.triggerClientEvent(sender, "MenuSortirLogement");
                            }
                        }
                        if (app == "app2")
                        {
                            Vector3 pos = new Vector3(-35.31277, -580.4199, 88.71221);
                            if (sender.position.DistanceTo(pos) < 2)
                            {
                                Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                                API.setEntityPosition(sender, logpos);
                                API.setEntityDimension(sender, 0);
                                objplayer.dimension = 0;
                                objplayer.IsOnInt = false;
                            }
                        }
                    }
                }
                #endregion

                #region AccesExterieurVersGarage
                DataTable accesgarage = API.exported.database.executeQueryWithResult("SELECT * FROM Garages");
                foreach (DataRow row in accesgarage.Rows)
                {
                    if(!sender.isInVehicle & sender.position.DistanceTo(new Vector3(Convert.ToDouble(row["PosX"]), Convert.ToDouble(row["PosY"]), Convert.ToDouble(row["PosZ"]))) < 2)
                    {
                        API.triggerClientEvent(sender, "MenuExtToGarage");
                        return;
                    }
                }
            #endregion
            }
        }
        #endregion

        #region Location

        public static bool IsOnLocation(int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            foreach (DataRow row in result.Rows)
            {
                if (Convert.ToInt32(row["EnLocation"]) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasLocataire(int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            foreach (DataRow row in result.Rows)
            {
                if (Convert.ToInt32(row["IDLocataire"]) == -1) return false;
                else return true;
            }
            return false;
        }

        public static bool IsLocataire(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE IDLocataire = '" + objplayer.dbid + "'");
            if(result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsColocataire(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE IDColocataire = '" + objplayer.dbid + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static int GetIdLogementLocataire(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE IDLocataire = '" + objplayer.dbid + "'");
            foreach (DataRow row in result.Rows)
            {
                return Convert.ToInt32(row["ID"]);
            }
            return -1;
        }

        public static void RemoveLocataire(int IDlogement, int Locataire)
        {
            API.shared.exported.database.executeQuery("UPDATE Logements SET IDLocataire='-1' WHERE ID = '" + IDlogement + "'");
            Logement.RemoveHomeKeyToPlayer(Locataire, IDlogement);
        }

        public static int GetIDLocataire(int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            foreach (DataRow row in result.Rows)
            {
                return Convert.ToInt32(row["IDLocataire"]);
            }
            return -1;
        }

        public static void SetLocation(int IDlogement, bool option)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            if (result.Rows.Count != 0)
            {
                API.shared.exported.database.executeQuery("UPDATE Logements SET  EnLocation='"+ Convert.ToInt32(option) +"' WHERE ID = '" + IDlogement + "'");
                return;
            }
            return;
        }

        public static bool GetEtatLocation(int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            if (result.Rows.Count != 0)
            {
                foreach(DataRow row in result.Rows)
                {
                    if (Convert.ToBoolean(row["EnLocation"]) == true) return true;
                    else return false;
                }
            }
            return false;
        }

        public static void SetPrixLocation(int IDlogement, int Prix)
        {
            API.shared.exported.database.executeQuery("UPDATE Logements SET PrixLocation='" + Prix + "' WHERE ID = '" + IDlogement + "'");
        }

        public static void SetLocataire(int IDlogement, Client Locataire)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(Locataire);
            API.shared.exported.database.executeQuery("UPDATE Logements SET  Locataire='" + objplayer.dbid + "' WHERE ID = '" + IDlogement + "'");
            Logement.AddHomeKeyToPlayer(Locataire, IDlogement);
        }
        #endregion

        #region Colocation

        public void SetColocataire(int IDlogement, Client Colocataire)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(Colocataire);
            API.exported.database.executeQuery("UPDATE Logements SET IDColocataire='" + objplayer.dbid + "' WHERE ID = '" + IDlogement + "'");
        }

        public static bool HasColocataire(int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            foreach (DataRow row in result.Rows)
            {
                if (Convert.ToInt32(row["IDColocataire"]) != -1) return true;
                else return false;
            }
            return false;
        }

        public static void RemoveColocataire(int IDlogement, int Colocataire)
        {
            API.shared.exported.database.executeQuery("UPDATE Logements SET IDColocataire='-1' WHERE ID = '" + IDlogement + "'");
            Logement.RemoveHomeKeyToPlayer(Colocataire, IDlogement);
        }

        public static int GetIDColocataire(int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            foreach (DataRow row in result.Rows)
            {
                return Convert.ToInt32(row["IDColocataire"]);
            }
            return -1;
        }

        #endregion

        #region Faonctions Générales

        public static bool IsLogement(Client sender)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                    if (sender.position.DistanceTo(logpos) < 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasProprietaire(Client player, int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            foreach (DataRow row in result.Rows)
            {
                if (Convert.ToString(row["proprietaire"]) == "Aucun")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsProprietaire(Client player)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE proprietaire = '" + player.name + "'");
            if(result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static int GetLogementID(Client player)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    if (player.name == Convert.ToString(row["proprietaire"]))
                    {
                        return Convert.ToInt32(row["ID"]);
                    }
                }
            }
            return -1;
        }

        public static int GetLogementIDProche(Client player)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                    if (player.position.DistanceTo(logpos) < 2)
                    {
                        return Convert.ToInt32(row["ID"]);
                    }
                }
            }
            return -1;
        }

        public static bool PlayerHaveKeyHouse(Client player, int IDMaison)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM ClefsLogements WHERE IDLogement = '" + IDMaison + "' AND IDJoueur = '" + objplayer.dbid + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static void AddHomeKeyToPlayer(Client player, int IDlogement)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            API.shared.exported.database.executeQuery("INSERT INTO ClefsLogements VALUE ('', '"+ IDlogement +"', '" + objplayer.dbid + "')");
        }

        public static void RemoveHomeKeyToPlayer(int IDplayer, int IDlogement)
        {
            API.shared.exported.database.executeQuery("DELETE * FROM ClefsLogements WHERE IDJoueur ='" + IDplayer + "' AND IDLogement='"+ IDlogement +"'");
        }

        public static Vector3 GetLogementPos(int IDlogement)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID = '" + IDlogement + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Vector3 poslogement = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                    return poslogement;
                }
            }
            return new Vector3(0,0,0);
        }

        public void LoadLogement()
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Logements");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Vector3 poslogement = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                    //poslogement.Z = poslogement.Z - 1;
                    //String textdraw = String.Format("Maison ID : {0}", row["ID"]);
                    //API.createTextLabel(textdraw, poslogement, 25, 1);
                    API.createMarker(0, poslogement, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), 150, 255, 255, 0, 0);
                }
            }
        }

        #endregion
    }
}
