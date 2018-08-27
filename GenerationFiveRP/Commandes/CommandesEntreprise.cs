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
    public class CommandesEntreprise : Script
    {
        #region Commandes Lead/Membre
        [Command("einvite", "~o~ENTREPRISE: ~s~/einvite [ID/Nom]")]
        public void Einvite(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.entrepriseid != 0)
            {
                if (objplayer.rangentreprise <= 2)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasLeader);
                }
                else
                {
                    if (player.position.DistanceTo(target.Handle.position) >= 2)
                    {
                        API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                    }
                    else if (target.entrepriseid != 0)
                    {
                        API.sendChatMessageToPlayer(player, "Cette personne ~r~est déjà ~s~dans une entreprise.");
                    }
                    else
                    {
                        target.entrepriseid = objplayer.entrepriseid;
                        target.rangentreprise = 1;
                        API.sendChatMessageToPlayer(player, "Tu as recruté ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~dans ton entreprise.");
                        API.sendChatMessageToPlayer(target.Handle, "Tu as ~g~rejoins ~s~une entreprise.");
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDent);
            }
        }

        [Command("evirer", "~o~ENTREPRISE: ~s~/evirer [ID/Nom]")]
        public void Evirer(Client player, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.entrepriseid != 0)
            {
                if (objplayer.rangentreprise <= 2)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasLeader);
                }
                else
                {
                    if (target.entrepriseid != objplayer.entrepriseid)
                    {
                        API.sendChatMessageToPlayer(player, "Tu ne peux pas ~r~virer ~s~une personne qui ~r~n'est pas ~s~dans ton entreprise.");
                    }
                    else
                    {
                        if (player.position.DistanceTo(target.Handle.position) >= 2)
                        {
                            API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                        }
                        else
                        {
                            target.entrepriseid = 0;
                            target.rangentreprise = 0;
                            API.sendChatMessageToPlayer(player, "Tu as viré ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~de ton entreprise.");
                            API.sendChatMessageToPlayer(target.Handle, "Tu as été ~r~virer ~s~de ton entreprise.");
                        }
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDent);
            }
        }

        [Command("edonnerrang", "~o~ENTREPRISE: ~s~/edonnerrang [IdOuPartieDuNom] [Rang]")]
        public void Edonnerrang(Client player, String idOrName, int rang)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objplayer.entrepriseid != 0)
            {
                if (objplayer.rangentreprise <= 3)
                {
                    API.sendChatMessageToPlayer(player, Constante.PasLeader);
                }
                else
                {
                    if (target.entrepriseid != objplayer.entrepriseid)
                    {
                        API.sendChatMessageToPlayer(player, "Tu ne peux pas gérer le ~r~rang ~s~d'une personne qui ~r~n'est pas ~s~dans ton entreprise.");
                    }
                    else
                    {
                        if (rang <= 0 || rang >= 5)
                        {
                            API.sendChatMessageToPlayer(player, "Le rang doit être compris entre 1 et 4.");
                        }
                        else
                        {
                            target.rangentreprise = rang;
                            API.sendChatMessageToPlayer(player, "Tu viens de passer ~b~" + Fonction.RemoveUnderscore(target.PlayerName) + " ~s~au rang ~g~" + rang + " ~s~de ton entreprise.");
                            API.sendChatMessageToPlayer(target.Handle, "Tu as été promu au rang ~g~" + rang + " ~s~de ton entreprise par ton chef.");
                        }
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDent);
            }
        }

        [Command("emembres")]
        public void Emembres(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.entrepriseid != 0)
            {
                DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Utilisateur WHERE entrepriseid = '" + objplayer.entrepriseid + "'");
                if (result.Rows.Count != 0)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        API.sendChatMessageToPlayer(player, String.Format("(R{0}) {1}", Convert.ToInt32(result.Rows[i]["rangentreprise"]), Convert.ToString(result.Rows[i]["PlayerName"])));
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, Constante.PasDent);
            }
        }
        #endregion

        #region Script Mécano
        [Command("reparer", "~o~ENTREPRISE: ~s~/reparer [IdDuVehicule]")]
        public void Reparer(Client player, String id)
        {
            int idveh = Int32.Parse(id);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(idveh);
            if (player.position.DistanceTo(objveh.handle.position) > 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
            else
            {
                API.repairVehicle(objveh.handle);
                API.sendChatMessageToPlayer(player, "Tu viens de ~b~réparer ~s~ce véhicule.");
                var PayeEnAttente = objplayer.pendingpaye;
                objplayer.pendingpaye = PayeEnAttente + Constante.PayeReparer;
            }
        }

        [Command("changercouleur", "~o~ENTREPRISE: ~s~/ccouleur [IdDuVehicule] [Couleur1] [Couleur2]", Alias = "ccouleur")]
        public void Changercouleur(Client player, String id, int couleur1, int couleur2)
        {
            int idveh = Int32.Parse(id);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(idveh);
            if (player.position.DistanceTo(objveh.handle.position) > 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
            else
            {
                if ((couleur1 < 0 || couleur1 > 159) && couleur1 != -1)
                {
                    API.sendChatMessageToPlayer(player, "La couleur principale doit être comprise entre 0 et 159.");
                }
                if ((couleur2 < 0 || couleur2 > 159) && couleur2 != -1)
                {
                    API.sendChatMessageToPlayer(player, "La couleur secondaire doit être comprise entre 0 et 159.");
                }
                if (couleur1 == -1 && couleur2 == -1) return;
                if (couleur1 != -1)
                {
                    objveh.color1 = couleur1;
                }
                if (couleur2 != -1)
                {
                    objveh.color2 = couleur2;
                }
                API.sendChatMessageToPlayer(player, "Tu as repeint le véhicule en couleur : ~b~" + couleur1 + " ~s~et ~b~" + couleur2 + "~s~.");
                var PayeEnAttente = objplayer.pendingpaye;
                objplayer.pendingpaye = PayeEnAttente + Constante.PayeCouleur;
            }
        }

        [Command("plein", "~o~ENTREPRISE: ~s~/plein [IdDuVehicule] [NbDeLitres]")]
        public void Plein(Client player, String id, int Litres)
        {
            int idveh = Int32.Parse(id);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(idveh);
            if (player.position.DistanceTo(objveh.handle.position) > 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
            else
            {
                VehiculeInfo.SetVehiculeEssence(objveh, Litres);
                API.sendChatMessageToPlayer(player, "Tu viens de mettre ~b~" + Litres + " ~s~dans le réservoir.");
                var PayeEnAttente = objplayer.pendingpaye;
                objplayer.pendingpaye = PayeEnAttente + Litres;
            }
        }
        #endregion

        #region Script Taxi
        [Command("Taxi", "~o~ENTREPRISE: ~s~/taxi [on/off/prendrecourse]")]
        public void Taxi(Client player, String option)
        {
            if (option == "on")
            {
                API.sendChatMessageToPlayer(player, "Tu viens de ~g~commencer ~s~à travailler en temps que taximan.");
                API.setEntityData(player, "taximan", "true");
            }
            if (option == "off")
            {
                API.sendChatMessageToPlayer(player, "Tu viens de ~r~terminer ~s~ton travail de taximan.");
                API.setEntityData(player, "taximan", "false");
            }
            if (option == "prendrecourse")
            {
                API.sendChatMessageToPlayer(player, "Tu viens de prendre la course, rends toi aux coordonnées GPS.");
            }
        }
        #endregion
    }
}