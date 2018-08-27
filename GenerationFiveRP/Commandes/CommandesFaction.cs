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
    public class CommandesFaction : Script
    {
        public CommandesFaction()
        {
            ColShapeEntree();
        }
        public static Vector3 InteArmes = new Vector3(1087.523, -3099.337, -38.99994);

        #region Commandes de base
        [Command("invite", "~y~FACTION: ~s~/invite [ID/Nom]")]
        public void Invite(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.factionid != 0)
            {
                if (objplayer.rangfaction <= 4)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasLeader);
                }
                else
                {
                    if (player.position.DistanceTo(target.Handle.position) >= 2)
                    {
                        API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                    }
                    else if (target.factionid != 0)
                    {
                        API.sendChatMessageToPlayer(player, "Cette personne ~r~a déjà ~s~une faction.");
                    }
                    else
                    {
                        target.factionid = objplayer.factionid;
                        target.rangfaction = 1;
                        API.sendChatMessageToPlayer(player, "Tu as invité ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~dans ta faction.");
                        API.sendChatMessageToPlayer(target.Handle, "Tu as ~g~rejoins ~s~une faction.");
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDeFact);
            }
        }

        [Command("virer", "~y~FACTION: ~s~/virer [ID/Nom]")]
        public void Virer(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.factionid != 0)
            {
                if (objplayer.rangfaction <= 4)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasLeader);
                }
                else
                {
                    if (target.factionid != objplayer.factionid)
                    {
                        API.sendChatMessageToPlayer(player, "Tu ne peux pas ~r~virer ~s~une personne qui ~r~n'est pas ~s~dans ta faction.");
                    }
                    else
                    {
                        if (player.position.DistanceTo(target.Handle.position) >= 2)
                        {
                            API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                        }
                        else
                        {
                            target.factionid = 0;
                            target.rangfaction = 0;
                            API.sendChatMessageToPlayer(player, "Tu as viré ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~de ta faction.");
                            API.sendChatMessageToPlayer(target.Handle, "Tu as été ~r~virer ~s~de ta faction.");
                        }
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDeFact);
            }
        }

        [Command("donnerrang", "~y~FACTION: ~s~/donnerrang [IdOuPartieDuNom] [Rang]")]
        public void Donnerrang(Client player, String idOrName, int rang)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.factionid != 0)
            {
                if (objplayer.rangfaction <= 5)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasLeader);
                }
                else
                {
                    if (target.factionid != objplayer.factionid)
                    {
                        API.sendChatMessageToPlayer(player, "Tu ne peux pas gérer le ~r~rang ~s~d'une personne qui ~r~n'est pas ~s~dans ta faction.");
                    }
                    else
                    {
                        if (rang <= 0 || rang >= 7)
                        {
                            API.sendChatMessageToPlayer(player, "Le rang doit être compris entre 1 et 6.");
                        }
                        else
                        {
                            target.rangfaction = rang;
                            API.sendChatMessageToPlayer(player, "Tu viens de passer ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~rang ~g~" + rang + " ~s~de ta faction.");
                            API.sendChatMessageToPlayer(target.Handle, "Tu as été promu rang ~g~" + rang + " ~s~de ta faction par ton chef.");
                        }
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDeFact);
            }
        }

        [Command("membres")]
        public void Membres(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.factionid != 0)
            {
                DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Utilisateur WHERE factionid = '" + objplayer.factionid + "'");
                if (result.Rows.Count != 0)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        string dscpdujoueur = Convert.ToString(result.Rows[i]["DescriptionFaction"]);
                        if (dscpdujoueur.Length >= 1) API.sendChatMessageToPlayer(player, String.Format("(R{0}) {1} : ({2})", Convert.ToInt32(result.Rows[i]["rangfaction"]), Convert.ToString(result.Rows[i]["PlayerName"]), Convert.ToString(result.Rows[i]["DescriptionFaction"])));
                        else API.sendChatMessageToPlayer(player, String.Format("(R{0}) {1}", Convert.ToInt32(result.Rows[i]["rangfaction"]), Convert.ToString(result.Rows[i]["PlayerName"])));
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDeFact);
            }
        }
        #endregion

        #region Script Armes
        [Command("placerplanquearme")]
        public void PlacerPlanqueArme(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.factionid == 0) API.sendChatMessageToPlayer(player, Constante.PasDeFact);
            else
            {
                if (objplayer.rangfaction <= 5) API.sendChatMessageToPlayer(player, Constante.PasLeader);
                else
                {
                    DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction WHERE factionid = '" + objplayer.factionid + "'");
                    if (result.Rows.Count != 0)
                    {
                        API.sendChatMessageToPlayer(player, "Ton organisation à déjà une planque d'armes.");
                        return;
                    }
                    else
                    {
                        if (objplayer.bank < Constante.PrixPlanqueArme) API.sendChatMessageToPlayer(player, Constante.PasAssezEnBanque);
                        else
                        {
                            Vector3 jpos = API.getEntityPosition(player);
                            API.exported.database.executeQuery("INSERT INTO PlanqueFaction VALUES ('', 'InteArmes', '" + objplayer.factionid + "', '" + jpos.X + "', '" + jpos.Y + "', '" + jpos.Z + "', '" + objplayer.PlayerName + "', '0', '0', '0', '0', '0')");
                            API.sendChatMessageToPlayer(player, "Tu viens d'acheter la planque de ton organisation, ~b~tu peux désormais commander des kits d'armes");
                            API.sendChatMessageToPlayer(player, "auprès d'un fournisseur, et te les faires livrer ici. La planque t'a coûté~b~ " + Constante.PrixPlanqueArme + "~s~$.");
                            int AncienBanque = objplayer.bank;
                            objplayer.bank = AncienBanque - Constante.PrixPlanqueArme;
                            var ShapeEntree = API.createSphereColShape(jpos, 2.0f);
                        }
                    }
                }
            }
        }

        [Command("lockplanque")]
        public void LockPlanque(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction WHERE factionid = '" + objplayer.factionid + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                    int Factionid = Convert.ToInt32(row["factionid"]);
                    int Ouvert = Convert.ToInt32(row["locked"]);
                    if (objplayer.rangfaction <= 5) API.sendChatMessageToPlayer(player, Constante.PasLeader);
                    else
                    {
                        if (Factionid != objplayer.factionid) API.sendChatMessageToPlayer(player, "Ton organisation ne possède pas de planque.");
                        else
                        {
                            if (player.position.DistanceTo(logpos) > 2) API.sendChatMessageToPlayer(player, "Tu n'es pas à côté de ta planque.");
                            else
                            {
                                if (Ouvert == 0)
                                {
                                    API.shared.exported.database.executeQuery("UPDATE PlanqueFaction SET locked ='" + 1 + "' WHERE factionid = '" + objplayer.factionid + "'");
                                    return;
                                }
                                else
                                {
                                    API.shared.exported.database.executeQuery("UPDATE PlanqueFaction SET locked ='" + 0 + "' WHERE factionid = '" + objplayer.factionid + "'");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            else API.sendChatMessageToPlayer(player, Constante.PasDeFact);
        }

        [Command("kitarme", "~y~FACTION: ~s~/kitarme [1 = Pistolet / 2 = P-Mitrailleur / 3 = Pompe / 4 = fusil] [quantité]", Alias = "ka")]
        public void KitArme(Client player, int Nombre, int Qtt)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction WHERE factionid = '" + objplayer.factionid + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    if (objplayer.IsOnPlanqueArmes == false) API.sendChatMessageToPlayer(player, "Tu ne peux pas trouver de kit d'armes ici.");
                    else
                    {
                        int KitPistoletFournisseur = Convert.ToInt32(row["kitpisto"]);
                        int KitPMitrFournisseur = Convert.ToInt32(row["kitpmitr"]);
                        int KitPompeFournisseur = Convert.ToInt32(row["kitpompe"]);
                        int KitFusilFournisseur = Convert.ToInt32(row["kitfusil"]);
                        if (Nombre == 0 || Nombre >= 5) API.sendChatMessageToPlayer(player, "Tu dois entrer un nombre valide! (1 = Pistolet, 2 = P-Mitrailleur, 3 = Pompe, 4 = fusil)");
                        if (Qtt == 0) API.sendChatMessageToPlayer(player, "Tu dois entrer une quantité valide!");
                        else
                        {
                            if (Nombre == 1)
                            {
                                if (Qtt > KitPistoletFournisseur) API.sendChatMessageToPlayer(player, "Il n'y a pas autant de kit dans la planque.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens de retirer~g~ " + Qtt + " ~s~kit pistolet de la planque.");
                                    int Item = Inventaire.KitPistolet;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, Qtt);
                                    int NouveauNombre = KitPistoletFournisseur - Qtt;
                                    API.shared.exported.database.executeQuery("UPDATE PlanqueFaction SET kitpisto ='" + NouveauNombre + "' WHERE factionid = '" + objplayer.factionid + "'");
                                    return;
                                }
                            }
                            if (Nombre == 2)
                            {
                                if (Qtt > KitPMitrFournisseur) API.sendChatMessageToPlayer(player, "Il n'y a pas autant de kit dans la planque.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens de retirer~g~ " + Qtt + " ~s~kit pistolet-mitrailleur de la planque.");
                                    int Item = Inventaire.KitPMitrailleur;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, Qtt);
                                    int NouveauNombrePM = KitPMitrFournisseur - Qtt;
                                    API.shared.exported.database.executeQuery("UPDATE PlanqueFaction SET locked ='" + NouveauNombrePM + "' WHERE factionid = '" + objplayer.factionid + "'");
                                    return;
                                }
                            }
                            if (Nombre == 3)
                            {
                                if (Qtt > KitPompeFournisseur) API.sendChatMessageToPlayer(player, "Il n'y a pas autant de kit dans la planque.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens de retirer~g~ " + Qtt + " ~s~kit fusil à pompe de la planque.");
                                    int Item = Inventaire.KitPompe;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, Qtt);
                                    int NouveauNombrePompe = KitPompeFournisseur - Qtt;
                                    API.shared.exported.database.executeQuery("UPDATE PlanqueFaction SET locked ='" + NouveauNombrePompe + "' WHERE factionid = '" + objplayer.factionid + "'");
                                    return;
                                }
                            }
                            if (Nombre == 4)
                            {
                                if (Qtt > KitFusilFournisseur) API.sendChatMessageToPlayer(player, "Il n'y a pas autant de kit dans la planque.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens de retirer~g~ " + Qtt + " ~s~kit fusil d'assaut de la planque.");
                                    int Item = Inventaire.KitFusil;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, Qtt);
                                    int NouveauNombreFusil = KitFusilFournisseur - Qtt;
                                    API.shared.exported.database.executeQuery("UPDATE PlanqueFaction SET locked ='" + NouveauNombreFusil + "' WHERE factionid = '" + objplayer.factionid + "'");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        [Command("vendrekit", "~y~FACTION: ~s~/vendrekit [Id/PartieDuNom] [type de kit (1/2/3/4)] [quantité] [prix]")]
        public void VendreKit(Client player, String idOrName, int Nombre, int Qtt, int Prix)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.factionid == 0) API.sendChatMessageToPlayer(player, Constante.PasDeFact);
            else
            {
                if (player.position.DistanceTo(target.Handle.position) >= 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                else
                {
                    if (Nombre == 0 || Nombre >= 5) API.sendChatMessageToPlayer(player, "Tu dois entrer un nombre valide! (1 = Pistolet, 2 = P-Mitrailleur, 3 = Pompe, 4 = fusil)");
                    else
                    {
                        if (Qtt == 0) API.sendChatMessageToPlayer(player, "Tu dois entrer une quantité valide!");
                        else
                        {
                            if (Nombre == 1)
                            {
                                if (Inventaire.GetItemNumberInBDD(player, 16) <= 0) API.sendChatMessageToPlayer(player, "Tu n'as pas de kit à vendre.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu proposes à cette personne~b~ " + Qtt + " ~b~kit de pistolet~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "Cette personne te propose~b~ " + Qtt + " ~b~kit de pistolet~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "(Tapes ~b~/accepter kit~s~ pour accepter l'offre.)");
                                    API.setEntityData(target.Handle, "TypeKit", 1);
                                    API.setEntityData(target.Handle, "PrixKit", Prix);
                                    API.setEntityData(target.Handle, "QuantitéKit", Qtt);
                                }
                            }

                            if (Nombre == 2)
                            {
                                if (Inventaire.GetItemNumberInBDD(player, 17) <= 0) API.sendChatMessageToPlayer(player, "Tu n'as pas de kit à vendre.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu proposes à cette personne~b~ " + Qtt + " ~b~kit de pistolet-mitrailleur~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "Cette personne te propose~b~ " + Qtt + " ~b~kit de pistolet-mitrailleur~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "(Tapes ~b~/accepter kit~s~ pour accepter l'offre.)");
                                    API.setEntityData(target.Handle, "TypeKit", 2);
                                    API.setEntityData(target.Handle, "PrixKit", Prix);
                                    API.setEntityData(target.Handle, "QuantitéKit", Qtt);
                                }
                            }

                            if (Nombre == 3)
                            {
                                if (Inventaire.GetItemNumberInBDD(player, 18) <= 0) API.sendChatMessageToPlayer(player, "Tu n'as pas de kit à vendre.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu proposes à cette personne~b~ " + Qtt + " ~b~kit de fusil à pompe~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "Cette personne te propose~b~ " + Qtt + " ~b~kit de fusil à pompe~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "(Tapes ~b~/accepter kit~s~ pour accepter l'offre.)");
                                    API.setEntityData(target.Handle, "TypeKit", 3);
                                    API.setEntityData(target.Handle, "PrixKit", Prix);
                                    API.setEntityData(target.Handle, "QuantitéKit", Qtt);
                                }
                            }

                            if (Nombre == 4)
                            {
                                if (Inventaire.GetItemNumberInBDD(player, 19) <= 0) API.sendChatMessageToPlayer(player, "Tu n'as pas de kit à vendre.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu proposes à cette personne~b~ " + Qtt + " ~b~kit de fusil d'assaut~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "Cette personne te propose~b~ " + Qtt + " ~b~kit de fusil d'assaut~s~ pour~g~ " + Prix + "~s~$.");
                                    API.sendChatMessageToPlayer(target.Handle, "(Tapes ~b~/accepter kit~s~ pour accepter l'offre.)");
                                    API.setEntityData(target.Handle, "TypeKit", 4);
                                    API.setEntityData(target.Handle, "PrixKit", Prix);
                                    API.setEntityData(target.Handle, "QuantitéKit", Qtt);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool IsEntreePlanque(Client player)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                    if (player.position.DistanceTo(logpos) < 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsSortiePlanque(Client player)
        {
            if (player.position.DistanceTo(InteArmes) < 2)
            {
                return true;
            }
            return false;
        }

        public void ColShapeEntree()
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                    var ShapeEntree = API.createSphereColShape(logpos, 2.0f);
                }
            }
        }
        #endregion

        #region Script Cannabis Evolué
        
        #endregion

        //convoyeur
        [Command("stopjob")]
        public void Stopjob(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (Fonction.IsPlayerInFaction(objplayer, "Gardien", false) && objplayer.IsJobDuty)
            {
                var jv = API.getPlayerVehicle(player);
                API.sendChatMessageToPlayer(player, "~g~Reviens au depot ! Un point est indiqué sur ton GPS ! (Touche 'w' au depot pour garer le camion)");
                objplayer.retourconv = true;
                API.setEntityData(jv, "retourcon", true);
                API.triggerClientEvent(player, "retourconv");
            }
        }
    }
}