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
    public class Connexion : Script
    {

        [Flags]
        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        }

        public Connexion()
        {
            API.onPlayerFinishedDownload += OnPlayerFinishedDownloadHandler;
            API.onPlayerDisconnected += OnPlayerDisconnectedHandler;
            API.onClientEventTrigger += OnClientEvent;
        }

        //Verification compte existant + Freeze et application de la camera connexion

        private void OnPlayerFinishedDownloadHandler(Client player)
        {
            if(IsBan(player))
            {
                API.kickPlayer(player, "~r~Tu es banni du serveur, Rendez-vous sur Generation-Five.fr pour plus d'informations");
            }
            else
            {
                PlayerInfo objplayer = new PlayerInfo(player);
                //player.freeze(true);
                Cams.createCameraActive(player, new Vector3(332.8148, -1625.127, 98.49599), new Vector3(0, 0, 18.26657));
                //API.setEntityPosition(player, new Vector3(334.1689, -1628.986, 98.49599));
                API.sleep(5000);
                API.triggerClientEvent(player, "showLogin");
                if (CompteJoueurExistant(player) != -1)
                {
                    API.sendChatMessageToPlayer(player, "~s~Bienvenue sur ~b~GenerationFive~s~.");
                    API.sendChatMessageToPlayer(player, "~s~Tu es connecté avec le compte social club~b~ " + player.socialClubName + "~s~.");
                    API.sendChatMessageToPlayer(player, "~s~Ton compte rôleplay a été trouvé au nom de ~b~" + Fonction.RemoveUnderscore(player.name) + "~s~.");
                    API.sendChatMessageToPlayer(player, "~s~Version actuelle du serveur ~b~" + Constante.Version + "~s~.");
                    return;
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "~s~Bienvenue sur ~b~GenerationFive~s~.");
                    API.sendChatMessageToPlayer(player, "~s~Tu es connecté avec le compte social club~b~ " + player.socialClubName + " ~s~.");
                    API.sendChatMessageToPlayer(player, "~s~Tu n'as ~b~pas encore de compte rôleplay~s~, entre un ~b~mot de passe ~s~pour en créer un.");
                    API.sendChatMessageToPlayer(player, "~s~Version actuelle du serveur ~b~" + Constante.Version + "~s~.");
                }
            }
        }

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
            if (eventName == "CefConnexion")
            {
                int cmptexiste = CompteJoueurExistant(player);
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                string hashmdp = Fonction.ConvertSHA256(String.Format("" + arguments[1]));
                switch (cmptexiste)
                {
                    case -1: /*Le compte n'existe pas*/
                        API.triggerClientEvent(player, "showLogin");
                        break;

                    default: /*Le compte existe*/
                        DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Comptes WHERE id = " + cmptexiste);
                        foreach (DataRow row in result.Rows)
                        {                            
                            string motdepasse = String.Format("" + row["Password"]);
                            if (hashmdp == motdepasse)
                                LoadCompte(player, cmptexiste);
                            else
                            {
                                API.sendChatMessageToPlayer(player, "~r~Le Mot De Passe est incorrect");
                                var ec = API.getEntityData(player, "EssaiConnexion");
                                if (ec == 3)
                                {
                                    API.kickPlayer(player, "~r~Trop de tentatives de connexion échouées");
                                }
                                else
                                {
                                    API.setEntityData(player, "EssaiConnexion", ec + 1);
                                    API.triggerClientEvent(player, "showLogin");
                                }
                            }
                        }
                        break;
                }
            }

            if (eventName == "CefInscription")
            {
                int cmptexiste = CompteJoueurExistant(player);
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                string hashmdp = Fonction.ConvertSHA256(String.Format("" + arguments[3]));
                if (cmptexiste == -1)
                {
                    API.exported.database.executeQuery("INSERT INTO Comptes VALUES ('','" + player.socialClubName + "', '"+ arguments[0] +"', '"+ hashmdp +"', '"+ arguments[2] +"', 'false')");
                    API.exported.database.executeQuery("INSERT INTO Utilisateur (Compte, PlayerName) VALUES ('" + CompteJoueurExistant(player) + "', '" + arguments[1] + "')");
                }
            }
        }

        //Sauvegarde BDD Compte Deconnexion

        private void OnPlayerDisconnectedHandler(Client player, string reason)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            AmendeInfo.DeleteAllForPlayer(player);
            if (objplayer.Logged && objplayer.Spawned)
            {
                SaveCompte(player);
                SavePosJoueur(player);
            }
            PlayerInfo.Delete(objplayer);
        }

        //Fonctions Diverses De Connexion

        /* Methode de vérification du compte existant
         * retour possible :
         * -1 : aucun compte a été trouvé
         * ID quelquonque : correspon a l'id de l'utilisateur dans la table Utilisateur
         * 
         */
        public int CompteJoueurExistant(Client player) //
        {
            //Verification avec le pseudo
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Comptes WHERE SCN = '" + player.socialClubName + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["id"]);
                }
            }
            //PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            //Verification avec le social club
            /*DataTable result2 = API.exported.database.executeQueryWithResult("SELECT * FROM Utilisateur WHERE SCN = '" + player.socialClubName + "'");
            if (result2.Rows.Count != 0)
            {
                foreach (DataRow row in result2.Rows)
                {
                    string namebdd = String.Format("" + row["PlayerName"]);
                    if (namebdd == player.name)
                    {
                        return Convert.ToInt32(row["ID"]);
                    }
                    API.sendChatMessageToPlayer(player, String.Format("~r~Ton Prenom_Nom viens d'être changé en : {0}", namebdd));
                    API.setPlayerName(player, namebdd);
                    objplayer.PlayerName = namebdd;
                    return Convert.ToInt32(row["ID"]);
                }
            }*/

            //Verification avec le HWID - pas encore fais -
            /*DataTable result3 = API.exported.database.executeQueryWithResult("SELECT * FROM Utilisateur WHERE HWID = '" + player.uniqueHardwareId + "'");
            if (result3.Rows.Count != 0)
            {
                foreach (DataRow row in result3.Rows)
                {
                    string namebdd = String.Format("" + row["PlayerName"]);
                    if (namebdd == player.name)
                    {
                        return (Int16)row["ID"];
                    }
                    API.sendChatMessageToPlayer(player, String.Format("~r~Ton Prenom_Nom viens d'être changé en : {0}", namebdd));
                    API.setPlayerName(player, namebdd);
                    objplayer.PlayerName = namebdd;
                    return (Int16)row["ID"];
                }
            }*/
            return -1;
        }

        public bool VetementsExistant(Client player)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM UtilisateurVetements WHERE PlayerName = '" + player.name + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public void LoadCompte(Client player, int arg)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Utilisateur WHERE Compte = " + arg);
            foreach (DataRow row in result.Rows)
            {
                objplayer.IsFactionDuty = false;
                objplayer.Logged = true;
                objplayer.idDBCompte = arg;
                objplayer.PlayerName = Convert.ToString(row["PlayerName"]);
                objplayer.level = Convert.ToInt32(row["lvl"]);
                objplayer.adminlvl = Convert.ToInt32(row["adminlvl"]);
                objplayer.bank = Convert.ToInt32(row["bank"]);
                objplayer.money = Convert.ToInt32(row["money"]);
                objplayer.cuntdownpaye = Convert.ToInt32(row["cuntdownpaye"]);
                objplayer.pendingpaye = Convert.ToInt32(row["pendingpaye"]);
                objplayer.sante = Convert.ToInt32(row["sante"]);
                objplayer.armure = Convert.ToInt32(row["armure"]);
                objplayer.jobid = Convert.ToInt32(row["jobid"]);
                objplayer.factionid = Convert.ToInt32(row["factionid"]);
                objplayer.rangfaction = Convert.ToInt32(row["rangfaction"]);
                objplayer.entrepriseid = Convert.ToInt32(row["entrepriseid"]);
                objplayer.rangentreprise = Convert.ToInt32(row["rangentreprise"]);
                objplayer.dbid = Convert.ToInt32(row["ID"]);
                objplayer.IsOnPlanqueArmes = Convert.ToBoolean(row["IsOnPlanqueArmes"]);
                objplayer.IsOnPlanqueDrogues = Convert.ToBoolean(row["IsOnPlanqueDrogues"]);
                objplayer.TimerKitArmes = Convert.ToInt32(row["TimerKitArmes"]);
                objplayer.IsOnInt = Convert.ToBoolean(row["IsOnInt"]);
                objplayer.dimension = Convert.ToInt32(row["dimension"]);
                objplayer.IsMenotter = Convert.ToBoolean(row["IsMenotter"]);
                objplayer.IsDead = Convert.ToBoolean(row["IsDead"]);
                objplayer.position = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                objplayer.rotation = new Vector3(float.Parse(String.Format("" + row["RotX"])), float.Parse(String.Format("" + row["RotY"])), float.Parse(String.Format("" + row["RotZ"])));
            }
            API.consoleOutput(String.Format("[COMPTES] Chargement du compte de {0}({1}) effectué.", player.socialClubName, player.name));
            SpawnPlayer(player);
        }
        public void connexion(Client player)
        {
            return;
        }

        public void SpawnPlayer(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM UtilisateurVisage WHERE ID = " + objplayer.dbid);
            {
                if (result.Rows.Count != 1)
                {
                    API.call("CharCreator", "SendToCreator", player);
                    return;
                }
            }
            API.call("CharCreator", "LoadCharacter", player);
            LoadVetements(player);
            LoadAccessoires(player);
            //LoadAmende(player);
            API.setEntityPosition(player, objplayer.position);
            API.setEntityRotation(player, objplayer.rotation);
            API.setEntityDimension(player, objplayer.dimension);
            API.setPlayerHealth(player, objplayer.sante);
            if (objplayer.factionid != Constante.Faction_Police)
            {
                API.setPlayerArmor(player, objplayer.armure);
                API.call("SaveWeapons", "Load", player);
            }
            if (objplayer.IsMenotter == true)
            {
                API.setPlayerClothes(player, 7, (objplayer.sexe == 0 ? 41 : 25), 0);
                API.playPlayerAnimation(player, (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), "mp_arresting", "idle");
            }
            if (objplayer.IsDead == true)
            {
                API.playPlayerAnimation(player, (int)(SystemeMort.AnimationFlags.StopOnLastFrame), "combat@death@from_writhe", "death_c");
            }
            /*API.sendNativeToPlayer(player, Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, 631614199, 464.5701f, -992.6641f, 25.06443f, true, 0); //Porte principale des cellules
            API.sendNativeToPlayer(player, Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, 631614199, 461.8065f, -994.4086f, 25.06443f, true, 0); //Porte Cellule 1
            API.sendNativeToPlayer(player, Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, 631614199, 461.8065f, -997.6583f, 25.06443f, true, 0); //Porte Cellule 2
            API.sendNativeToPlayer(player, Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, 631614199, 461.8065f, -1001.302f, 25.06443f, true, 0); //Porte Cellule 3
            API.sendNativeToPlayer(player, Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, -1033001619, 463.4782f, -1003.538f, 25.00599f, true, 0); //Porte Arriere Cellules*/
            API.sendNativeToPlayer(player, Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, 1557126584, 450.1041f, -985.7384f, 30.8393f, true, 0); //Porte Acces Vestiere
            
            Cams.clearCameras(player);

            objplayer.Spawned = true;

            API.delay(1000, true, () => { API.call("DoorManager", "refreshallDoor", player); });
            API.delay(2000, true, () => { API.call("DoorManager", "refreshallDoor", player); });
            API.delay(5000, true, () => { API.call("DoorManager", "refreshallDoor", player); });
            API.delay(6000, true, () => { API.call("DoorManager", "refreshallDoor", player); });
            API.delay(7000, true, () => { API.call("DoorManager", "refreshallDoor", player); });
        }

        public void SaveCompte(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            objplayer.sante = API.getPlayerHealth(player);
            objplayer.armure = API.getPlayerArmor(player);
            if (objplayer.factionid != Constante.Faction_Police) API.call("SaveWeapons", "Save", player);
            string requete;
            requete = "UPDATE Utilisateur SET ";
            requete += String.Format("lvl={0}", objplayer.level);
            foreach (var prop in objplayer.GetType().GetProperties())
            {
                requete += String.Format(",{0}={1}", prop.Name, prop.GetValue(objplayer, null));
            }
            requete += " WHERE Compte = '" + objplayer.idDBCompte + "'";
            API.exported.database.executeQuery(requete);
        }

        public void SavePosJoueur(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            objplayer.position = API.getEntityPosition(player.handle);
            objplayer.rotation = API.getEntityRotation(player.handle);
            API.exported.database.executeQuery("UPDATE Utilisateur SET PosX='" + objplayer.position.X + "',PosY='" + objplayer.position.Y + "',PosZ='" + objplayer.position.Z + "',RotX='" + objplayer.rotation.X + "',RotY='" + objplayer.rotation.Y + "',RotZ='" + objplayer.rotation.Z + "' WHERE PlayerName = '" + player.name + "'");
        }
        
        public void LoadVetements(Client player)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM UtilisateurVetements WHERE PlayerName = '" + player.name + "'");
            foreach (DataRow row in result.Rows)
            {
                string[] entete = { "draw0", "draw1", "draw2", "draw3", "draw4", "draw5", "draw6", "draw7", "draw8", "draw9", "draw10", "draw11" };
                string[] entete2 = { "tx0", "tx1", "tx2", "tx3", "tx4", "tx5", "tx6", "tx7", "tx8", "tx9", "tx10", "tx11" };
                for (int i = 0; i < entete.Length; i++)
                {
                    API.setEntityData(player, entete[i], row[entete[i]]);
                    API.setEntityData(player, entete2[i], row[entete2[i]]);
                    API.setPlayerClothes(player, i, int.Parse(String.Format("" + row[entete[i]])), int.Parse(String.Format("" + row[entete2[i]])));
                }
            }
        }

        public void LoadAccessoires(Client player)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM UtilisateurVetements WHERE PlayerName = '" + player.name + "'");
            foreach (DataRow row in result.Rows)
            {
                string[] entete = { "propdraw0", "propdraw1", "propdraw2", "propdraw3", "propdraw4", "propdraw5", "propdraw6", "propdraw7", "propdraw8", "propdraw9" };
                string[] entete2 = { "proptx0", "proptx1", "proptx2", "proptx3", "proptx4", "proptx5", "proptx6", "proptx7", "proptx8", "proptx9" };
                for (int i = 0; i < entete.Length; i++)
                {
                    int rentete = int.Parse(String.Format("" + row[entete[i]]));
                    if (rentete != 0)
                    {
                        API.setEntityData(player, entete[i], row[entete[i]]);
                        API.setEntityData(player, entete2[i], row[entete2[i]]);
                        API.setPlayerAccessory(player, i, int.Parse(String.Format("" + row[entete[i]])), int.Parse(String.Format("" + row[entete2[i]])));
                    }
                }
            }
        }

        public void LoadAmende(Client player)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM UtilisateurAmende WHERE playername = '" + player.name + "'");
            foreach (DataRow row in result.Rows)
            {
                new AmendeInfo(player, (int)row["montant"], (string)row["raison"], (string)row["auteur"], (int)row["date"]);
            }
        }

        public bool IsBan(Client player)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Comptes WHERE SCN = '" + player.socialClubName + "'");
            foreach(DataRow row in result.Rows)
            {
                if (Convert.ToBoolean(row["IsBan"]) == true)
                {
                    return true;
                }
                else return false;
            }
            return false;
        }
    }
}