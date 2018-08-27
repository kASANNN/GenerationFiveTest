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
using System.Threading.Tasks;

namespace GenerationFiveRP
{
    public class CommandesPolice : Script
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

        [Command("creerunite", "~b~POLICE:~s~ /creerunite")]
        public void Creerpatrouille(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            if (objplayer.IsFactionDuty == false)
            {
                API.sendChatMessageToPlayer(player, Constante.PasEnService);
                return;
            }
            if(UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player) != null)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu es déjà dans une unité.");
                return;
            }
            if(!player.isInVehicle || VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle.handle).factionid != Constante.Faction_Police)
            {
                API.sendChatMessageToPlayer(player, "~r~Il faut etre dans un véhicule pour creer une unité.");
            }
            else
            {
                UnitesLSPDInfo Uniteobj = new UnitesLSPDInfo(player, player.vehicle);
                API.sendChatMessageToPlayer(player, "Numéro d'unité : ~g~" + (Uniteobj.ID + 1) + "~s~ | Numéro du véhicule : ~g~" + VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle.handle).ID);
                return;
            }
        }

        [Command("quitterunite", "~b~POLICE:~s~ /quitterunite")]
        public void QuitterUnite(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            else if (objplayer.IsFactionDuty == false)
            {
                API.sendChatMessageToPlayer(player, Constante.PasEnService);
                return;
            }
            else if(UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player) == null)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'es pas dans une unité.");
                return;
            }
            else
            {
                UnitesLSPDInfo objUnite = UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player);
                if(objUnite.Membres.Count == 1)
                {
                    UnitesLSPDInfo.UnitesList.Remove(objUnite);
                    API.sendChatMessageToPlayer(player, "~g~Tu viens de quitter ton unité.");
                    return;
                }
                else
                {
                    objUnite.Membres.Remove(player);
                    API.sendChatMessageToPlayer(player, "~g~Tu viens de quitter ton unité.");
                    return;
                }
            }
        }

        [Command("rejoindreunite", "~b~POLICE:~s~ /rejoindreunite [NuméroUnité]")]
        public void RejoindreUnite(Client player, int NumeroUnite)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            if (objplayer.IsFactionDuty == false)
            {
                API.sendChatMessageToPlayer(player, Constante.PasEnService);
                return;
            }
            if (UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player) != null)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu es déjà dans une unité.");
                return;
            }
            UnitesLSPDInfo objUnite = UnitesLSPDInfo.GetUniteLSPDInfoByID(NumeroUnite - 1);
            if (objUnite == null)
            {
                API.sendChatMessageToPlayer(player, "~r~L'unité demandée n'éxiste pas.");
                return;
            }
            if(objUnite.Membres.Count == 3)
            {
                API.sendChatMessageToPlayer(player, "~r~L'unité demandée est complète.");
                return;
            }
            else
            {
                objUnite.Membres.Add(player);
                for(int i = 0; i < objUnite.Membres.Count; i++)
                {
                    if(objUnite.Membres[i] != player)
                    {
                        API.sendNotificationToPlayer(objUnite.Membres[i], "~g~" + Fonction.RemoveUnderscore(player.name) + "~s~ a rejoint ton unité.");
                    }
                }
            }
        }

        [Command("menotter", "~b~POLICE:~s~ /menotter [Id/PartieDuNom]")]
        public void Menotter(Client player, String idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            else
            {
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                    return;
                if (player.position.DistanceTo(API.getEntityPosition(target.Handle)) >= 2)
                {
                    API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                    return;
                }
                else if (objplayer.IsFactionDuty == false)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasEnService);
                    return;
                }
                else if (target.IsMenotter == true)
                {
                    API.sendChatMessageToPlayer(player, "Tu ne peux pas ~r~menotter ~s~une personne qui l'est déjà.");
                    return;
                }
                API.setPlayerClothes(target.Handle, 7, (target.sexe == 0 ? 41 : 25), 0);
                API.sendChatMessageToPlayer(player, "Tu viens de ~g~menotter ~s~cette personne.");
                API.sendChatMessageToPlayer(target.Handle, "Tu viens d'être ~r~menotté ~s~par un policier.");
                target.IsMenotter = true;
                API.playPlayerAnimation(target.Handle, (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), "mp_arresting", "idle");
            }
            return;
        }

        [Command("demenotter", "~b~POLICE:~s~ /demenotter [Id/PartieDuNom]")]
        public void DeMenotter(Client player, String idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            else
            {
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                    return;
                if (player.position.DistanceTo(API.getEntityPosition(target.Handle)) >= 2)
                {
                    API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                    return;
                }
                else if (objplayer.IsFactionDuty == false)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasEnService);
                    return;
                }
                else if (target.IsMenotter == false)
                {
                    API.sendChatMessageToPlayer(player, "Tu ne peux pas ~r~démenotter ~s~une personne qui n'est pas menottée.");
                    return;
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "Tu viens de ~g~démenotter ~s~cette personne.");
                    API.sendChatMessageToPlayer(target.Handle, "Tu viens d'être ~g~démenotté ~s~par un policier.");
                    target.IsMenotter = false;
                    API.setPlayerClothes(target.Handle, 7, 0, 0);
                    API.stopPlayerAnimation(target.Handle);
                }
            }
            return;
        }

        [Command("Megaphone", Alias = "m", GreedyArg = true)]
        public void Megaphone(Client player, string message)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            if (!objplayer.IsFactionDuty)
            {
                API.sendChatMessageToPlayer(player, Constante.PasEnService);
                return;
            }
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle);
            if (!player.isInVehicle)
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas de ~r~mégaphone ~s~sur toi.");
                return;
            }
            if (objveh.factionid != Constante.Faction_Police)
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas de ~r~mégaphone ~s~dans ton véhicule.");
            }
            else
            {
                Fonction.SendCloseMessage(player, 40.0f, Constante.JauneMegaphone, Fonction.RemoveUnderscore(player.name) + " dit au mégaphone : " + message);
            }
        }

        [Command("b")]
        public void Balise(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            objplayer.BaliseLSPD = true;
            List<PlayerInfo> Joueurs = PlayerInfo.PlayerList;
            List<Client> JoueursDuty = new List<Client>();
            foreach (PlayerInfo joueur in Joueurs)
            {
                if (joueur.factionid == Constante.Faction_Police && joueur.IsFactionDuty == true)
                {
                    JoueursDuty.Add(joueur.Handle);
                    var msgreceveur = Fonction.RemoveUnderscore(player.name) + " dit: Besoin de renfort sur notre balise GPS.";
                    API.sendChatMessageToPlayer(joueur.Handle, Constante.RadioFaction, msgreceveur);
                }
            }
            Fonction.SendCloseMessage(player, 15.0f, Constante.VioletMe, Fonction.RemoveUnderscore(player.name) + " utilise sa balise GPS.");
            foreach (Client TargetDuty in JoueursDuty)
            {
                API.sendChatMessageToPlayer(player, "test 1");
                API.triggerClientEvent(player, "BaliseLSPD", player.position.X, player.position.Y, player.position.Z, String.Format("Balise{0}", player.name));
            }
            while (objplayer.BaliseLSPD == true)
            {
                System.Threading.Thread.Sleep(2000);
                foreach (Client TargetDuty in JoueursDuty)
                {
                    API.sendChatMessageToPlayer(player, "test 2");
                    API.triggerClientEvent(TargetDuty, "DeleteBaliseLSPD", String.Format("Balise{0}", player.name));
                    API.triggerClientEvent(player, "BaliseLSPD", player.position.X, player.position.Y, player.position.Z, String.Format("Balise{0}", player.name));
                }
            }  
        }

        [Command("bc")]
        public void BaliseCoupee(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            objplayer.BaliseLSPD = false;
            List<Client> PlayerDuty = new List<Client>();
            foreach (Client target in API.getAllPlayers())
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfoObject(target);
                if (objtarget.factionid == Constante.Faction_Police && objtarget.IsFactionDuty == true)
                {
                    PlayerDuty.Add(target);
                    var msgreceveur = Fonction.RemoveUnderscore(player.name) + " dit: Demande de renfort annulée.";
                    API.sendChatMessageToPlayer(target, Constante.RadioFaction, msgreceveur);
                }
            }
            foreach(Client TargetDuty in PlayerDuty)
            {
                System.Threading.Thread.Sleep(5000);
                API.triggerClientEvent(TargetDuty, "DeleteBaliseLSPD", String.Format("Balise{0}", player.name));
            }
            Fonction.SendCloseMessage(player, 15.0f, Constante.VioletMe, Fonction.RemoveUnderscore(player.name) + " utilise sa balise GPS.");
        }

        [Command("amende", "~b~POLICE:~s~ /amende [Id/PartieDuNom] [Montant] [Raison]", GreedyArg = true)]
        public void Amende(Client player, String idOrName, int montant, string raison)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            if (!objplayer.IsFactionDuty)
            {
                API.sendChatMessageToPlayer(player, Constante.PasEnService);
                return;
            }
            API.sendChatMessageToPlayer(player, "Tu viens de donner une amende de ~g~" + montant + "~s~$ à " + Fonction.RemoveUnderscore(objtarget.PlayerName));
            API.sendChatMessageToPlayer(objtarget.Handle, "Tu viens de recevoir une amende de ~r~" + montant + "~s~$ par " + Fonction.RemoveUnderscore(objplayer.PlayerName) + " pour " + raison);

            int date;
            Int32.TryParse(DateTime.Now.ToString("ddMMyyyyhhmmss"), out date);
            AmendeInfo amende = new AmendeInfo(objtarget.Handle, montant, raison, objplayer.PlayerName, date);

            if (objtarget.money >= montant)
            {
                API.sendChatMessageToPlayer(objtarget.Handle, "Tu possèdes assez d'argent sur toi pour la payer maintenant ~m~(/payeramende " + amende.id + ")~s~.");
                API.sendChatMessageToPlayer(objtarget.Handle, "Pour la payer plus tard au poste tu peux. Si tu attends trop, elle sera prélevée sur ton salaire.");
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(objtarget.Handle, "Rends toi au poste dès que possible pour régler ton amende. Sinon, elle sera prélevée sur ton salaire.");
                API.sendChatMessageToPlayer(objtarget.Handle, "Id de ton amende : "+ amende.id + ".");
            }
            API.exported.database.executeQuery("INSERT INTO UtilisateurAmende SET playername = '" + objtarget.PlayerName + "', montant = " + amende.montant + ", raison='"+amende.raison+"', auteur='"+amende.auteur+"', date="+amende.date);
        }

        [Command("verifiervehicule", "/verifiervehicule [IdDuVeh]")]
        public void Verifiervehicule(Client player, String id)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            if (!objplayer.IsFactionDuty)
            {
                API.sendChatMessageToPlayer(player, Constante.PasEnService);
                return;
            }
            int idveh = Int32.Parse(id);
            {
                VehiculeInfo objvehicule = VehiculeInfo.GetVehicleInfoById(idveh);
                API.sendChatMessageToPlayer(player, "Le véhicule appartient à");
            }
        }

        [Command("emprisonner", "/emprisonner [Id/PartieDuNom] [temps en minutes]")]
        public void Emprisonner_CMD(Client player, String idOrName, int temps)
        {            
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null)
            {
                API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
                return;
            }

            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Fonction.IsPlayerInFaction(objplayer, "Police", true))
                return;
            if (!objplayer.IsFactionDuty)
            {
                API.sendChatMessageToPlayer(player, Constante.PasEnService);
                return;
            }
            if (player.position.DistanceTo(Constante.Pos_EntrerPrison) < 4 && target.Handle.position.DistanceTo(Constante.Pos_EntrerPrison) < 4 )
            {
                target.position = Constante.Pos_SortiePrison;
                API.setEntityPosition(target.Handle, Constante.Pos_SortiePrison);
                API.sendChatMessageToPlayer(target.Handle, "Tu viens d'être emprisonné pour "+ temps+" minutes");
            }
        }
    }
}