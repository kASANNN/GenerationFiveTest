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
    public class CommandesJoueur : Script
    {
        [Command("binco")]
        public void CmdJoueurBinco(Client player, int var)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!objplayer.Logged) return;
            //detecter si il peux faire la commande par rapport a sa position
            switch (var)
            {
                case 0:
                    API.triggerClientEvent(player, "ShowCEFBinco");
                    break;
                case 1:
                    API.triggerClientEvent(player, "HideCEFBinco");
                    break;
                default:
                    API.sendNotificationToPlayer(player, "0 ou 1 fdp");
                    break;
            }
        }

        [Command("nomoff")]
        public void Nomoff(Client player)
        {
            API.resetPlayerNametag(player);
            API.sendNotificationToPlayer(player, "~g~Ton nom à bien été caché");
        }

        [Command("nomon")]
        public void Nomon(Client player)
        {
            API.setPlayerNametag(player, player.name);
            API.sendNotificationToPlayer(player, "~g~Ton nom est désormais visible");
        }

        [Command("monnaie")]
        public void Monnaie(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            var monnaie = objplayer.money;
            API.sendChatMessageToPlayer(player, "Tu as ~g~" + monnaie + "~s~$ dans ton porte-monnaie.");
        }

        [Command("payer", "~m~JOUEUR: ~s~/payer [id/PartieDuNom] [Montant]")]
        public void Payer(Client player, String idOrName, int Montant)
        {
            PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objtarget == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.money >= Montant)
            {
                var anciennemoney = objplayer.money;
                objplayer.money = anciennemoney - Montant;
                var anciennemoneytarget = objtarget.money;
                objtarget.money = anciennemoneytarget + Montant;
                API.sendChatMessageToPlayer(player, "Tu viens de donner ~g~" + Montant + "~s~$ à cette personne.");
                API.sendChatMessageToPlayer(objtarget.Handle, "Tu viens de reçevoir ~g~" + Montant + "~s~$ de cette personne.");
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent sur toi");
            }
        }

        [Command("pos")]
        public void Pos(Client player)
        {
            Vector3 playerPos = API.getEntityPosition(player);
            API.sendChatMessageToPlayer(player, "X: " + playerPos.X + " Y: " + playerPos.Y + " Z: " + playerPos.Z);
        }

        [Command("rot")]
        public void Rot(Client player)
        {
            Vector3 playerRot = API.getEntityRotation(player);
            API.sendChatMessageToPlayer(player, "X: " + playerRot.X + " Y: " + playerRot.Y + " Z: " + playerRot.Z);
        }

        [Command("trafiquerarme")]
        public void TrafiquerArmes(Client player)
        {
            if (API.getPlayerCurrentWeapon(player) == WeaponHash.Musket && (Inventaire.PlayerHaveItemInBDD(player, 5)))
            {
                if (API.getPlayerWeaponAmmo(player, WeaponHash.Musket) > 6)
                {
                    API.delay(4000, true, () =>
                    {
                        API.removePlayerWeapon(player, WeaponHash.Musket);
                        API.givePlayerWeapon(player, WeaponHash.DoubleBarrelShotgun, 6, true, true);
                        API.sendChatMessageToPlayer(player, "Tu viens de trafiquer ton arme.");
                        Random aleatoire = new Random();
                        int resultat = aleatoire.Next(0, 10);
                        if (resultat >= 8)
                        {
                            API.sendChatMessageToPlayer(player, "Ta scie s'est cassé en trafiquant ton arme.");
                            Inventaire.RemoveItemToPlayerInventaire(player, 5, 1);
                        }
                        else
                        {
                            return;
                        }
                    });
                }
                else
                {
                    API.delay(4000, true, () =>
                    {
                        API.removePlayerWeapon(player, WeaponHash.Musket);
                        API.givePlayerWeapon(player, WeaponHash.DoubleBarrelShotgun, 2, true, true);
                        API.sendChatMessageToPlayer(player, "Tu viens de trafiquer ton arme.");
                    });
                }
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le nécessaire pour trafiquer ton arme ou elle ne peut pas être trafiquée.");
            }
        }

        [Command("id", "~m~JOUEUR: ~s~/aid [id/PartieDuNom]")]
        public void Cmd_id(Client player, string idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (target == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            else API.sendChatMessageToPlayer(player, string.Format(Fonction.RemoveUnderscore(player.name) + " - id: " + target.id));
            return;
        }

        [Command("acceptermort")]
        public void Acceptermort(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            Vector3 PosRespawn = new Vector3(342.1638, -1397.347, 32.50927);
            Vector3 RotRespawn = new Vector3(49.6481, 0, 0);
            API.setEntityPosition(player, PosRespawn);
            API.setEntityRotation(player, RotRespawn);
            var banque = objplayer.bank;
            objplayer.bank = banque - Constante.PrixMort;
            API.sendChatMessageToPlayer(player, "Tu viens d'accepter ta mort, tu perds donc ~r~" + Constante.PrixMort + "$ et tu oublies les évènements avant ta mort.");
            objplayer.IsDead = false;
            objplayer.sante = 50;
            API.stopPlayerAnimation(objplayer.Handle);
            API.removeAllPlayerWeapons(player);
        }

        [Command("ordi", "~y~JOUEUR: ~s~/ordi [allumer/eteindre/puce]")]
        public void Ordi(Client player, String option)
        {
            if (option == "allumer")
            {
                if (API.getEntityData(player, "OrdiHack") == false)
                {
                    if (Inventaire.PlayerHaveItemInBDD(player, 6) & (Inventaire.PlayerHaveItemInBDD(player, 10)))
                    {
                        API.setEntityData(player, "OrdiHack", true);
                        API.sendChatMessageToPlayer(player, "Tu viens d'~g~allumer ~s~ton ordinateur, appuie sur '~b~E~s~' pour l'utiliser.");
                    }
                    else
                    {
                        API.sendChatMessageToPlayer(player, "~r~Tu ne peux pas utiliser ton ordinateur sans le routeur.");
                    }
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "Ton ordinateur est déjà allumé.");
                }
            }

            if (option == "eteindre")
            {
                if (API.getEntityData(player, "OrdiHack") == true)
                {
                    if (Inventaire.PlayerHaveItemInBDD(player, 6) & (Inventaire.PlayerHaveItemInBDD(player, 10)))
                    {
                        API.setEntityData(player, "OrdiHack", false);
                        API.sendChatMessageToPlayer(player, "Tu viens d'~r~éteindre ~s~ton ordinateur.");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "Ton ordinateur est déjà éteint.");
                }
            }

            if (option == "puce")
            {
                if (!Inventaire.PlayerHaveItemInBDD(player, 7) && (!Inventaire.PlayerHaveItemInBDD(player, 8)))
                {
                    API.sendChatMessageToPlayer(player, "~r~Trouves le matériel nécessaire pour faire ça.");
                }
                else if (!Inventaire.PlayerHaveItemInBDD(player, 8) || Inventaire.GetItemNumberInBDD(player, 8) < 1)
                {
                    API.sendChatMessageToPlayer(player, "~r~Il te manque la puce pour faire fonctionner ton routeur.");
                }
                else if (!Inventaire.PlayerHaveItemInBDD(player, 7))
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas de routeur.");
                }
                else
                {
                    int Item = Inventaire.RouteurPuce;
                    Inventaire saveitem = new Inventaire();
                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                    API.sendChatMessageToPlayer(player, "Tu as placer la puce dans ton routeur.");
                    Inventaire.RemoveItemToPlayerInventaire(player, 7, 1);
                    Inventaire.RemoveItemToPlayerInventaire(player, 8, 1);
                }
            }
        }

        [Command("donnerclefveh", "~y~JOUEUR: ~s~/donnerclefveh [Id/PartieDuNom] [IDVehicule]")]
        public void Donnerclefveh(Client player, String idOrName, int IDVehicule)
        {
            PlayerInfo objtarget = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (objtarget == null) API.sendChatMessageToPlayer(player, Constante.message_id_incorrect);
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(IDVehicule);
            if (objveh == null) API.sendChatMessageToPlayer(player, Constante.message_idveh_incorrect);
            if(Concess.GetVehKeyNumberPlayer(player, objveh.dbid) == 0)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu ne possède pas de clé de ce vehicule.");
                return;
            }
            if(Concess.PlayerIsProprio(player, objveh.dbid))
            {
                if(Concess.GetVehKeyNumberPlayer(player, objveh.dbid) == 1)
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu ne peux pas donner la derniere clé de ton vehicule.");
                    return;
                }
                else
                {
                    Concess.RemoveKeyVehToPlayer(player, objveh.dbid);
                    Concess.AddKeyVehToPlayer(objtarget.Handle, objveh.dbid);
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de ~g~donner ~s~une clé du véhicule ~b~{0} ~s~au joueur ~b~{1}~s~.", IDVehicule, Fonction.RemoveUnderscore(objtarget.PlayerName)));
                    API.sendChatMessageToPlayer(objtarget.Handle, String.Format("~b~{0} ~s~vient de te ~g~donner ~s~une clé du véhicule ~b~{1}~s~.", Fonction.RemoveUnderscore(player.name), IDVehicule));
                    return;
                }
            }
            else
            {
                Concess.RemoveKeyVehToPlayer(player, objveh.dbid);
                Concess.AddKeyVehToPlayer(objtarget.Handle, objveh.dbid);
                API.sendChatMessageToPlayer(player, String.Format("Tu viens de ~g~donner ~s~une cléf du véhicule ~b~{0} ~s~au joueur ~b~{1}~s~.", IDVehicule, Fonction.RemoveUnderscore(objtarget.PlayerName)));
                API.sendChatMessageToPlayer(objtarget.Handle, String.Format("~b~{0} ~s~vient de te ~g~donner ~s~une clé du véhicule ~b~{1}~s~.", Fonction.RemoveUnderscore(player.name), IDVehicule));
            }
        }

        [Command("jeterclefveh", "~y~JOUEUR: ~s~/jeterclefveh [IDVehicule]")]
        public void Jeterclefveh(Client player, int IDVehicule)
        {
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(IDVehicule);
            if (objveh == null) API.sendChatMessageToPlayer(player, Constante.message_idveh_incorrect);
            if (Concess.GetVehKeyNumberPlayer(player, objveh.dbid) == 0)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu ne possède pas de clé de ce vehicule.");
                return;
            }
            if (Concess.PlayerIsProprio(player, objveh.dbid))
            {
                if (Concess.GetVehKeyNumberPlayer(player, objveh.dbid) == 1)
                {
                    API.sendChatMessageToPlayer(player, "~r~Tu ne peux pas jeter la derniere clé de ton vehicule.");
                    return;
                }
                else
                {
                    Concess.RemoveKeyVehToPlayer(player, objveh.dbid);
                    API.sendChatMessageToPlayer(player, String.Format("Tu viens de ~r~jeter ~s~une cléf du véhicule ~b~{0} ~s~.", IDVehicule));
                    return;
                }
            }
            else
            {
                Concess.RemoveKeyVehToPlayer(player, objveh.dbid);
                API.sendChatMessageToPlayer(player, String.Format("Tu viens de ~r~jeter ~s~une cléf du véhicule ~b~{0} ~s~.", IDVehicule));
            }
        }

        [Command("voiramende")]
        public void Accepteramende(Client player)
        {
            if(!AmendeInfo.PlayerHasAmende(player))
                API.sendChatMessageToPlayer(player, "Personne ne t'a mis d'~r~amende~s~.");
            List<AmendeInfo> listedesamende = AmendeInfo.GetPlayerAmendeInfo(player);

            foreach (AmendeInfo amende in listedesamende)
            {
                API.sendChatMessageToPlayer(player, "N°"+amende.id+" prix~r~ "+amende.montant+"~s~$ raison :  "+amende.raison);
            }
        }

        [Command("payeramende", "~y~JOUEUR: ~s~/payeramende [ID de l'amende (/voiramende)]")]
        public void Accepteramende(Client player, int id)
        {
            
            if (!AmendeInfo.PlayerHasAmende(player))
                API.sendChatMessageToPlayer(player, "Personne ne t'a mis d'~r~amende~s~.");

            AmendeInfo amende = AmendeInfo.GetAmendeInfoById(id);
            if(amende == null)
                API.sendChatMessageToPlayer(player, "Référence de l'amende invalide.");

            if (amende.player != player)
                API.sendChatMessageToPlayer(player, "Cette amende ne t'est pas adressée.");

            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if(objplayer.money < amende.montant)
                API.sendChatMessageToPlayer(player, "Tu n'as pas assez d'argent sur toi.");

            objplayer.money -= amende.montant;
            API.sendChatMessageToPlayer(player, "Tu viens de régler ton amende de ~r~" + amende.montant + "~s~$.");
            AmendeInfo.Delete(amende);
            API.shared.exported.database.executeQuery("DELETE FROM UtilisateurAmende WHERE date=" + amende.date);
        }

        [Command("delier", "~m~JOUEUR: ~s~/delier [id/PartieDuNom]")]
        public void Delier(Client player, String idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (player.position.DistanceTo(API.getEntityPosition(target.Handle)) >= 2)
            {
                API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                return;
            }
            else if (target.IsMenotter == false)
            {
                API.sendChatMessageToPlayer(player, "Cette personne ~r~n'est pas ~s~menottée.");
                return;
            }
            else
            {
                if (Inventaire.PlayerHaveItemInBDD(player, 15))
                {
                    API.sendChatMessageToPlayer(player, "Tu viens de ~g~démenotter ~s~cette personne avec ta pince.");
                    API.sendChatMessageToPlayer(target.Handle, "Tu viens d'être ~g~démenotté ~s~.");
                    target.IsMenotter = false;
                    API.setPlayerClothes(target.Handle, 7, 0, 0);
                    API.stopPlayerAnimation(target.Handle);
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "Tu n'as rien pour briser les menottes.");
                }
            }
        }

        [Command("depanner", "~m~JOUEUR: ~s~/depanner [idDuVehicule]")]
        public void Depanner(Client player, String id)
        {
            int idveh = Int32.Parse(id);
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoById(idveh);
            if (player.position.DistanceTo(objveh.handle.position) > 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
            else if (Inventaire.GetItemNumberInBDD(player, 4) <= 0) API.sendChatMessageToPlayer(player, "Tu n'as pas de kit d'outils pour dépanner ton véhicule.");
            else
            {
                if (API.getVehicleHealth(objveh.handle) >= 600) API.sendChatMessageToPlayer(player, "Tu dois emmener ton véhicule au garage pour le réparer complètement.");
                else
                {
                    API.setVehicleHealth(objveh.handle, 600);
                    API.sendChatMessageToPlayer(player, "Tu viens de dépanner ton véhicule, emmène le au garage si tu veux le réparer complètement.");
                    Inventaire.RemoveItemToPlayerInventaire(player, 4, 1);
                }
            }
        }

        [Command("forcer", "~m~JOUEUR: ~s~/forcer [vehicule/maison]")]
        public void Forcer(Client player, String option)
        {
            NetHandle veh = CreationMenus.GetClosestVehicle(player);
            VehiculeInfo vehobj = VehiculeInfo.GetVehicleInfoByNetHandle(veh);
            if (option == "vehicule")
            {
                if (API.getPlayerCurrentWeapon(player) == WeaponHash.Crowbar)
                {
                    API.delay(5000, true, () =>
                    {
                        Random aleatoire = new Random();
                        int resultat = aleatoire.Next(1, 10);
                        if (resultat == 10)
                        {
                            API.sendChatMessageToPlayer(player, "Tu as ~g~réussis ~s~à forcer la porte du véhicule.");
                            API.setVehicleLocked(vehobj.handle, false);
                            vehobj.locked = false;
                            return;
                        }
                        else if (resultat >= 3)
                        {
                            API.sendChatMessageToPlayer(player, "Tu n'as ~r~pas réussis ~s~à forcer la porte du véhicule.");
                            return;
                        }
                        else
                        {
                            API.removePlayerWeapon(player, WeaponHash.Crowbar);
                            API.sendChatMessageToPlayer(player, "Tu as ~g~réussis ~s~à forcer la porte du véhicule, mais ton pied de biche s'est cassé.");
                            API.setVehicleLocked(vehobj.handle, false);
                            vehobj.locked = false;
                        }
                    });
                }
                else API.sendChatMessageToPlayer(player, "Tu n'as rien pour ~r~forcer ~s~la porte de ce véhicule.");
            }
            
            if (option == "maison")
            {
                API.sendChatMessageToPlayer(player, "J'arrive bientôt.");
            }
        }

        [Command("assemblerarme", "~m~JOUEUR: ~s~/assemblerarme [type d'arme (1-4)]")]
        public void AssemblerArme(Client player, int Nombre)
        {
            if (Nombre == 0 || Nombre >= 5) API.sendChatMessageToPlayer(player, "Tu dois entrer un nombre valide! (1 = Pistolet, 2 = P-Mitrailleur, 3 = Pompe, 4 = fusil)");
            else
            {
                if (Nombre == 1)
                {
                    if (Inventaire.GetItemNumberInBDD(player, 16) <= 0) API.sendChatMessageToPlayer(player, Constante.PasDeKit);
                    else API.triggerClientEvent(player, "KitPistolet");
                }
                if (Nombre == 2)
                {
                    if (Inventaire.GetItemNumberInBDD(player, 17) <= 0) API.sendChatMessageToPlayer(player, Constante.PasDeKit);
                    else API.triggerClientEvent(player, "KitPMitr");
                }
                if (Nombre == 3)
                {
                    if (Inventaire.GetItemNumberInBDD(player, 18) <= 0) API.sendChatMessageToPlayer(player, Constante.PasDeKit);
                    else API.triggerClientEvent(player, "KitPompe");
                }
                if (Nombre == 4)
                {
                    if (Inventaire.GetItemNumberInBDD(player, 19) <= 0) API.sendChatMessageToPlayer(player, Constante.PasDeKit);
                    else API.triggerClientEvent(player, "KitFusil");
                }
            }
        }

        [Command("vendrearme", "~m~JOUEUR: ~s~/vendrearme [Id/PartieDuNom] [Prix]")]
        public void VendreArme(Client player, String idOrName, int Prix)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (player.position.DistanceTo(target.Handle.position) >= 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
            else
            {
                if (API.getPlayerCurrentWeapon(player) == WeaponHash.Unarmed) API.sendChatMessageToPlayer(player, "Tu n'as pas d'armes dans les mains.");
                else
                {
                    if (API.getPlayerCurrentWeapon(target.Handle) != WeaponHash.Unarmed) API.sendChatMessageToPlayer(player, "Cette personne à déjà une arme dans les mains.");
                    else
                    {
                        WeaponHash ArmeActuelle = API.getPlayerCurrentWeapon(player);
                        int MunitionsActuelle = API.getPlayerWeaponAmmo(player, ArmeActuelle);
                        API.sendChatMessageToPlayer(player, "Tu propose ton arme à cette personne pour~g~ " + Prix + "~s~$.");
                        API.sendChatMessageToPlayer(target.Handle, "Cette personne te propose un ~b~" + ArmeActuelle + " ~s~pour~g~ " + Prix + "~s~$.");
                        API.sendChatMessageToPlayer(target.Handle, "(Tapes ~b~/accepter arme~s~ pour accepter l'offre.)");
                        API.setEntityData(target.Handle, "PrixArme", Prix);
                        API.setEntityData(target.Handle, "ArmeActuelle", ArmeActuelle);
                        API.setEntityData(player, "ArmeActuelle", ArmeActuelle);
                        API.setEntityData(target.Handle, "MunitionsActuelle", MunitionsActuelle);
                    }
                }
            }
        }

        [Command("accepter", "~m~JOUEUR: ~s~/accepter [arme/drogue/kit] [Id/PartieDuNom]")]
        public void Accepter(Client player, String option, String idOrName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (option == "arme")
            {
                if (player.position.DistanceTo(target.Handle.position) >= 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                else
                {
                    if (API.getEntityData(player, "PrixArme") <= 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé d'arme.");
                    else
                    {
                        if (API.getEntityData(player, "ArmeActuelle") == 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé d'arme.");
                        else
                        {
                            API.sendChatMessageToPlayer(player, "Tu viens d'acheter cette arme pour~g~ " + API.getEntityData(player, "PrixArme") + "~s~$.");
                            API.sendChatMessageToPlayer(target.Handle, "Tu viens de vendre une arme pour~g~ " + API.getEntityData(player, "PrixArme") + "~s~$.");
                            API.givePlayerWeapon(player, API.getEntityData(player, "ArmeActuelle"), API.getEntityData(player, "MunitionsActuelle"), true, true);
                            API.removePlayerWeapon(target.Handle, API.getEntityData(target.Handle, "ArmeActuelle"));
                            var anciennemoney = objplayer.money;
                            var anciennemoneyvendeur = target.money;
                            objplayer.money = anciennemoney - API.getEntityData(player, "PrixArme");
                            target.money = anciennemoneyvendeur + API.getEntityData(player, "PrixArme");
                            API.setEntityData(player, "PrixArme", 0);
                            API.setEntityData(player, "ArmeActuelle", 0);
                            API.setEntityData(target.Handle, "ArmeActuelle", 0);
                            API.setEntityData(player, "MunitionsActuelle", 0);
                        }
                    }
                }
            }
            if (option == "kit")
            {
                if (player.position.DistanceTo(target.Handle.position) >= 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                else
                {
                    if (API.getEntityData(player, "TypeKit") <= 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé de kit.");
                    else
                    {
                        if (API.getEntityData(player, "QuantitéKit") == 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé de kit.");
                        else
                        {
                            API.sendChatMessageToPlayer(player, "Tu viens d'acheter~b~ " + API.getEntityData(player, "QuantitéKit") +  " ~s~kit pour~g~ " + API.getEntityData(player, "PrixKit") + "~s~$.");
                            API.sendChatMessageToPlayer(target.Handle, "Tu viens de vendre~b~" + API.getEntityData(player, "QuantitéKit") + " ~s~kit pour~g~ " + API.getEntityData(player, "PrixKit") + "~s~$.");
                            if (API.getEntityData(player, "TypeKit") == 1)
                            {
                                int Item = Inventaire.KitPistolet;
                                Inventaire saveitem = new Inventaire();
                                saveitem.AddItemToPlayerInventaire(player, Item, API.getEntityData(player, "QuantitéKit"));
                                Inventaire.RemoveItemToPlayerInventaire(target.Handle, Item, API.getEntityData(player, "QuantitéKit"));
                            }
                            if (API.getEntityData(player, "TypeKit") == 2)
                            {
                                int Item = Inventaire.KitPMitrailleur;
                                Inventaire saveitem = new Inventaire();
                                saveitem.AddItemToPlayerInventaire(player, Item, API.getEntityData(player, "QuantitéKit"));
                                Inventaire.RemoveItemToPlayerInventaire(target.Handle, Item, API.getEntityData(player, "QuantitéKit"));
                            }
                            if (API.getEntityData(player, "TypeKit") == 3)
                            {
                                int Item = Inventaire.KitPompe;
                                Inventaire saveitem = new Inventaire();
                                saveitem.AddItemToPlayerInventaire(player, Item, API.getEntityData(player, "QuantitéKit"));
                                Inventaire.RemoveItemToPlayerInventaire(target.Handle, Item, API.getEntityData(player, "QuantitéKit"));
                            }
                            if (API.getEntityData(player, "TypeKit") == 4)
                            {
                                int Item = Inventaire.KitFusil;
                                Inventaire saveitem = new Inventaire();
                                saveitem.AddItemToPlayerInventaire(player, Item, API.getEntityData(player, "QuantitéKit"));
                                Inventaire.RemoveItemToPlayerInventaire(target.Handle, Item, API.getEntityData(player, "QuantitéKit"));
                            }
                            var anciennemoney = objplayer.money;
                            var anciennemoneyvendeur = target.money;
                            objplayer.money = anciennemoney - API.getEntityData(player, "PrixKit");
                            target.money = anciennemoneyvendeur + API.getEntityData(player, "PrixKit");
                            API.setEntityData(target.Handle, "TypeKit", 0);
                            API.setEntityData(target.Handle, "PrixKit", 0);
                            API.setEntityData(target.Handle, "QuantitéKit", 0);
                        }
                    }
                }
            }
        }

        [Command("refuser", "~m~JOUEUR: ~s~/refuser [arme/drogue/kit] [Id/PartieDuNom]")]
        public void Refuser(Client player, String option, String idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            if (option == "arme")
            {
                if (player.position.DistanceTo(target.Handle.position) >= 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                else
                {
                    if (API.getEntityData(player, "PrixArme") <= 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé d'arme.");
                    else
                    {
                        if (API.getEntityData(player, "ArmeActuelle") == 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé d'arme.");
                        else
                        {
                            API.sendChatMessageToPlayer(player, "Tu refuses d'acheter cette arme.");
                            API.sendChatMessageToPlayer(target.Handle, "Cette personne a refusé de t'acheter l'arme.");
                            API.setEntityData(player, "PrixArme", 0);
                            API.setEntityData(player, "ArmeActuelle", 0);
                            API.setEntityData(target.Handle, "ArmeActuelle", 0);
                            API.setEntityData(player, "MunitionsActuelle", 0);
                        }
                    }
                }
            }
            if (option == "kit")
            {
                if (player.position.DistanceTo(target.Handle.position) >= 2) API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                else
                {
                    if (API.getEntityData(player, "TypeKit") <= 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé de kit.");
                    else
                    {
                        if (API.getEntityData(player, "QuantitéKit") == 0) API.sendChatMessageToPlayer(player, "Personne ne t'a proposé de kit.");
                        else
                        {
                            API.sendChatMessageToPlayer(player, "Tu refuses cette transaction.");
                            API.sendChatMessageToPlayer(target.Handle, "Cette personne a refusé la transaction.");
                            API.setEntityData(target.Handle, "TypeKit", 0);
                            API.setEntityData(target.Handle, "PrixKit", 0);
                            API.setEntityData(target.Handle, "QuantitéKit", 0);
                        }
                    }
                }
            }
        }

        [Command("vliste", "~m~JOUEUR: ~s~/vliste")]
        public void Vliste(Client player)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Vehicules WHERE Proprio = '"+ PlayerInfo.GetPlayerInfoObject(player).dbid +"'");
            if(result.Rows.Count == 0)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'es proprietaire d'aucuns vehicules.");
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~g~Vehicules dont tu es le propriétaire :");
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (VehiculeInfo.GetVehicleInfoByIdBDD(Convert.ToInt32(result.Rows[i]["ID"])) == null)
                    {
                        API.sendChatMessageToPlayer(player, String.Format("~g~{0}~s~ : {1} Etat : ~r~Garé~s~.", i + 1, API.getVehicleDisplayName((VehicleHash)result.Rows[i]["model"])));
                    }
                    else
                    {
                        API.sendChatMessageToPlayer(player, String.Format("~g~{0}~s~ : {1} Etat : ~g~Spawn~s~.", i + 1, API.getVehicleDisplayName((VehicleHash)result.Rows[i]["model"])));
                    }
                }
            }
        }

        [Command("vspawn", "~m~JOUEUR: ~s~/vspawn [Numero dans le /vliste]")]
        public void Vspawn(Client player, int Numero)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Vehicules WHERE Proprio = '" + PlayerInfo.GetPlayerInfoObject(player).dbid + "'");
            if (result.Rows.Count == 0)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'es proprietaire d'aucuns vehicules.");
                return;
            }
            if (result.Rows.Count < Numero - 1)
            {
                API.sendChatMessageToPlayer(player, "~r~Ce vehicule n'existe pas.");
                return;
            }
            else
            {
                if (Convert.ToBoolean(result.Rows[Numero - 1]["Spawned"]) == false)
                {
                    Vector3 PosVspawn = new Vector3(Convert.ToInt32(result.Rows[Numero - 1]["PosX"]), Convert.ToInt32(result.Rows[Numero - 1]["PosY"]), Convert.ToInt32(result.Rows[Numero - 1]["PosZ"]));
                    Vector3 RotVspawn = new Vector3(Convert.ToInt32(result.Rows[Numero - 1]["RotX"]), Convert.ToInt32(result.Rows[Numero - 1]["RotY"]), Convert.ToInt32(result.Rows[Numero - 1]["RotZ"]));
                    VehiculeInfo vehicleobj = new VehiculeInfo((VehicleHash)result.Rows[Numero - 1]["model"], PosVspawn, RotVspawn, Convert.ToInt32(result.Rows[Numero - 1]["color1"]), Convert.ToInt32(result.Rows[Numero - 1]["color2"]), Convert.ToString(result.Rows[Numero - 1]["plaque"]), 0);
                    vehicleobj.spawned = true;
                    API.exported.database.executeQuery("UPDATE Vehicules SET Spawned = '1' WHERE ID = '" + Convert.ToInt32(result.Rows[Numero - 1]["ID"]) + "'");
                    vehicleobj.jobid = Convert.ToInt32(result.Rows[Numero - 1]["jobid"]);
                    vehicleobj.factionid = Convert.ToInt32(result.Rows[Numero - 1]["factionid"]);
                    vehicleobj.IDBDDProprio = Convert.ToInt32(result.Rows[Numero - 1]["Proprio"]);
                    vehicleobj.dbid = Convert.ToInt32(result.Rows[Numero - 1]["ID"]);
                    API.setVehicleLocked(vehicleobj.handle, Convert.ToBoolean(result.Rows[Numero - 1]["Locked"]));
                    vehicleobj.locked = Convert.ToBoolean(result.Rows[Numero - 1]["Locked"]);
                    API.sendChatMessageToPlayer(player, "Tu viens de spawn le vehicule n°~g~" + Numero + "~s~.");
                    return;
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "~r~Ce vehicule est déjà spawn.");
                }
            }
        }

        [Command("irc", "~m~JOUEUR: ~s~/irc [Message]", GreedyArg = true)]
        public void Irc(Client player, String Message)
        {
            API.sendChatMessageToPlayer(player, "~g~Ton message a bien été envoyé aux Admins.");
            List<Client> Player = API.getAllPlayers();
            foreach (Client target in Player)
            {
                if(PlayerInfo.GetPlayerInfoObject(target).adminlvl > 0 && PlayerInfo.GetPlayerInfoObject(target).IrcActif == true)
                {
                    API.sendChatMessageToPlayer(target, "~r~[IRC]~s~ " + player.name + "(" + PlayerInfo.GetPlayerInfoObject(player).id + ") : " + Message);
                }
            }
        }

        [Command("achetergraine")]
        public void Achetergraine(Client player)
        {
            API.triggerClientEvent(player, "ShowCEFDrogue");
        }

        [Command("fermer")]
        public void Fermer(Client player)
        {
            API.triggerClientEvent(player, "HideCEFDrogue");
        }
    }
}