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
    public class CreationMenus : Script
    {
        public CreationMenus()
        {
        }

        public void ClientEvent(Client player, PlayerInfo objplayer, string eventName)
        {
            switch (eventName)
            {
            #region Bouton E
                case "Bouton.E":
                    //Entrée Auto-Ecole
                    if (!player.isInVehicle && player.position.DistanceTo(Constante.Pos_EntrerAutoEcole) < 2)
                    {
                        API.setEntityPosition(player, Constante.Pos_SortieAutoEcole);
                        return;
                    }

                    if (!player.isInVehicle && player.position.DistanceTo(Constante.Pos_EntrerPrison) < 4 && objplayer.factionid == Constante.Faction_Gardien)
                    {

                        API.setEntityPosition(player, Constante.Pos_SortiePrison);
                        objplayer.IsFactionDuty = true;
                        API.sendChatMessageToPlayer(player, "~#d2d628~", "Tu viens de prendre ton service.");

                        if (objplayer.sexe == 0)
                        {
                            API.setPlayerClothes(player, 3, 0, 0);
                            API.setPlayerClothes(player, 4, 35, 0);
                            API.setPlayerClothes(player, 5, 0, 0);
                            API.setPlayerClothes(player, 6, 25, 0);
                            API.setPlayerClothes(player, 7, 0, 0);
                            API.setPlayerClothes(player, 8, 58, 0);
                            API.setPlayerClothes(player, 11, 55, 0);
                        }
                        else
                        {
                            API.setPlayerClothes(player, 3, 14, 0);
                            API.setPlayerClothes(player, 4, 34, 0);
                            API.setPlayerClothes(player, 5, 0, 0);
                            API.setPlayerClothes(player, 6, 25, 0);
                            API.setPlayerClothes(player, 7, 0, 0);
                            API.setPlayerClothes(player, 8, 35, 0);
                            API.setPlayerClothes(player, 11, 48, 0);
                        }
                        API.givePlayerWeapon(player, WeaponHash.Flashlight, 1, true, true);
                        API.givePlayerWeapon(player, WeaponHash.StunGun, 1, true, true);
                        API.givePlayerWeapon(player, WeaponHash.Nightstick, 1, true, true);
                        return;
                    }
                    if (!player.isInVehicle && player.position.DistanceTo(Constante.Pos_SortiePrison) < 4 && objplayer.factionid == Constante.Faction_Gardien)
                    {
                        API.setEntityPosition(player, Constante.Pos_EntrerPrison);
                        objplayer.IsFactionDuty = false;
                        API.removeAllPlayerWeapons(player);
                        objplayer.armure = 0;
                        API.sendChatMessageToPlayer(player, "~#d2d628~", "Tu viens de terminer ton service.");
                        API.call("Connexion", "LoadVetements", player);
                        API.call("Connexion", "LoadAccessoires", player);
                        return;
                    }

                    //Sortie Auto-Ecole
                    if (!player.isInVehicle && player.position.DistanceTo(Constante.Pos_SortieAutoEcole) < 2)
                    {
                        API.setEntityPosition(player, Constante.Pos_EntrerAutoEcole);
                        return;
                    }

                    //Job eboueur
                    if (!player.isInVehicle && player.position.DistanceTo(Constante.Pos_ServiceEboueur) < 2)
                    {
                        if (objplayer.jobid == Constante.Job_Eboueur)
                        {
                            if (objplayer.IsJobDuty == false)
                            {
                                API.setPlayerClothes(player, 3, 119, 0);
                                API.setPlayerClothes(player, 4, 36, 0);
                                API.setPlayerClothes(player, 6, 51, 0);
                                API.setPlayerClothes(player, 8, 59, 0);
                                API.setPlayerClothes(player, 11, 57, 0);
                                objplayer.IsJobDuty = true;
                            }
                            else
                            {
                                objplayer.IsJobDuty = false;
                                if (objplayer.pendingpaye > 0)
                                {
                                    API.sendChatMessageToPlayer(player, "Merci pour ton travail, tu reçevra ton argent sur ta prochaine paye!");
                                }
                                Connexion lv = new Connexion();
                                lv.LoadVetements(player);
                                lv.LoadAccessoires(player);
                                return;
                            }
                        }
                        else
                        {
                            API.sendChatMessageToPlayer(player, "~r~Tu ne fais pas partie ~s~de ce job, fais toi d'abord recruter. ~m~(/rejoindreeboueur)");
                        }
                    }

                    //Menu Auto-Ecole
                    if(!player.isInVehicle && player.position.DistanceTo(new Vector3(-139.1808, -631.9503, 168.8204)) < 2)
                    {
                        API.triggerClientEvent(player, "MenuAutoEcole");
                        return;
                    }

                    //Menu Armurerie
                    if (police.isArmurerieLSPD(player))
                    {
                        if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                            return;
                        if (objplayer.IsFactionDuty == true)
                        {
                            API.triggerClientEvent(player, "MenuArmurerieLSPD");
                        }
                        else
                        {
                            API.sendChatMessageToPlayer(player, Constante.PasEnService);
                        }
                        return;
                    }

                    //menu cellule
                    if (police.isCellule(player))
                    {
                        if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                            return;
                        if (objplayer.IsFactionDuty == true)
                        {
                            API.triggerClientEvent(player, "MenuCelluleLSPD");
                        }
                        else
                        {
                            API.sendChatMessageToPlayer(player, Constante.PasEnService);
                        }
                        return;
                    }

                    //Menu Distrib
                    if (police.isDistrib(player))
                    {
                        API.triggerClientEvent(player, "MenuDistrib");
                        return;
                    }

                    //Menu Service
                    if (police.isService(player))
                    {
                        if (Fonction.IsPlayerInFaction(objplayer, "Police", true))
                            API.triggerClientEvent(player, "MenuService");
                        return;
                    }

                    //Menu Armurerie civile
                    if (Fonction.isArmurerieCivil(player))
                    {
                        API.triggerClientEvent(player, "MenuArmurerieCivil");
                        return;
                    }

                    //Menu ATM
                    if (ATMInfo.GetATMInfoClosePos(player.position, 2) != null)
                    {
                        if(Fonction.IsPlayerInFaction(objplayer, "Gardien", false) && objplayer.IsJobDuty == true)
                        {
                            objplayer.sacbanque = false;
                            API.setPlayerClothes(player, 5, 0, 0);
                        }
                        else
                        {
                            API.triggerClientEvent(player, "showATM");
                            objplayer.IdATM = ATMInfo.GetATMInfoClosePos(player.position, 2).ID;
                            API.setEntitySyncedData(player, "SoldeCompte", objplayer.bank);
                        }
                        return;
                    }

                    //Menu Banque
                    if (BanqueInfo.GetBanqueInfoClosePos(player.position, 1) != null)
                    {
                        API.triggerClientEvent(player, "MenuBanque");
                        objplayer.IdBanque = BanqueInfo.GetBanqueInfoClosePos(player.position, 1).ID;
                        return;
                    }

                    //Menu Magasin
                    if (Magasin.isMagasin(player))
                    {
                        API.triggerClientEvent(player, "MenuMagasin");
                        return;
                    }

                    //Menu Revendeur
                    if (Magasin.isRevendeur(player))
                    {
                        API.triggerClientEvent(player, "MenuRevendeur");
                        return;
                    }

                    //Menu Logement
                    if (Logement.IsLogement(player))
                    {
                        if(Logement.HasProprietaire(player, Logement.GetLogementIDProche(player)))
                        {
                            if(Logement.PlayerHaveKeyHouse(player, Logement.GetLogementIDProche(player)))
                                API.triggerClientEvent(player, "MenuMaisonAchetéeProprio");
                            else
                                API.triggerClientEvent(player, "MenuMaisonAchetéeNonProprio");
                        }
                        else
                            API.triggerClientEvent(player, "MenuMaisonAVendre");
                        return;
                    }

                    //Entrée Planque
                    if (CommandesFaction.IsEntreePlanque(player) && objplayer.dimension == 0)
                    {
                        System.Data.DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction");
                        if (result.Rows.Count != 0)
                        {
                            foreach (System.Data.DataRow row in result.Rows)
                            {
                                String nominte = Convert.ToString(row["nominte"]);
                                int PorteOuverture = Convert.ToInt32(row["locked"]);
                                if (PorteOuverture == 1) API.shared.sendNotificationToPlayer(player, "La porte est fermée.");
                                else
                                {
                                    if (nominte == "InteArmes")
                                    {
                                        API.shared.setEntityPosition(player, CommandesFaction.InteArmes);
                                        objplayer.IsOnInt = true;
                                        objplayer.IsOnPlanqueArmes = true;
                                        API.shared.setEntityDimension(player, Convert.ToInt32(row["ID"]));
                                        objplayer.dimension = Convert.ToInt32(row["ID"]);
                                        return;
                                    }
                                    if (nominte == "InteDrogues")
                                    {
                                        //API.shared.setEntityPosition(player, InteDrogues);
                                        API.shared.sendChatMessageToPlayer(player, "En maintenance.");
                                        objplayer.IsOnInt = true;
                                        objplayer.IsOnPlanqueArmes = true;
                                        API.shared.setEntityDimension(player, Convert.ToInt32(row["ID"]));
                                        objplayer.dimension = Convert.ToInt32(row["ID"]);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    //Sortie Planque
                    if (CommandesFaction.IsSortiePlanque(player) && objplayer.dimension != 0)
                    {
                        System.Data.DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction");
                        if (result.Rows.Count != 0)
                        {
                            foreach (System.Data.DataRow row in result.Rows)
                            {
                                objplayer.IsOnInt = false;
                                API.shared.setEntityDimension(player, 0);
                                objplayer.dimension = 0;
                                Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                                API.shared.setEntityPosition(player, logpos);
                                objplayer.IsOnPlanqueArmes = false;
                                return;
                            }
                        }
                    }

                    //Menu hackeur
                    if (Hackeur.isRepairePNJ(player))
                    {
                        API.triggerClientEvent(player, "MenuPNJHackeur");
                        return;
                    }
                    if (API.getEntityData(player, "OrdiHack") == true)
                    {
                        API.triggerClientEvent(player, "MenuHackeur");
                        return;
                    }
                    if (Hackeur.isRepaireDehors(player))
                    {
                        API.setEntityPosition(player, new Vector3(1274.184, -1719.719, 54.77145));
                        return;
                    }
                    if (Hackeur.isRepaireDedans(player))
                    {
                        API.setEntityPosition(player, new Vector3(882.7369, -1052.517, 33.00666));
                        return;
                    }

                    //menu exte veh
                    if (!player.isInVehicle)
                    {
                        VehiculeInfo objveh = VehiculeInfo.GetVehicleArroundPlayer(player);
                        if(objveh != null)
                            API.triggerClientEvent(player, "MenuExteVeh");
                        return;
                    }

                    //menu veh eboueur
                    if (player.position.DistanceTo(Constante.Pos_CamionEboueur) < 2)
                    {
                        if (objplayer.jobid == Constante.Job_Eboueur)
                        {
                            if (objplayer.IsJobDuty == true)
                                API.triggerClientEvent(player, "MenuVehEboueur");
                            else
                                API.sendChatMessageToPlayer(player, "Tu n'es pas en service.");
                        }
                        else
                            API.sendChatMessageToPlayer(player, "Tu n'es pas éboueur.");
                        return;
                    }
                    break;
                #endregion
            #region Bouton F1
                case "Bouton.F1":  
                    //menu inte veh
                    if (player.isInVehicle == true)
                    {
                        if (player.isInVehicle == true)
                            API.triggerClientEvent(player, "MenuInteVeh");
                        return;
                    }
                    break;
                #endregion
            #region Bouton F2
                case "Bouton.F2":
                    //menu Unite LSPD
                    if (objplayer.factionid == Constante.Faction_Police && UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player) != null)
                    {
                        API.triggerClientEvent(player, "MenuUniteLSPD");
                        return;
                    }
                    break;
            #endregion
            #region Bouton R
                case "Bouton.R":
                    //event rechargement
                    if (Inventaire.GetItemNumberInBDD(player, 11) != 0)
                    {
                        API.triggerClientEvent(player, "RechargementPistol");
                    }
                    if (Inventaire.GetItemNumberInBDD(player, 12) != 0)
                    {
                        API.triggerClientEvent(player, "RechargementSMG");
                    }
                    if (Inventaire.GetItemNumberInBDD(player, 13) != 0)
                    {
                        API.triggerClientEvent(player, "RechargementRifle");
                    }
                    if (Inventaire.GetItemNumberInBDD(player, 14) != 0)
                    {
                        API.triggerClientEvent(player, "RechargementPompe");
                    }
                    break;
            #endregion
            }
        }

        public static NetHandle GetClosestVehicle(Client player, float distance = 2.0f)
        {
            NetHandle handleReturned = new NetHandle();
            foreach (var veh in API.shared.getAllVehicles())
            {
                Vector3 vehPos = API.shared.getEntityPosition(veh);
                float distanceVehicleToPlayer = player.position.DistanceTo(vehPos);
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