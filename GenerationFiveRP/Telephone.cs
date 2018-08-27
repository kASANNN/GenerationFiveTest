using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Data;
using System;

namespace GenerationFiveRP
{
    public class Telephone : Script
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

        // Commande Puce

        [Command("puceliste")]
        public void Puceliste(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (GetNumberOfPucePlayer(player) != 0)
            {
                DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE IDJoueur = '" + objplayer.dbid + "'");
                if (result.Rows.Count != 0)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(result.Rows[i]["Active"]) == true)
                        {
                            API.sendChatMessageToPlayer(player, String.Format("Puce n°{0}: 555-{1} ~g~activée~s~.", i + 1, Convert.ToInt32(result.Rows[i]["Numero"])));
                        }
                        else
                        {
                            API.sendChatMessageToPlayer(player, String.Format("Puce n°{0}: 555-{1} ~r~désactivée~s~.", i + 1, Convert.ToInt32(result.Rows[i]["Numero"])));
                        }
                    }
                }

            }
        }

        [Command("mettrepuce", "~y~UTILISATION: ~w~/mettrepuce [Numéro de telephone de la puce]")]
        public void Mettrepuce(Client player, int NumeroPuce)
        {
            if(!PlayerHaveTelephone(player))
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas de ~r~téléphone ~s~sur toi.");
                return;
            }
            if(!PlayerHaveThisPuce(player, NumeroPuce))
            {
                API.sendChatMessageToPlayer(player, "Tu ne possède pas de ~r~puce avec ce numéro de téléphone~s~.");
                return;
            }
            else
            {
                int AnciennePuceActive = GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player));
                if(AnciennePuceActive == -1)
                {
                    SetPuceInTelephone(player, NumeroPuce);
                    API.sendChatMessageToPlayer(player, String.Format("La puce ~g~555-{0} ~s~a été mise dans ton téléphone.", NumeroPuce));
                }
                else
                {
                    RemovePuceOnTelephone(player, GetPuceNumber(AnciennePuceActive));
                    SetPuceInTelephone(player, NumeroPuce);
                    API.sendChatMessageToPlayer(player, String.Format("La puce ~g~555-{0} ~s~a été mise à la place de la ~r~555-{1} ~s~dans ton téléphone.", NumeroPuce, GetPuceNumber(AnciennePuceActive)));
                }
            }
        }

        [Command("enleverpuce")]
        public void Enlerverpuce(Client player)
        {
            if(!PlayerHaveTelephone(player))
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas de ~r~téléphone ~s~sur toi.");
                return;
            }
            if(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player)) == -1)
            {
                API.sendChatMessageToPlayer(player, "Il n'y a pas de ~r~puce ~s~dans ton téléphone.");
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, String.Format("Tu viens de retirer la puce ~r~555-{0} ~s~de ton téléphone.", GetPuceNumber(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player)))));
                RemovePuceOnTelephone(player, GetPuceNumber(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player))));
            }
        }

        [Command("detruirepuce", "~y~UTILISATION: ~w~/detruirepuce [Numéro de telephone de la puce]")]
        public void Detruirepuce(Client player, int NumeroPuce)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if(GetNumberOwner(NumeroPuce) == objplayer.dbid)
            {
                if(PuceIsActived(NumeroPuce))
                {
                    RemovePuceOnTelephone(player, NumeroPuce);
                    DestroyPuce(GetPuceIDBDD(NumeroPuce));
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de detruire la ~g~puce ~s~avec le numéro ~b~{0}.", NumeroPuce));
                    return;
                }
                else
                {
                    DestroyPuce(GetPuceIDBDD(NumeroPuce));
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de detruire la ~g~puce ~s~avec le numéro ~b~{0}.", NumeroPuce));
                    return;
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas cette ~r~puce ~s~sur toi.");
            }
        }

        [Command("donnerpuce", "~y~UTILISATION: ~w~/donnerpuce [id/PartieDuNom] [Numéro de telephone de la puce]")]
        public void Donnerpuce(Client player, String idOrName, int NumeroPuce)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            else
            {
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                if (GetNumberOwner(NumeroPuce) != objplayer.dbid)
                {
                    API.sendChatMessageToPlayer(player, "Tu n'as pas cette ~r~puce ~s~sur toi.");
                    return;
                }
                if (GetNumberOfPucePlayer(target.Handle) >= 3)
                {
                    API.sendChatMessageToPlayer(player, "Le joueur a déjà 3 ~r~puces ~s~sur lui.");
                    return;
                }
                if (PuceIsActived(NumeroPuce))
                {
                    RemovePuceOnTelephone(player, NumeroPuce);
                    ChangePuceOwner(GetPuceIDBDD(NumeroPuce), target.Handle);
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de donner la ~g~puce ~s~avec le numéro ~b~{0}.", NumeroPuce));
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de recevoir la ~g~puce ~s~avec le numéro ~b~{0}.", NumeroPuce));
                }
                else
                {
                    ChangePuceOwner(GetPuceIDBDD(NumeroPuce), target.Handle);
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de donner la ~g~puce ~s~avec le numéro ~b~{0}.", NumeroPuce));
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de recevoir la ~g~puce ~s~avec le numéro ~b~{0}.", NumeroPuce));
                }                
            }
            return;
        }

        // Commande Téléphone

        [Command("appeler", "~y~UTILISATION: ~w~/appeler [Numéro]")]
        public void Appeler(Client player, int Numero)
        {
            if (!PlayerHaveTelephone(player))
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas de ~r~téléphone ~s~sur toi.");
                return;
            }
            if(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player)) != -1)
            {
                {
                    PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                    DataTable resultfournisseur = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction WHERE factionid = '" + objplayer.factionid + "'");
                    if (Numero == 15937)
                    {
                        if (resultfournisseur.Rows.Count != 0)
                        {
                            if (objplayer.rangfaction == 6)
                            {
                                if (objplayer.TimerKitArmes <= 0)
                                {
                                    API.sendChatMessageToPlayer(player, "~b~Fournisseur~s~: Ouais j'écoute, t'as besoin de quoi?");
                                    API.triggerClientEvent(player, "MenuFournisseur");
                                    NetHandle objecttel = API.createObject(-1038739674, player.position, player.rotation, player.dimension);
                                    API.attachEntityToEntity(objecttel, player.handle, "PH_R_Hand", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
                                    objplayer.ObjTel = objecttel;
                                    API.delay(10000, true, () =>
                                    {
                                        API.stopPlayerAnimation(player);
                                        API.deleteEntity(objplayer.ObjTel);
                                    });
                                    return;
                                }
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "La personne ~r~ne répond pas~s~ à ton appel.");
                                    return;
                                }
                            }
                        }
                    }
                    int idplayer = GetNumberOwner(Numero);
                    if (idplayer == -1)
                    {
                        API.sendChatMessageToPlayer(player, "Ce ~r~numéro ~s~n'existe pas.");
                        return;
                    }
                    if (!PuceIsActived(Numero))
                    {
                        API.sendChatMessageToPlayer(player, "La personne ~r~ne répond pas~s~ à ton appel.");
                        return;
                    }
                    else
                    {
                        PlayerInfo Eobjplayer = PlayerInfo.GetPlayerInfoObject(player);
                        PlayerInfo Robjplayer = PlayerInfo.GetPlayerInfoByIdBDD(idplayer);
                        NetHandle objecttel = API.createObject(-1038739674, player.position, player.rotation, player.dimension);
                        API.attachEntityToEntity(objecttel, player.handle, "PH_R_Hand", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
                        Eobjplayer.ObjTel = objecttel;
                        API.playPlayerAnimation(player, (int)(AnimationFlags.StopOnLastFrame | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), (Eobjplayer.sexe == 0 ? "cellphone@" : "cellphone@female"), "cellphone_call_in");
                        Eobjplayer.eappel = true;
                        Robjplayer.rappel = true;
                        Eobjplayer.Correspondant = Robjplayer.Handle;
                        Robjplayer.NumeroAppelRecu = GetPuceNumber(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player)));
                        API.sendChatMessageToPlayer(Eobjplayer.Handle, "Le téléphone est en train de sonner.");
                        API.sendChatMessageToPlayer(Robjplayer.Handle, String.Format("Ton téléphone sonne (Numéro affiché : ~b~555-{0}~s~) ~g~/dec pour décrocher~s~.", Robjplayer.NumeroAppelRecu));
                        return;
                    }
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas de ~r~puce ~s~dans téléphone.");
            }
        }

        [Command("dec")]
        public void Dec(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.rappel == true)
            {
                int Eappel = GetNumberOwner(objplayer.NumeroAppelRecu);
                PlayerInfo Eobjplayer = PlayerInfo.GetPlayerInfoByIdBDD(Eappel);
                objplayer.EnAppel = true;
                objplayer.rappel = false;
                objplayer.Correspondant = Eobjplayer.Handle;
                Eobjplayer.EnAppel = true;
                Eobjplayer.eappel = false;
                Eobjplayer.Correspondant = objplayer.Handle;
                NetHandle objecttel = API.createObject(-1038739674, player.position, player.rotation, player.dimension);
                API.attachEntityToEntity(objecttel, player.handle, "PH_R_Hand", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
                objplayer.ObjTel = objecttel;
                API.playPlayerAnimation(player, (int)(AnimationFlags.StopOnLastFrame | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), (objplayer.sexe == 0 ? "cellphone@" : "cellphone@female"), "cellphone_call_in");
                API.sendChatMessageToPlayer(player, "Tu ~g~réponds ~s~à l'appel.");
                API.sendChatMessageToPlayer(Eobjplayer.Handle, "Ton correspondant ~g~vient de décrocher~s~.");
                Eobjplayer.TimeDebutAppel = DateTime.Now;
                /*while (objplayer.EnAppel == true)
                {
                    API.delay(1000, true, () =>
                    {
                        SetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player)), GetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player))) - 1);
                        SetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(Eobjplayer.Handle)), GetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(Eobjplayer.Handle))) - 1);
                        if (GetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player))) == 0)
                        {
                            API.sendChatMessageToPlayer(player, String.Format("Appel interrompu, tu n'as plus de ~r~crédits ~s~sur le numéro ~b~{0}~s~.", GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player))));
                            API.sendChatMessageToPlayer(Eobjplayer.Handle, "Ton correspondant vient de ~r~racrocher~s~.");
                            objplayer.EnAppel = false;
                            Eobjplayer.EnAppel = false;
                        }
                        if (GetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(Eobjplayer.Handle))) == 0)
                        {
                            API.sendChatMessageToPlayer(Eobjplayer.Handle, String.Format("Appel interrompu, tu n'as plus de ~r~crédits ~s~sur le numéro ~b~{0}~s~.", GetPuceIDActive(GetTelephoneIDBDDFromPlayer(Eobjplayer.Handle))));
                            API.sendChatMessageToPlayer(player, "Ton correspondant vient de ~r~racrocher~s~.");
                            objplayer.EnAppel = false;
                            Eobjplayer.EnAppel = false;
                        }
                    });
                }*/
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Personne ~s~ne t'appelle actuellement.");
            }
        }

        [Command("rac")]
        public void Rac(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.eappel == true)
            {
                PlayerInfo robjplayer = PlayerInfo.GetPlayerInfoObject(objplayer.Correspondant);
                API.stopPlayerAnimation(player);
                API.deleteEntity(objplayer.ObjTel);
                robjplayer.rappel = false;
                objplayer.eappel = false;
                API.sendChatMessageToPlayer(player, "Tu viens de ~r~racrocher~s~.");
                return;
            }
            if (objplayer.EnAppel == true)
            {
                int Eappel = GetNumberOwner(objplayer.NumeroAppelRecu);
                PlayerInfo Eobjplayer = PlayerInfo.GetPlayerInfoByIdBDD(Eappel);
                objplayer.EnAppel = false;
                objplayer.Correspondant = Eobjplayer.Handle;
                Eobjplayer.EnAppel = false;
                Eobjplayer.Correspondant = objplayer.Handle;
                API.stopPlayerAnimation(player);
                API.stopPlayerAnimation(Eobjplayer.Handle);
                API.deleteEntity(objplayer.ObjTel);
                API.deleteEntity(Eobjplayer.ObjTel);
                API.sendChatMessageToPlayer(player, "Tu viens de ~r~racrocher~s~.");
                API.sendChatMessageToPlayer(Eobjplayer.Handle, "Ton correspondant vient de ~r~racrocher~s~.");
                DateTime TimeRacAppel = DateTime.Now;
                TimeSpan Interval = TimeRacAppel - Eobjplayer.TimeDebutAppel;
                int ForfaitRestant = GetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(Eobjplayer.Handle)));
                int NewForfaitRestant = ForfaitRestant - Interval.Seconds;
                if (NewForfaitRestant < 0) SetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(Eobjplayer.Handle)), 0);
                else SetTempsAppelRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(Eobjplayer.Handle)), NewForfaitRestant);
                return;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "Tu n'es pas en ~r~communication actuellement~s~.");
            }
        }

        [Command("sms", GreedyArg = true)]
        public void Sms(Client player, int Numero, string Message)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if(!PlayerHaveTelephone(player))
            {
                API.sendChatMessageToPlayer(player, "Tu n'as pas de ~r~téléphone ~s~sur toi.");
            }
            if(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player)) != -1)
            {
                int idplayer = GetNumberOwner(Numero);
                if (idplayer == -1)
                {
                    API.sendChatMessageToPlayer(player, "Ce ~r~numéro ~s~n'existe pas.");
                    return;
                }
                else
                {
                    NetHandle objecttel = API.createObject(-1038739674, player.position, player.rotation, player.dimension);
                    API.attachEntityToEntity(objecttel, player.handle, "PH_R_Hand", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
                    API.playPlayerAnimation(player, (int)(AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), (objplayer.sexe == 0 ? "cellphone@" : "cellphone@female"), "cellphone_text_in");
                    API.delay(1500, true, () =>
                    {
                        API.deleteEntity(objecttel);
                    });
                    if (GetNombreSmsRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player))) == 0)
                    {
                        API.sendChatMessageToPlayer(player, "Tu n'as plus de ~r~crédits SMS~s~.");
                        return;
                    }
                    else
                    {
                        SetNombreSmsRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player)), GetNombreSmsRestant(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player))) - 1);
                        API.sendChatMessageToPlayer(player, "Ton message a bien été ~g~envoyé~s~.");
                        PlayerInfo robjplayer = PlayerInfo.GetPlayerInfoByIdBDD(GetNumberOwner(Numero));
                        if (robjplayer != null)
                        {
                            API.playPlayerAnimation(player, (int)(AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), (objplayer.sexe == 0 ? "cellphone@" : "cellphone@female"), "cellphone_text_in");
                            API.delay(1500, true, () =>
                            {
                                API.deleteEntity(objecttel);
                            });
                            API.sendChatMessageToPlayer(robjplayer.Handle, String.Format("Message reçu du ~b~555-{0} ~s~: {1}", GetPuceNumber(GetPuceIDActive(GetTelephoneIDBDDFromPlayer(player))), Message));
                            return;
                        }
                    }
                }
            }
        }

        // Telephone

        public static int GetTelephoneIDBDDFromPlayer(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Telephones WHERE IDJoueur = '" + objplayer.dbid + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["ID"]);
                }
            }
            return 0;
        }

        public static bool PlayerHaveTelephone(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Telephones WHERE IDJoueur = '" + objplayer.dbid + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static void AddTelephoneToPlayer(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            API.shared.exported.database.executeQuery("INSERT INTO Telephones VALUES ('','" + objplayer.dbid + "','-1')");
        }

        public static void RemoveTelephoneToPlayer(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            API.shared.exported.database.executeQuery("DELETE * FROM Telephones WHERE IDJoueur ='" + objplayer.dbid + "'");
        }

        // Puce

        public static int GetNumberOfPucePlayer(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE IDJoueur = '" + objplayer.dbid + "'");
            if (result.Rows.Count != 0)
            {
                return result.Rows.Count;
            }
            return 0;
        }

        public static int GetPuceIDBDD(int NumeroPuce)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE Numero = '" + NumeroPuce + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["ID"]);
                }
            }
            return -1;
        }

        public static int GetNumberOwner(int NumeroPuce)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE Numero = '" + NumeroPuce + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["IDJoueur"]);
                }
            }
            return -1;
        }

        public static int GetPuceNumber(int IDPuce)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE ID = '" + IDPuce + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["Numero"]);
                }
            }
            return -1;
        }

        public static int GetPuceIDActive(int IDTelephone)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Telephones WHERE ID = '" + IDTelephone + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["Puce"]);
                }
            }
            return -1;
        }

        public static bool PlayerHaveThisPuce(Client player, int Numero)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE IDJoueur = '" + objplayer.dbid + "' AND Numero = '"+ Numero +"'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static bool PuceIsActived(int NumeroPuce)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE Numero = '" + NumeroPuce + "'");
            foreach (DataRow row in result.Rows)
            {
                return true;
            }
            return false;
        }

        public static bool VerifNumeroDispo(int numero)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE Numero = '" + numero + "'");
            if (result.Rows.Count != 0)
            {
                return false;
            }
            return true;

        }

        public static void AddPuceToPlayer(Client player)
        {
            bool variable = false;
            while (!variable)
            {
                Random aleatoire = new Random();
                int n1 = aleatoire.Next(0, 9);
                int n2 = aleatoire.Next(0, 9);
                int n3 = aleatoire.Next(0, 9);
                int n4 = aleatoire.Next(0, 9);
                int numero = Convert.ToInt32(String.Format("" + n1 + "" + n2 + "" + n3 + "" + n4));
                if (VerifNumeroDispo(numero))
                {
                    PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                    variable = true;
                    API.shared.exported.database.executeQuery("INSERT INTO PucesTelephones VALUES ('','" + numero + "','" + objplayer.dbid + "','0')");
                }
            }
        }

        public static void ChangePuceOwner(int PuceID, Client NouveauProprietaire)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(NouveauProprietaire);
            API.shared.exported.database.executeQuery("UPDATE PucesTelephones SET IDJoueur ='" + objplayer.dbid + "' WHERE ID = '" + PuceID + "'");
        }

        public static void DestroyPuce(int PuceID)
        {
            API.shared.exported.database.executeQuery("DELETE FROM PucesTelephones WHERE ID ='" + PuceID + "'");
        }

        public static void SetNombreSmsRestant(int PuceID, int NombreSmsRestant)
        {
            API.shared.exported.database.executeQuery("UPDATE PucesTelephones SET NombreSmsRestant ='" + NombreSmsRestant + "' WHERE ID = '" + PuceID + "'");
        }

        public static void SetTempsAppelRestant(int PuceID, int TempsAppelRestant)
        {
            API.shared.exported.database.executeQuery("UPDATE PucesTelephones SET TempsAppelRestant ='" + TempsAppelRestant + "' WHERE ID = '" + PuceID + "'");
        }

        public static int GetNombreSmsRestant(int PuceID)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE ID = '" + PuceID + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["NombreSmsRestant"]);
                }
            }
            return -1;
        }

        public static int GetTempsAppelRestant(int PuceID)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM PucesTelephones WHERE ID = '" + PuceID + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["TempsAppelRestant"]);
                }
            }
            return -1;
        }

        // Interaction entre Puce et Telephone

        public static void SetPuceInTelephone(Client player, int NumeroPuce)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            int puce = GetPuceIDBDD(NumeroPuce);
            API.shared.exported.database.executeQuery("UPDATE Telephones SET Puce ='" + puce + "' WHERE IDJoueur = '" + objplayer.dbid + "'");
            API.shared.exported.database.executeQuery("UPDATE PucesTelephones SET Active ='1' WHERE ID = '" + puce + "'");

        }

        public static void RemovePuceOnTelephone(Client player, int NumeroPuce)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            int puce = GetPuceIDBDD(NumeroPuce);
            API.shared.exported.database.executeQuery("UPDATE Telephones SET Puce ='-1' WHERE IDJoueur = '" + objplayer.dbid + "'");
            API.shared.exported.database.executeQuery("UPDATE PucesTelephones SET Active ='0' WHERE ID = '" + puce + "'");
        }

    }
}
