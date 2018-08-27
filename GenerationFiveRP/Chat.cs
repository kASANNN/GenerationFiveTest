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
    public class Chat : Script
    {

        public Chat()
        {
            API.onChatMessage += OnPlayerChat;
        }

        public void OnPlayerChat(Client player, string message, CancelEventArgs e)
        {
            API.exported.database.executeQuery("INSERT INTO LogTextJoueur VALUE ('', 'Message', '" + player.name + "', '" + message + "')");
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if(objplayer.EnAppel == true)
            {
                API.sendChatMessageToPlayer(objplayer.Correspondant, "~b~Téléphone~s~: " + message);
            }
            Fonction.SendCloseMessage(player, 15.0f, "~#ffffff~", Fonction.RemoveUnderscore(player.name) + " dit: " + message);
            if (objplayer.addquestion == 1)
            {
                API.exported.database.executeQuery("INSERT INTO QuestionAutoEcole VALUES ('','" + message + "', '', '', '')");
                DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM QuestionAutoEcole WHERE Question='" + message + "'");
                foreach (DataRow row in result.Rows)
                {
                    objplayer.IDBDDquestion = Convert.ToInt32(row["ID"]);
                    objplayer.addquestion = 2;
                    API.sendChatMessageToPlayer(player, "[2/4] Entre maintenant la bonne réponse.");
                    e.Cancel = true;
                    return;
                }
            }
            if (objplayer.addquestion == 2)
            {
                API.exported.database.executeQuery("UPDATE QuestionAutoEcole SET BR='" + message + "' WHERE ID='" + objplayer.IDBDDquestion + "'");
                API.sendChatMessageToPlayer(player, "[3/4] Entre maintenant une mauvaise réponse.");
                objplayer.addquestion = 3;
                e.Cancel = true;
                return;
            }
            if (objplayer.addquestion == 3)
            {
                API.exported.database.executeQuery("UPDATE QuestionAutoEcole SET R2='" + message + "' WHERE ID='" + objplayer.IDBDDquestion + "'");
                API.sendChatMessageToPlayer(player, "[4/4] Entre maintenant une autre mauvaise réponse.");
                objplayer.addquestion = 4;
                e.Cancel = true;
                return;
            }
            if (objplayer.addquestion == 4)
            {
                API.exported.database.executeQuery("UPDATE QuestionAutoEcole SET R3='" + message + "' WHERE ID='" + objplayer.IDBDDquestion + "'");
                API.sendChatMessageToPlayer(player, "La question a bien été ajoutée.");
                objplayer.addquestion = 0;
                e.Cancel = true;
                return;
            }
            if (objplayer.DansQuestionnaire == true)
            {
                if (message == Convert.ToString(objplayer.BonneReponse))
                {
                    if (objplayer.QuestionEnCours != objplayer.OrdreQuestionAutoEcole.Count)
                    {
                        objplayer.QuestionEnCours += 1;
                        if (objplayer.QuestionEnCours != objplayer.OrdreQuestionAutoEcole.Count)
                        {
                            for(int p = 0; p < 10; p++)
                            {
                                API.sendChatMessageToPlayer(player, " ");
                            }
                            AutoEcole.LoadQuestion(player);
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            for (int p = 0; p < 10; p++)
                            {
                                API.sendChatMessageToPlayer(player, " ");
                            }
                            API.sendChatMessageToPlayer(player, "~g~Félicitation~s~, tu as ~g~réussi~s~ l'épreuve du ~b~code de la route~s~ !");
                            API.sendChatMessageToPlayer(player, "Tu peux maintenant ~g~passer~s~ l'épreuve de ~b~conduite~s~.");
                            objplayer.DansQuestionnaire = false;
                            objplayer.CodeDeLaRoute = true;
                            objplayer.OrdreQuestionAutoEcole.Clear();
                        }
                    }
                }
            }
            e.Cancel = true;
            return;
        }

        [Command("me", GreedyArg = true)]
        public void Command_me(Client player, string action)
        {
            Fonction.SendCloseMessage(player, 15.0f, Constante.VioletMe, Fonction.RemoveUnderscore(player.name) + " " + action);
        }

        [Command("l", GreedyArg = true)]
        public void Command_L(Client player, string message)
        {
            Fonction.SendCloseMessage(player, 15.0f, "~#ffffff~", Fonction.RemoveUnderscore(player.name) + " dit: " + message);
        }

        [Command("melow", GreedyArg = true)] // /me avec petit radius 
        public void Command_melow(Client player, string action)
        {
            Fonction.SendCloseMessage(player, 7.5f, Constante.VioletMe, Fonction.RemoveUnderscore(player.name) + " " + action);
        }

        [Command("do", GreedyArg = true)]
        public void Command_do(Client player, string action)
        {
            Fonction.SendCloseMessage(player, 15.0f, Constante.VioletMe, action + " " + "((" + Fonction.RemoveUnderscore(player.name) + "))");
        }

        [Command("o", GreedyArg = true)] //ooc
        public void Command_o(Client player, string message)
        {
            Fonction.SendCloseMessage(player, 15.0f, "~#ffffff~", Fonction.RemoveUnderscore(player.name) + " dit: " + "(( " + message + " ))");
        }

        [Command("chu", GreedyArg = true)] //chuchotter
        public void Command_chu(Client player, string message)
        {
            Fonction.SendCloseMessage(player, 7.5f, "~#ffffff~", Fonction.RemoveUnderscore(player.name) + " chuchotte: " + message);
        }

        [Command("s", GreedyArg = true)] //crier
        public void Command_s(Client player, string message)
        {
            Fonction.SendCloseMessage(player, 25.0f, "~#ffffff~", Fonction.RemoveUnderscore(player.name) + " crie: " + message + "!");
        }

        [Command("pm", GreedyArg = true)]
        public void Command_pm(Client player, String idOrName, string message)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            else
            {
                var msg_to_sender = "PM envoyé à " + Fonction.RemoveUnderscore(target.PlayerName) + ": " + message;
                API.sendChatMessageToPlayer(player, msg_to_sender);
                var msg = "PM reçu de " + Fonction.RemoveUnderscore(player.name) + ": " + message;
                API.sendChatMessageToPlayer(target.Handle, msg);
            }
            return;
        }

        [Command("veh")]//Parler en vehicule seulement
        public void Command_veh(Client player, string message)
        {
            if (player.isInVehicle)
            {
                var usersInCar = API.getVehicleOccupants(API.getPlayerVehicle(player));
                foreach (var joueurs in usersInCar)
                {
                    API.sendChatMessageToPlayer(player, "(Vehicule) " + Fonction.RemoveUnderscore(player.name) + ": " + message);

                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'es pas dans un véhicule!");
            }

        }

        [Command("t", GreedyArg = true)]//Chuchoter a une personne uniquement
        public void Command_t(Client player, String idOrName, String message)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            else
            {
                if (player.position.DistanceTo(API.getEntityPosition(target.Handle)) >= 2)
                {
                    API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                    return;
                }
                var msgsender = "Tu chuchotes à " + Fonction.RemoveUnderscore(target.PlayerName) + ": " + message;
                API.sendChatMessageToPlayer(player, msgsender);
                var msgdest = Fonction.RemoveUnderscore(player.name) + " te chuchote : " + message;
                API.sendChatMessageToPlayer(target.Handle, msgdest);
            }
            return;
        }

        [Command("(", GreedyArg = true)]
        public void Command_OocAdmin(Client player, string message)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 1)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                var msg = Fonction.RemoveUnderscore(player.name) + ": (( " + message + " ))";
                API.sendChatMessageToAll(msg);
            }
        }

        [Command("radio", Alias ="r", GreedyArg = true)]
        public static void Command_R(Client player, string message)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            FactionInfo objfaction = FactionInfo.GetFactionInfoById(objplayer.factionid);
            if (objfaction == null)
                API.shared.sendChatMessageToPlayer(player, Constante.PasDeFact);
            if(!objfaction.HasRadio)
                API.shared.sendChatMessageToPlayer(player, Constante.PasDeRadioFact);
            else
            {
                if (objplayer.IsFactionDuty == false)
                    API.shared.sendChatMessageToPlayer(player, Constante.PasEnService);
                else
                {
                    switch (objplayer.factionid)
                    {
                        case Constante.Faction_Police:
                            {
                                if (UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player) != null)
                                {
                                    UnitesLSPDInfo objUnite = UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player);
                                    var msgsender = "[Radio] " + Fonction.RemoveUnderscore(player.name) + " " + UnitesLSPDInfo.GetUniteName(objUnite.ID) + " dit: " + message;
                                    API.shared.sendChatMessageToPlayer(player, msgsender);
                                    List<Client> PlayerDuty = API.shared.getAllPlayers();
                                    foreach (Client target in PlayerDuty)
                                    {
                                        PlayerInfo objtarget = PlayerInfo.GetPlayerInfoObject(target);
                                        if (objtarget.factionid == Constante.Faction_Police && objtarget.IsFactionDuty == true && objtarget.Handle != player.handle)
                                        {
                                            var msgreceveur = "[Radio] " + Fonction.RemoveUnderscore(player.name) + " " + UnitesLSPDInfo.GetUniteName(objUnite.ID) + " : " + message;
                                            API.shared.sendChatMessageToPlayer(target, Constante.RadioFaction, msgreceveur);
                                        }
                                    }
                                }
                                else
                                {
                                    var msgsender = "[Radio]" + Fonction.RemoveUnderscore(player.name) + " dit: " + message;
                                    API.shared.sendChatMessageToPlayer(player, msgsender);
                                    List<Client> PlayerDuty = API.shared.getAllPlayers();
                                    foreach (Client target in PlayerDuty)
                                    {
                                        PlayerInfo objtarget = PlayerInfo.GetPlayerInfoObject(target);
                                        if (objtarget.factionid == Constante.Faction_Police && objtarget.IsFactionDuty == true)
                                        {
                                            var msgreceveur = Fonction.RemoveUnderscore(player.name) + ": " + message;
                                            API.shared.sendChatMessageToPlayer(target, Constante.RadioFaction, msgreceveur);
                                        }
                                    }
                                }
                                break;
                            }
                        case Constante.Faction_Gardien:
                            {
                                var msgsender = "[Radio]" + Fonction.RemoveUnderscore(player.name) + " dit: " + message;
                                API.shared.sendChatMessageToPlayer(player, msgsender);
                                List<Client> PlayerDuty = API.shared.getAllPlayers();
                                foreach (Client target in PlayerDuty)
                                {
                                    PlayerInfo objtarget = PlayerInfo.GetPlayerInfoObject(target);
                                    if (objtarget.factionid == Constante.Faction_Gardien && objtarget.IsFactionDuty == true)
                                    {
                                        var msgreceveur = Fonction.RemoveUnderscore(player.name) + ": " + message;
                                        API.shared.sendChatMessageToPlayer(target, Constante.RadioFaction, msgreceveur);
                                    }
                                }
                                break;
                            }
                    }
                }
            }
        }
    }
}
