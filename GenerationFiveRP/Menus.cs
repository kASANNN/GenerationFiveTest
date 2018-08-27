using System;
using System.Collections.Generic;
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
    public class Menus : Script
    {
        bool Premiereportelocked = true;
        bool Premierecellulelocked = true;
        bool Deuxiemecellulelocked = true;
        bool Troisiemecellulelocked = true;
        bool Portefondlocked = true;

        public Menus()
        {
            API.onClientEventTrigger += ClientEvent;
            API.onEntityEnterColShape += EntityEnterColShape;
        }

        WeaponHash[] Pistol = { WeaponHash.Pistol, WeaponHash.CombatPistol, WeaponHash.Pistol50, WeaponHash.SNSPistol, WeaponHash.HeavyPistol };
        WeaponHash[] SMG = { WeaponHash.MicroSMG, WeaponHash.MachinePistol, WeaponHash.SMG, WeaponHash.CombatPDW, WeaponHash.MiniSMG };
        WeaponHash[] Pompe = { WeaponHash.PumpShotgun, WeaponHash.SawnoffShotgun, WeaponHash.Musket, WeaponHash.DoubleBarrelShotgun };
        WeaponHash[] Rifle = { WeaponHash.AssaultRifle, WeaponHash.CarbineRifle, WeaponHash.AdvancedRifle, WeaponHash.SpecialCarbine, WeaponHash.BullpupRifle, WeaponHash.CompactRifle };

        public void ClientEvent(Client player, string eventName, object[] args)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            NetHandle veh = CreationMenus.GetClosestVehicle(player);
            VehiculeInfo vehobj = VehiculeInfo.GetVehicleInfoByNetHandle(veh);
            
            switch (eventName)
            {
                case "MenuAutoEcole":
                    switch (Convert.ToInt16(args[0]))
                    {
                        case 0:
                            if(objplayer.CodeDeLaRoute == true || objplayer.PermisDeConduire > 0)
                            {
                                API.sendChatMessageToPlayer(player, "Tu ~g~possèdes~s~ déjà le ~b~code de la route~s~ !");
                                break;
                            }
                            else
                            {
                                API.sendChatMessageToPlayer(player, "~g~Pour répondre aux questions, il te suffit d'ecrire le numéro de la réponse.");
                                API.sleep(3000);
                                objplayer.QuestionEnCours = 0;
                                objplayer.DansQuestionnaire = true;
                                AutoEcole.PreparerQuestionnaire(player);
                                AutoEcole.LoadQuestion(player);
                                break;
                            }
                        case 1:
                            if(objplayer.PermisDeConduire > 0)
                            {
                                API.sendChatMessageToPlayer(player, "Tu ~g~possèdes~s~ déjà le ~b~permis de conduire~s~ !");
                            }
                            if(objplayer.CodeDeLaRoute == true)
                            {
                                VehiculeInfo objvehicule = new VehiculeInfo(VehicleHash.Panto, new Vector3(254.111, -1640.929, 29.12594), new Vector3(0, 0, 45.47156), 64, 64, "AutoEcole");
                                API.setVehicleEngineStatus(objvehicule.handle, false);
                                objvehicule.essence = 100;
                                API.setPlayerIntoVehicle(player, objvehicule.handle, -1);
                                DataTable resultAutoEcole = API.shared.exported.database.executeQueryWithResult("SELECT * FROM CheckpointAutoEcole");
                                objplayer.NombreCheckpointAutoEcole = resultAutoEcole.Rows.Count;
                                objplayer.CheckpointEnCoursAutoEcole = 0;
                                objplayer.ConduiteEnCoursAutoEcole = true;
                                API.triggerClientEvent(player, "pointconduiteAE", Convert.ToInt32(resultAutoEcole.Rows[0]["PosX"]), Convert.ToInt32(resultAutoEcole.Rows[0]["PosY"]), Convert.ToInt32(resultAutoEcole.Rows[0]["PosZ"]));
                                ColShape test = API.createSphereColShape(new Vector3(Convert.ToInt32(resultAutoEcole.Rows[0]["PosX"]), Convert.ToInt32(resultAutoEcole.Rows[0]["PosY"]), Convert.ToInt32(resultAutoEcole.Rows[0]["PosZ"])), 5f);
                                test.setData("CheckpointAE", true);
                                break;
                            }
                            else
                            {
                                API.sendChatMessageToPlayer(player, "~r~Pour acceder a l'épreuve de conduite, tu dois d'abord passer le code de la route !");
                                break;
                            }
                    }
                    break;

                case "MenuUniteLSPD":
                    if(UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player) != null)
                    {
                        UnitesLSPDInfo objUnite = UnitesLSPDInfo.GetUniteLSPDInfoByMembre(player);
                        switch(Convert.ToInt16(args[0]))
                        {
                            case 0:
                                objUnite.Status = "En patrouille";
                                Chat.Command_R(player, "10.41.");
                                break;
                            case 1:
                                objUnite.Status = "inactif";
                                Chat.Command_R(player, "10.42.");
                                break;
                            case 2:
                                objUnite.Status = "En Stand-By";
                                Chat.Command_R(player, "10.23.");
                                break;
                        }
                    }
                    break;
                case "MenuArmurerieLSPD":
                    if (police.isArmurerieLSPD(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                objplayer.armure = 100;
                                API.setPlayerArmor(player, 100);
                                break;
                            case 1:
                                API.givePlayerWeapon(player, WeaponHash.Flashlight, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.StunGun, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Nightstick, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Pistol, 36, true, true);
                                break;
                            case 2:
                                API.givePlayerWeapon(player, WeaponHash.Flashlight, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.StunGun, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Nightstick, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Pistol, 36, true, true);
                                API.givePlayerWeapon(player, WeaponHash.SMG, 60, true, true);
                                break;
                            case 3:
                                API.givePlayerWeapon(player, WeaponHash.Flashlight, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Pistol, 36, true, true);
                                break;
                            case 4:
                                API.givePlayerWeapon(player, WeaponHash.Flashlight, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.StunGun, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Nightstick, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Pistol, 36, true, true);
                                API.givePlayerWeapon(player, WeaponHash.PumpShotgun, 16, true, true);
                                break;
                            case 5:
                                API.givePlayerWeapon(player, WeaponHash.Flashlight, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.StunGun, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Nightstick, 1, true, true);
                                API.givePlayerWeapon(player, WeaponHash.Pistol, 36, true, true);
                                API.givePlayerWeapon(player, WeaponHash.PumpShotgun, 16, true, true);
                                break;
                        }
                    }
                    break;

                case "MenuService":
                    if (police.isService(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (objplayer.IsFactionDuty == false)
                                {
                                    objplayer.IsFactionDuty = true;
                                    API.sendChatMessageToPlayer(player, "~#d2d628~", "Tu viens de prendre ton service, enfiles tes vêtements.");
                                    return;
                                }
                                else
                                {
                                    objplayer.IsFactionDuty = false;
                                    API.removeAllPlayerWeapons(player);
                                    objplayer.armure = 0;
                                    API.sendChatMessageToPlayer(player, "~#d2d628~", "Tu viens de terminer ton service.");
                                    Connexion lv = new Connexion();
                                    lv.LoadVetements(player);
                                    lv.LoadAccessoires(player);
                                    return;
                                }
                            case 1:
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
                                break;
                        }
                    }
                    break;

                case "MenuCelluleLSPD":
                    if (police.isCellule(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                Premiereportelocked = !Premiereportelocked;
                                API.call("DoorManager", "setDoorState", Constante.porte1, Premiereportelocked, 0);
                                API.sendChatMessageToPlayer(player, "Tu as " + (Premiereportelocked ? "fermer" : "ouvert") + " la porte.");
                                break;
                            case 1:
                                Premierecellulelocked = !Premierecellulelocked;
                                API.call("DoorManager", "setDoorState", Constante.porte2, Premierecellulelocked, 0);
                                API.sendChatMessageToPlayer(player, "Tu as " + (Premierecellulelocked ? "fermer" : "ouvert") + " la porte.");
                                break;
                            case 2:
                                Deuxiemecellulelocked = !Deuxiemecellulelocked;
                                API.call("DoorManager", "setDoorState", Constante.porte3, Deuxiemecellulelocked, 0);
                                API.sendChatMessageToPlayer(player, "Tu as " + (Deuxiemecellulelocked ? "fermer" : "ouvert") + " la porte.");
                                break;
                            case 3:
                                Troisiemecellulelocked = !Troisiemecellulelocked;
                                API.call("DoorManager", "setDoorState", Constante.porte4, Troisiemecellulelocked, 0);
                                API.sendChatMessageToPlayer(player, "Tu as " + (Troisiemecellulelocked ? "fermer" : "ouvert") + " la porte.");
                                break;
                            case 4:
                                Portefondlocked = !Portefondlocked;
                                API.call("DoorManager", "setDoorState", Constante.porte3, Portefondlocked, 0);
                                API.sendChatMessageToPlayer(player, "Tu as " + (Portefondlocked ? "fermer" : "ouvert") + " la porte.");
                                break;
                        }
                    }
                    break;
                case "MenuArmurerieCivil":
                    if (Fonction.isArmurerieCivil(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (objplayer.money < 2500)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour conclure cet achat.");
                                }
                                else
                                {
                                    int amount = 2500;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.givePlayerWeapon(player, WeaponHash.Pistol, 24, true, true);
                                    API.sendChatMessageToPlayer(player, "~g~Tu as acheté un Pistolet 9mm pour " + amount + "~g~$.");
                                }
                                break;
                            case 1:
                                if (objplayer.money < 4000)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour conclure cet achat.");
                                }
                                else
                                {
                                    int amount = 4000;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.givePlayerWeapon(player, WeaponHash.Musket, 12, true, true);
                                    API.sendChatMessageToPlayer(player, "~g~Tu as acheté un Mousquet pour " + amount + "~g~$.");
                                }
                                break;
                            case 2:
                                if (objplayer.money < 300)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour conclure cet achat.");
                                }
                                else
                                {
                                    int amount = 300;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.givePlayerWeapon(player, WeaponHash.Knife, 1, true, true);
                                    API.sendChatMessageToPlayer(player, "~g~Tu as acheté un couteau de combat pour " + amount + "~g~$.");
                                }
                                break;
                        }
                    }
                    break;

                case "MenuVehEboueur":
                    if (objplayer.jobid == Constante.Job_Eboueur)
                    {
                        if (objplayer.IsJobDuty == true)
                        {
                            if (!player.isInVehicle)
                            {
                                if (player.position.DistanceTo(Constante.Pos_CamionEboueur) < 2)
                                {
                                    switch (Convert.ToInt16(args[0]))
                                    {
                                        case 0:
                                            Vector3 PlayerPos = API.getEntityPosition(player);
                                            Vector3 PlayerRot = API.getEntityRotation(player);
                                            VehiculeInfo vehicleobj = new VehiculeInfo(VehicleHash.Trash, PlayerPos, PlayerRot, 0, 0, "Eboueur", 0);
                                            vehicleobj.essence = 100;
                                            API.setVehicleEngineStatus(vehicleobj.handle, true);
                                            API.setPlayerIntoVehicle(player, vehicleobj.handle, -1);
                                            API.sendChatMessageToPlayer(player, "Tu peux commencer à travailler, trouve des poubelles et décharge les ici.");
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                switch (Convert.ToInt16(args[0]))
                                {
                                    case 1:
                                        API.sendChatMessageToPlayer(player, "Tu viens de ranger ton camion.");
                                        API.deleteEntity(API.getPlayerVehicle(player));
                                        break;
                                }
                            }
                        }                        
                    }
                    break;

                case "MenuBanque":
                    if (BanqueInfo.GetBanqueInfoClosePos(player.position, 1) != null)
                    {
                        int argentbanque = objplayer.bank;
                        int monnaie = objplayer.money;
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                API.sendChatMessageToPlayer(player, "Tu as ~g~" + argentbanque + "~s~$ sur ton compte en banque.");
                                break;
                            case 1:
                                int recupArgent = 0;
                                try
                                {
                                    recupArgent = int.Parse((String)args[2]);
                                    if (argentbanque >= recupArgent)
                                    {
                                        objplayer.bank = argentbanque - recupArgent;
                                        objplayer.money = monnaie + recupArgent;
                                        int argentupdate = objplayer.bank;
                                        API.sendChatMessageToPlayer(player, "Vous venez de retirer ~g~" + recupArgent + "~s~$ depuis votre compte en banque.");
                                        API.sendChatMessageToPlayer(player, "Votre nouveau solde en banque est de ~g~" + objplayer.bank + "~s~$.");
                                    }
                                    else
                                    {
                                        API.sendChatMessageToPlayer(player, "Impossible d'effectuer l'opération, vous n'avez que ~r~" + argentbanque + "~s~$ sur votre compte en banque.");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Exception erreur = e;
                                    API.sendChatMessageToPlayer(player, "~r~Vous devez entrer un montant valide pour pouvoir retirer de l'argent.");
                                }
                                break;
                            case 2:
                                int depotArgent = 0;
                                try
                                {
                                    depotArgent = int.Parse((String)args[2]);
                                    if (monnaie >= depotArgent)
                                    {
                                        objplayer.money = monnaie - depotArgent;
                                        objplayer.bank = argentbanque + depotArgent;
                                        API.sendChatMessageToPlayer(player, "Vous venez de déposer ~g~" + depotArgent + "~s~$ sur votre compte en banque.");
                                        API.sendChatMessageToPlayer(player, "Votre nouveau solde en banque est de ~g~" + objplayer.bank + "~s~$.");
                                    }
                                    else
                                    {
                                        API.sendChatMessageToPlayer(player, "Impossible d'effectuer l'opération, vous n'avez que ~r~" + monnaie + "~s~$ sur vous.");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Exception erreur = e;
                                    API.sendChatMessageToPlayer(player, "~r~Vous devez entrer un montant valide pour pouvoir déposer de l'argent.");
                                }
                                break;
                        }
                    }
                    break;

                case "MenuDistrib":
                    if (police.isDistrib(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (objplayer.money < 2)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour utiliser le distributeur.");
                                }
                                else
                                {
                                    int amount = 2;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.setPlayerHealth(player, objplayer.sante + 10);
                                    objplayer.sante = API.getPlayerHealth(player);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un donuts pour " + amount + "~g~$.");
                                }
                                break;
                            case 1:
                                if (objplayer.money < 2)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour utiliser le distributeur.");
                                }
                                else
                                {
                                    int amount = 2;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.setPlayerHealth(player, objplayer.sante + 10);
                                    objplayer.sante = API.getPlayerHealth(player);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter une boisson pour " + amount + "~g~$.");
                                }
                                break;
                            case 2:
                                if (objplayer.money < 2)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour utiliser le distributeur.");
                                }
                                else
                                {
                                    int amount = 2;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.setPlayerHealth(player, objplayer.sante + 10);
                                    objplayer.sante = API.getPlayerHealth(player);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un paquet de chips pour " + amount + "~g~$.");
                                }
                                break;
                            case 3:
                                if (objplayer.money < 2)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour utiliser le distributeur.");
                                }
                                else
                                {
                                    int amount = 2;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.setPlayerHealth(player, objplayer.sante + 10);
                                    objplayer.sante = API.getPlayerHealth(player);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un paquet de biscuits pour " + amount + "~g~$.");
                                }
                                break;
                        }
                    }
                    break;

                case "MenuMagasin":
                    if (Magasin.isMagasin(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (objplayer.money < 6)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour acheter ceci.");
                                }
                                else
                                {
                                    int amount = 6;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Cigarettes;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 20);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un paquet de cigarettes pour " + amount + "~g~$.");
                                }
                                break;
                            case 1:
                                if (objplayer.money < 1)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour acheter ceci.");
                                }
                                else
                                {
                                    int amount = 1;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Briquet;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un briquet pour " + amount + "~g~$.");
                                }
                                break;
                            case 2:
                                if (objplayer.money < 8)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour acheter ceci.");
                                }
                                else
                                {
                                    int amount = 8;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Alcool;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    //API.setPlayerHealth(player, 100);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter une bouteille d'alcool pour " + amount + "~g~$.");
                                }
                                break;
                            case 3:
                                if (Telephone.PlayerHaveTelephone(player))
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu as déjà un téléphone sur toi.");
                                }
                                else
                                {
                                    int amount = 50;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    Telephone.AddTelephoneToPlayer(player);
                                    API.sendChatMessageToPlayer(player, "Tu viens d'acheter un ~b~téléphone ~s~pour ~g~" + amount + "~s~$.");
                                    API.sendChatMessageToPlayer(player, "Tu peux acheter jusque ~b~3 puces ~s~et les changer à ta guise.");
                                }
                                break;
                            case 4:
                                if (Telephone.GetNumberOfPucePlayer(player) >= 3)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu as déjà 3 puces sur toi tu ne peux en avoir plus.");
                                }
                                else
                                {
                                    int amount = 10;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    Telephone.AddPuceToPlayer(player);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter une puce prépayée pour " + amount + "~g~$.");
                                }
                                break;
                        }
                    }
                    break;

                case "MenuRevendeur":
                    if (Magasin.isRevendeur(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (objplayer.money < 200) API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour te payer cela.");
                                else
                                {
                                    int amount = 200;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Outils;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un kit d'outils pour " + amount + "~g~$.");
                                }
                                break;
                            case 1:
                                if (objplayer.money < 50) API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour te payer cela.");
                                else
                                {
                                    int amount = 50;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Scie;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter une scie pour " + amount + "~g~$.");
                                }
                                break;
                            case 2:
                                if (objplayer.money < 20) API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour te payer cela.");
                                else
                                {
                                    int amount = 20;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un pied de biche pour " + amount + "~g~$.");
                                    API.givePlayerWeapon(player, WeaponHash.Crowbar, 1, true, true);
                                }
                                break;
                            case 3:
                                if (objplayer.money < 35) API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour te payer cela.");
                                else
                                {
                                    int amount = 35;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter une batte de baseball pour " + amount + "~g~$.");
                                    API.givePlayerWeapon(player, WeaponHash.Bat, 1, true, true);
                                }
                                break;
                            case 4:
                                if (objplayer.money < 20) API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour te payer cela.");
                                else
                                {
                                    int amount = 20;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Pince;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter une prince Monseigneur pour " + amount + "~g~$.");
                                }
                                break;
                        }
                    }
                    break;

                case "MenuMaisonAchetéeProprio":
                    DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Logements");
                    if (result.Rows.Count != 0)
                    {
                        foreach (DataRow row in result.Rows)
                        {
                            Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                            if (player.position.DistanceTo(logpos) < 2)
                            {
                                switch (Convert.ToInt16(args[0]))
                                {
                                    case 0:
                                        objplayer.IsOnInt = true;
                                        API.setEntityDimension(player, Convert.ToInt32(row["ID"]));
                                        objplayer.dimension = Convert.ToInt32(row["ID"]);
                                        String app = Convert.ToString(row["model"]);
                                        if (app == "app1")
                                        {
                                            Logement test = new Logement();
                                            API.setEntityPosition(player, test.app1);
                                        }
                                        if (app == "app2")
                                        {
                                            Logement test = new Logement();
                                            API.setEntityPosition(player, test.app2);
                                        }
                                        break;
                                    case 1:
                                        if (Convert.ToBoolean(row["Ouvert"]))
                                        {
                                            API.exported.database.executeQuery("UPDATE Logements SET Ouvert = '0'");
                                            API.sendNotificationToPlayer(player, "~g~Tu viens de fermer la porte de ta maison à clé");
                                        }
                                        else
                                        {
                                            API.exported.database.executeQuery("UPDATE Logements SET Ouvert = '1'");
                                            API.sendNotificationToPlayer(player, "~g~Tu viens de d'ouvrir la porte de ta maison");
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    break;

                case "MenuSortirLogement":
                    DataTable resultsortirlogement = API.exported.database.executeQueryWithResult("SELECT * FROM Logements WHERE ID='"+ objplayer.dimension +"'");
                    if (resultsortirlogement.Rows.Count != 0)
                    {
                        foreach (DataRow row in resultsortirlogement.Rows)
                        {
                            switch (Convert.ToInt16(args[0]))
                            {
                                case 0:
                                    Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                                    API.setEntityPosition(player, logpos);
                                    API.setEntityDimension(player, 0);
                                    objplayer.dimension = 0;
                                    objplayer.IsOnInt = false;
                                    break;
                                case 1:
                                    Vector3 posgarage = new Vector3(179.0791, -1000.706, -98.99995);
                                    API.setEntityPosition(player, posgarage);
                                    break;
                            }
                        }
                    }
                    break;

                case "MenuExtToGarage":
                    DataTable accesexttogarage = API.exported.database.executeQueryWithResult("SELECT * FROM Garages");
                    foreach (DataRow row in accesexttogarage.Rows)
                    {
                        if (player.position.DistanceTo(new Vector3(Convert.ToDouble(row["PosX"]), Convert.ToDouble(row["PosY"]), Convert.ToDouble(row["PosZ"]))) < 2)
                        {
                            switch (Convert.ToInt16(args[0]))
                            {
                                case 0:
                                    if(Convert.ToBoolean(row["Locked"]) == false)
                                    {
                                        API.setEntityPosition(player, new Vector3(172.8933, -1007.904, -98.99995));
                                        API.setEntityDimension(player, Convert.ToInt32(row["ID"]));
                                        objplayer.dimension = Convert.ToInt32(row["ID"]);
                                        objplayer.IsOnInt = true;
                                        return;
                                    }
                                    else
                                    {
                                        API.sendNotificationToPlayer(player, "~r~Ce garage est verrouillé.", true);
                                        return;
                                    }
                                case 1:
                                    if (Logement.PlayerHaveKeyHouse(player, Convert.ToInt32(row["MaisonID"])))
                                    {
                                        if(Convert.ToBoolean(row["Locked"]) == false)
                                        {
                                            API.exported.database.executeQuery("UPDATE Garages SET Locked=1 WHERE ID='"+ Convert.ToInt32(row["ID"]) +"'");
                                            API.sendNotificationToPlayer(player, "~r~Tu as verrouillé ce garage.", true);
                                            return;
                                        }
                                        else
                                        {
                                            API.exported.database.executeQuery("UPDATE Garages SET Locked=0 WHERE ID='" + Convert.ToInt32(row["ID"]) + "'");
                                            API.sendNotificationToPlayer(player, "~r~Tu as deverrouillé ce garage.", true);
                                            return;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case "MenuGarageToExt":
                    DataTable accesgaragetoext = API.exported.database.executeQueryWithResult("SELECT * FROM Garages WHERE ID='"+ objplayer.dimension +"'");
                    foreach (DataRow row in accesgaragetoext.Rows)
                    {
                        if (player.position.DistanceTo(new Vector3(172.8933, -1007.904, -98.99995)) < 2)
                        {
                            switch (Convert.ToInt16(args[0]))
                            {
                                case 0:
                                    API.setEntityPosition(player, new Vector3(Convert.ToDouble(row["PosX"]), Convert.ToDouble(row["PosY"]), Convert.ToDouble(row["PosZ"])));
                                    API.setEntityDimension(player, 0);
                                    objplayer.dimension = 0;
                                    objplayer.IsOnInt = false;
                                    return;
                                case 1:
                                    if (Logement.PlayerHaveKeyHouse(player, Convert.ToInt32(row["MaisonID"])))
                                    {
                                        if (Convert.ToBoolean(row["Locked"]) == false)
                                        {
                                            API.exported.database.executeQuery("UPDATE Garages SET Locked=1 WHERE ID='" + Convert.ToInt32(row["ID"]) + "'");
                                            API.sendNotificationToPlayer(player, "~r~Tu as verrouillé ce garage.", true);
                                            return;
                                        }
                                        else
                                        {
                                            API.exported.database.executeQuery("UPDATE Garages SET Locked=0 WHERE ID='" + Convert.ToInt32(row["ID"]) + "'");
                                            API.sendNotificationToPlayer(player, "~r~Tu as deverrouillé ce garage.", true);
                                            return;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case "MenuMaisonAchetéeNonProprio":
                    DataTable result2 = API.exported.database.executeQueryWithResult("SELECT * FROM Logements");
                    if (result2.Rows.Count != 0)
                    {
                        foreach (DataRow row in result2.Rows)
                        {
                            Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                            if (player.position.DistanceTo(logpos) < 2)
                            {
                                switch (Convert.ToInt16(args[0]))
                                {
                                    case 0:
                                        if(Convert.ToBoolean(row["Ouvert"]))
                                        {
                                            objplayer.IsOnInt = true;
                                            API.setEntityDimension(player, Convert.ToInt32(row["ID"]));
                                            objplayer.dimension = Convert.ToInt32(row["ID"]);
                                            String app = Convert.ToString(row["model"]);
                                            if (app == "app1")
                                            {
                                                Logement test = new Logement();
                                                API.setEntityPosition(player, test.app1);
                                            }
                                            if (app == "app2")
                                            {
                                                Logement test = new Logement();
                                                API.setEntityPosition(player, test.app2);
                                            }
                                        }
                                        else
                                        {
                                            API.sendNotificationToPlayer(player, "Ce logement est fermé.");
                                        }
                                        break;
                                    case 1:
                                        break;
                                    case 2:
                                        API.sendChatMessageToPlayer(player, String.Format("Nom du Proprietaire : {0}", Convert.ToString(row["proprietaire"])));
                                        break;
                                }
                            }
                        }
                    }
                    break;

                case "MenuMaisonAVendre":
                    DataTable result3 = API.exported.database.executeQueryWithResult("SELECT * FROM Logements");
                    if (result3.Rows.Count != 0)
                    {
                        foreach (DataRow row in result3.Rows)
                        {
                            Vector3 logpos = new Vector3(float.Parse(String.Format("" + row["PosX"])), float.Parse(String.Format("" + row["PosY"])), float.Parse(String.Format("" + row["PosZ"])));
                            if (player.position.DistanceTo(logpos) < 2)
                            {
                                switch (Convert.ToInt16(args[0]))
                                {
                                    case 0:
                                        objplayer.IsOnInt = true;
                                        API.setEntityDimension(player, Convert.ToInt32(row["ID"]));
                                        objplayer.dimension = Convert.ToInt32(row["ID"]);
                                        String app = Convert.ToString(row["model"]);
                                        if (app == "app1")
                                        {
                                            Logement test = new Logement();
                                            API.setEntityPosition(player, test.app1);
                                        }
                                        if (app == "app2")
                                        {
                                            Logement test = new Logement();
                                            API.setEntityPosition(player, test.app2);
                                        }
                                        break;
                                    case 1:
                                        API.sendChatMessageToPlayer(player, String.Format("Prix de ce logement : {0}$", Convert.ToInt32(row["Prix"])));
                                        break;
                                    case 2:
                                        if (objplayer.money >= Convert.ToInt32(row["Prix"]))
                                        {
                                            objplayer.money = objplayer.money - Convert.ToInt32(row["Prix"]);
                                            API.exported.database.executeQuery("UPDATE Logements SET proprietaire = '" + player.name + "' WHERE ID = '" + Convert.ToInt32(row["ID"]) + "'");
                                            ClefInfo clef = new ClefInfo(objplayer.dbid, "Logement", Convert.ToInt32(row["ID"]));
                                            API.sendNotificationToPlayer(player, String.Format("~g~Tu viens d'acquerir ce logement pour {0}$", Convert.ToInt32(row["prix"])));
                                            break;
                                        }
                                        else
                                        {
                                            API.sendChatMessageToPlayer(player, String.Format("Tu n'as pas assez d'argent pour obtenir ce bien immobilier d'une valeur de {0}$", Convert.ToInt32(row["prix"])));
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    break;
                                    
                case "MenuPNJHackeur":
                    if (Hackeur.isRepairePNJ(player))
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (objplayer.money < 900)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour acheter ça.");
                                }
                                else
                                {
                                    int amount = 900;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Ordinateur;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un ordinateur pour " + amount + "~g~$.");
                                }
                                break;
                            case 1:
                                if (objplayer.money < 200)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour acheter ça.");
                                }
                                else
                                {
                                    int amount = 200;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Routeur;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter un routeur pour " + amount + "~g~$.");
                                }
                                break;
                            case 2:
                                if (objplayer.money < 40)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour acheter ça.");
                                }
                                else
                                {
                                    int amount = 40;
                                    var anciennemoney = objplayer.money;
                                    objplayer.money = anciennemoney - amount;
                                    int Item = Inventaire.Puce;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.sendChatMessageToPlayer(player, "~g~Tu viens d'acheter une puce pour " + amount + "~g~$.");
                                }
                                break;
                        }
                    }
                    break;

                case "MenuHackeur":
                    if (API.getEntityData(player, "OrdiHack") == true)
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                API.sendChatMessageToPlayer(player, "~r~En cours de développement.");
                                break;
                            case 1:
                                Random aleatoire = new Random();
                                int resultat = aleatoire.Next(10);
                                if (resultat < 3)
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens de récupérer des données bancaires, tu peux maintenant les ~g~encoder~s~.");
                                    API.setEntityData(player, "donneesbancaires", true);
                                }
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu n'as ~r~pas réussis~s~ à récupérer des données bancaires.");
                                    API.sendChatMessageToPlayer(player, "~r~Attention ~s~la police peut te localiser.");
                                }
                                break;
                            case 2:
                                if (API.getEntityData(player, "donneesbancaires") == true)
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'encoder une carte bancaire, va retirer de l'~g~argent~s~ à un ~b~ATM.");
                                    int Item = Inventaire.FausseCarte;
                                    Inventaire saveitem = new Inventaire();
                                    saveitem.AddItemToPlayerInventaire(player, Item, 1);
                                    API.setEntityData(player, "donneesbancaires", false);
                                }
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as aucunes données bancaires à encoder.");
                                }
                                break;
                        }
                    }
                    break;

                case "MenuExteVeh":
                    if (veh != null)
                    {
                        VehiculeInfo objveh = VehiculeInfo.GetVehicleArroundPlayer(player);
                        if (objveh != null)
                        {
                            switch (Convert.ToInt16(args[0]))
                            {
                                case 0:
                                    if (vehobj.locked == false)
                                    {
                                        if (vehobj.handle.isDoorOpen(4))
                                        {
                                            vehobj.handle.closeDoor(4);
                                        }
                                        else
                                        {
                                            vehobj.handle.openDoor(4);
                                        }
                                    }
                                    else API.sendChatMessageToPlayer(player, "Le véhicule est ~r~fermé~s~.");
                                    break;
                                case 1:
                                    if (Concess.PlayerHaveVehicleKeys(player, objveh.dbid))
                                    {
                                        if (vehobj.locked == false)
                                        {
                                            API.setVehicleLocked(vehobj.handle, true);
                                            vehobj.locked = true;
                                            API.sendNotificationToPlayer(player, "~r~Portières Verrouillées", true);
                                            return;
                                        }
                                        else
                                        {
                                            API.setVehicleLocked(vehobj.handle, false);
                                            vehobj.locked = false;
                                            API.sendNotificationToPlayer(player, "~g~Portières Déverrouillées", true);
                                        }
                                    }
                                    else API.sendChatMessageToPlayer(player, "Tu n'as pas les clefs de ce véhicule.");
                                    break;
                                case 2:
                                    if (vehobj.locked == false)
                                    {
                                        if (vehobj.handle.isDoorOpen(5))
                                        {
                                            vehobj.handle.closeDoor(5);
                                        }
                                        else vehobj.handle.openDoor(5);
                                    }
                                    else API.sendChatMessageToPlayer(player, "Le véhicule est ~r~fermé~s~.");
                                    break;
                                case 3:
                                    if (vehobj.locked == false)
                                    {
                                        CoffreInfo.ShowCoffre(vehobj.coffre, player);
                                    }
                                    else API.sendChatMessageToPlayer(player, "Le véhicule est ~r~fermé~s~.");
                                    break;
                            }
                        }  
                    }
                    break;

                case "KitPistolet":
                    if (Inventaire.GetItemNumberInBDD(player, 16) >= 1)
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.Pistol) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Berretta 92FS.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 16, 1);
                                    API.givePlayerWeapon(player, WeaponHash.Pistol, 0, true, true);
                                }
                                break;
                            case 1:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.CombatPistol) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Glock 18.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 16, 1);
                                    API.givePlayerWeapon(player, WeaponHash.CombatPistol, 0, true, true);
                                }
                                break;
                            case 2:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.Pistol50) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Desert Eagle.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 16, 1);
                                    API.givePlayerWeapon(player, WeaponHash.Pistol50, 0, true, true);
                                }
                                break;
                            case 3:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.SNSPistol) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Heckler & Koch P7.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 16, 1);
                                    API.givePlayerWeapon(player, WeaponHash.SNSPistol, 0, true, true);
                                }
                                break;
                            case 4:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.HeavyPistol) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Colt M1911.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 16, 1);
                                    API.givePlayerWeapon(player, WeaponHash.HeavyPistol, 0, true, true);
                                }
                                break;
                        }
                    }
                    break;

                case "KitPMitr":
                    if (Inventaire.GetItemNumberInBDD(player, 17) >= 1)
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.MicroSMG) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Uzi.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 17, 1);
                                    API.givePlayerWeapon(player, WeaponHash.MicroSMG, 0, true, true);
                                }
                                break;
                            case 1:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.MachinePistol) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un TEC-9.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 17, 1);
                                    API.givePlayerWeapon(player, WeaponHash.MachinePistol, 0, true, true);
                                }
                                break;
                            case 2:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.SMG) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un HK MP5.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 17, 1);
                                    API.givePlayerWeapon(player, WeaponHash.SMG, 0, true, true);
                                }
                                break;
                            case 3:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.CombatPDW) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un SIG MPX.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 17, 1);
                                    API.givePlayerWeapon(player, WeaponHash.CombatPDW, 0, true, true);
                                }
                                break;
                            case 4:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.MiniSMG) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Skorpion.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 17, 1);
                                    API.givePlayerWeapon(player, WeaponHash.MiniSMG, 0, true, true);
                                }
                                break;
                        }
                    }
                    break;

                case "KitPompe":
                    if (Inventaire.GetItemNumberInBDD(player, 18) >= 1)
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.PumpShotgun) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Mossberg 500.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 18, 1);
                                    API.givePlayerWeapon(player, WeaponHash.PumpShotgun, 0, true, true);
                                }
                                break;
                            case 1:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.SawnoffShotgun) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Colt Model 1883.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 18, 1);
                                    API.givePlayerWeapon(player, WeaponHash.SawnoffShotgun, 0, true, true);
                                }
                                break;
                            case 2:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.Musket) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Mousquet.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 18, 1);
                                    API.givePlayerWeapon(player, WeaponHash.Musket, 0, true, true);
                                }
                                break;
                            case 3:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.DoubleBarrelShotgun) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Double-barreled shotgun.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 18, 1);
                                    API.givePlayerWeapon(player, WeaponHash.DoubleBarrelShotgun, 0, true, true);
                                }
                                break;
                        }
                    }
                    break;

                case "KitFusil":
                    if (Inventaire.GetItemNumberInBDD(player, 19) >= 1)
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            case 0:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.AssaultRifle) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un AK-47.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 19, 1);
                                    API.givePlayerWeapon(player, WeaponHash.AssaultRifle, 0, true, true);
                                }
                                break;
                            case 1:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.CarbineRifle) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un AR-15.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 19, 1);
                                    API.givePlayerWeapon(player, WeaponHash.CarbineRifle, 0, true, true);
                                }
                                break;
                            case 2:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.SpecialCarbine) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un G36C.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 19, 1);
                                    API.givePlayerWeapon(player, WeaponHash.SpecialCarbine, 0, true, true);
                                }
                                break;
                            case 3:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.BullpupRifle) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un Type 86S.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 19, 1);
                                    API.givePlayerWeapon(player, WeaponHash.BullpupRifle, 0, true, true);
                                }
                                break;
                            case 4:
                                if (API.getPlayerCurrentWeapon(player) == WeaponHash.CompactRifle) API.sendChatMessageToPlayer(player, "~r~Tu as déjà cette arme en main.");
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "Tu viens d'assembler un AKMSU.");
                                    Inventaire.RemoveItemToPlayerInventaire(player, 19, 1);
                                    API.givePlayerWeapon(player, WeaponHash.CompactRifle, 0, true, true);
                                }
                                break;
                        }
                    }
                    break;

                case "MenuFournisseur":
                    if (objplayer.rangfaction == 6)
                    {
                        DataTable resultatfournisseur = API.exported.database.executeQueryWithResult("SELECT * FROM PlanqueFaction");
                        if (resultatfournisseur.Rows.Count != 0)
                        {
                            foreach (DataRow row in resultatfournisseur.Rows)
                            {
                                int KitPistoletFournisseur = Convert.ToInt32(row["kitpisto"]);
                                int KitPMitrFournisseur = Convert.ToInt32(row["kitpmitr"]);
                                int KitPompeFournisseur = Convert.ToInt32(row["kitpompe"]);
                                int KitFusilFournisseur = Convert.ToInt32(row["kitfusil"]);
                                var anciennebanque = objplayer.bank;
                                switch (Convert.ToInt16(args[0]))
                                {
                                    case 0:
                                        API.sendChatMessageToPlayer(player, "~b~Fournisseur~s~: Les kits te seront livrés d'ici peu à ton entrepôt.");
                                        int NouveauKitPistoletFournisseur = KitPistoletFournisseur + 10;
                                        API.exported.database.executeQuery("UPDATE PlanqueFaction SET kitpisto ='" + NouveauKitPistoletFournisseur + "' WHERE factionid = '" + objplayer.factionid + "'");
                                        objplayer.bank = anciennebanque - Constante.KitPisto;
                                        objplayer.TimerKitArmes = 120; 
                                        break;
                                    case 1:
                                        API.sendChatMessageToPlayer(player, "~b~Fournisseur~s~: Les kits te seront livrés d'ici peu à ton entrepôt.");
                                        int NouveauKitPMitrFournisseur = KitPMitrFournisseur + 10;
                                        API.exported.database.executeQuery("UPDATE PlanqueFaction SET kitpisto ='" + NouveauKitPMitrFournisseur + "' WHERE factionid = '" + objplayer.factionid + "'");
                                        objplayer.bank = anciennebanque - Constante.KitPMitr;
                                        objplayer.TimerKitArmes = 150;
                                        break;
                                    case 2:
                                        API.sendChatMessageToPlayer(player, "~b~Fournisseur~s~: Les kits te seront livrés d'ici peu à ton entrepôt.");
                                        int NouveauKitPompeFournisseur = KitPompeFournisseur + 10;
                                        API.exported.database.executeQuery("UPDATE PlanqueFaction SET kitpisto ='" + NouveauKitPompeFournisseur + "' WHERE factionid = '" + objplayer.factionid + "'");
                                        objplayer.bank = anciennebanque - Constante.KitPompe;
                                        objplayer.TimerKitArmes = 180;
                                        break;
                                    case 3:
                                        API.sendChatMessageToPlayer(player, "~b~Fournisseur~s~: Les kits te seront livrés d'ici peu à ton entrepôt.");
                                        int NouveauKitFusilFournisseur = KitFusilFournisseur + 10;
                                        API.exported.database.executeQuery("UPDATE PlanqueFaction SET kitpisto ='" + NouveauKitFusilFournisseur + "' WHERE factionid = '" + objplayer.factionid + "'");
                                        objplayer.bank = anciennebanque - Constante.KitFusil;
                                        objplayer.TimerKitArmes = 300;
                                        break;
                                }
                            }
                        }
                    }
                    else API.sendChatMessageToPlayer(player, "~b~Fournisseur~s~: Je te connais pas assez pour te livrer, désolé vieux.");
                    break;

                //OnEnterVehicule
                case "MenuVehConcess":
                    VehiculeInfo objvehicle = VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle);
                    var MaxSpeed = API.getVehicleMaxSpeed(objvehicle.model);
                    var MaxSpeedKmh = MaxSpeed * 3.6;
                    switch (Convert.ToInt16(args[0]))
                    {
                        case 0:
                            API.sendChatMessageToPlayer(player, String.Format("Modèle du véhicule : ~b~{0}", API.getVehicleDisplayName(objvehicle.model)));
                            API.sendChatMessageToPlayer(player, "Vitesse maximale : ~b~" + (int)MaxSpeedKmh + "~s~ km/h");
                            API.sendChatMessageToPlayer(player, String.Format("Nombre de places : ~b~{0}", API.getVehicleMaxOccupants(objvehicle.model)));
                            API.sendChatMessageToPlayer(player, String.Format("Prix du véhicule: ~b~{0}$", objvehicle.PrixVente));
                            break;
                        case 1:
                            if (objplayer.money >= objvehicle.PrixVente)
                            {
                                API.sendChatMessageToPlayer(player, "Tu viens d'acheter ce véhicule pour ~g~" + objvehicle.PrixVente + "~s~$.");
                                objplayer.money = objplayer.money - objvehicle.PrixVente;
                                objvehicle.EnVente = false;
                                ClefInfo clef = new ClefInfo(objplayer.dbid, "Vehicule", objvehicle.dbid);
                                API.exported.database.executeQuery("UPDATE Concess SET Vendu ='1' WHERE IDVeh = '"+ objvehicle.dbid +"'");
                                API.exported.database.executeQuery("UPDATE Vehicules SET EnVente ='0' WHERE ID = '" + objvehicle.dbid + "'");
                                objvehicle.EnVente = false;
                                objvehicle.handle.invincible = false;
                                objvehicle.handle.freezePosition = false;
                            }
                            else
                            {
                                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas assez d'argent pour acheter ce vehicule.");
                            }
                            break;
                    }
                    break;

                //touche F1
                case "MenuInteVeh":
                    if (player.isInVehicle == true)
                    {
                        switch (Convert.ToInt16(args[0]))
                        {
                            #region Mettre sa ceinture
                            case 0:
                                if (API.getPlayerSeatbelt(player) == false)
                                {
                                    API.setPlayerSeatbelt(player, true);
                                    API.sendChatMessageToPlayer(player, "Tu as ~g~enfilé ~s~ta ceinture.");
                                }
                                else
                                {
                                    API.setPlayerSeatbelt(player, false);
                                    API.sendChatMessageToPlayer(player, "Tu viens d'~r~enlever ~s~ta ceinture.");
                                }
                                break;
                            #endregion
                            #region Demarrer ou éteindre le moteur
                            case 1:
                                if (VehiculeInfo.GetVehicleInfoByObject(player.vehicle).essence <= 0)
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Le vehicule n'a plus d'essence");
                                    return;
                                }
                                if(Concess.PlayerHaveVehicleKeys(player, VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle).dbid))
                                {
                                    if (API.getVehicleEngineStatus(player.vehicle) == false)
                                    {
                                        API.delay(1000, true, () =>
                                        {
                                            API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                            API.setVehicleEngineStatus(player.vehicle, true);
                                            API.sendChatMessageToPlayer(player, "Tu viens de démarrer ton véhicule.");
                                            return;
                                        });
                                    }
                                    else
                                    {
                                        API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                        API.setVehicleEngineStatus(player.vehicle, false);
                                        API.sendChatMessageToPlayer(player, "Tu viens d'éteindre ton véhicule.");
                                        return;
                                    }
                                }
                                if (objplayer.factionid == VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle).factionid || objplayer.jobid == VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle).jobid)
                                {
                                    if (objplayer.IsJobDuty == true)
                                    {
                                        if (API.getVehicleEngineStatus(player.vehicle) == false)
                                        {
                                            API.delay(1000, true, () =>
                                            {
                                                API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                                API.setVehicleEngineStatus(player.vehicle, true);
                                                API.sendChatMessageToPlayer(player, "Tu viens de démarrer ton véhicule.");
                                                return;
                                            });
                                        }
                                        else
                                        {
                                            API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                            API.setVehicleEngineStatus(player.vehicle, false);
                                            API.sendChatMessageToPlayer(player, "Tu viens d'éteindre ton véhicule.");
                                            return;
                                        }
                                    }
                                    if (objplayer.IsFactionDuty == true)
                                    {
                                        if (API.getVehicleEngineStatus(player.vehicle) == false)
                                        {
                                            API.delay(1000, true, () =>
                                            {
                                                API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                                API.setVehicleEngineStatus(player.vehicle, true);
                                                API.sendChatMessageToPlayer(player, "Tu viens de démarrer ton véhicule.");
                                                return;
                                            });
                                        }
                                        else
                                        {
                                            API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                            API.setVehicleEngineStatus(player.vehicle, false);
                                            API.sendChatMessageToPlayer(player, "Tu viens d'éteindre ton véhicule.");
                                            return;
                                        }
                                    }
                                }
                                if(objplayer.adminlvl > 0)
                                {
                                    if (API.getVehicleEngineStatus(player.vehicle) == false)
                                    {
                                        API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                        API.setVehicleEngineStatus(player.vehicle, true);
                                        API.sendChatMessageToPlayer(player, "Tu viens de démarrer le véhicule.");
                                    }
                                    else
                                    {
                                        API.playPlayerAnimation(player, 0, "veh@van@ds@base", "start_engine");
                                        API.setVehicleEngineStatus(player.vehicle, false);
                                        API.sendChatMessageToPlayer(player, "Tu viens d'éteindre le véhicule.");
                                        return;
                                    }
                                    break;
                                }
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu ne dispose pas des clés de ce vehicule.");
                                    return;
                                }
                            #endregion
                            #region Verrouiller/Déverrouiller les portes
                            case 2:
                                if (player.vehicle.locked == true)
                                {
                                    API.setVehicleLocked(player.vehicle, false);
                                    API.sendNotificationToPlayer(player, "~g~Portières Déverrouillées", true);
                                }
                                else
                                {
                                    API.setVehicleLocked(player.vehicle, true);
                                    API.sendNotificationToPlayer(player, "~g~Portières Verrouillées", true);
                                }
                                break;
                            #endregion
                            #region Garer le vehicule
                            case 3:
                                if(VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle).IDBDDProprio == PlayerInfo.GetPlayerInfoObject(player).dbid)
                                {
                                    if (VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle).Dansgarage != true)
                                    {
                                        DataTable result4 = API.exported.database.executeQueryWithResult("SELECT * FROM Garages");
                                        foreach (DataRow row in result4.Rows)
                                        {
                                            Vector3 gpos = new Vector3(float.Parse(Convert.ToString(row["PosX"])), float.Parse(Convert.ToString(row["PosY"])), float.Parse(Convert.ToString(row["PosZ"])));
                                            if (player.position.DistanceTo(gpos) < 3)
                                            {
                                                if (Logement.PlayerHaveKeyHouse(player, Convert.ToInt32(row["MaisonID"])))
                                                {
                                                    if (Convert.ToInt32(row["IDveh1"]) == -1)
                                                    {
                                                        API.exported.database.executeQuery("UPDATE Garages SET IDveh1 = '" + VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle).dbid + "' WHERE ID = '" + Convert.ToInt32(row["ID"]) + "'");
                                                        VehiculeInfo vehgarage = VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle);
                                                        Client[] occupants = API.getVehicleOccupants(vehgarage.handle);
                                                        foreach (Client occupant in occupants)
                                                        {
                                                            PlayerInfo occupantinfo = PlayerInfo.GetPlayerInfoObject(occupant);
                                                            occupantinfo.seat = API.getPlayerVehicleSeat(occupant);
                                                        }
                                                        API.setEntityPosition(vehgarage.handle, new Vector3(170.9824, -1004.419, -99.53024));
                                                        API.setEntityDimension(vehgarage.handle, Convert.ToInt32(row["ID"]));
                                                        API.setEntityInvincible(vehgarage.handle, true);
                                                        API.setEntityPositionFrozen(vehgarage.handle, true);
                                                        vehgarage.Dansgarage = true;
                                                        foreach (Client occupant in occupants)
                                                        {
                                                            API.setEntityDimension(occupant, Convert.ToInt32(row["ID"]));
                                                            PlayerInfo.GetPlayerInfoObject(occupant).dimension = Convert.ToInt32(row["ID"]);
                                                            API.setPlayerIntoVehicle(occupant, vehgarage.handle, PlayerInfo.GetPlayerInfoObject(occupant).seat);
                                                        }
                                                        return;
                                                    }
                                                    if (Convert.ToInt32(row["IDveh2"]) == -1)
                                                    {
                                                        API.exported.database.executeQuery("UPDATE Garages SET IDveh2 = '" + VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle).dbid + "' WHERE ID = '" + Convert.ToInt32(row["ID"]) + "'");
                                                        VehiculeInfo vehgarage = VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle);
                                                        Client[] occupants = API.getVehicleOccupants(vehgarage.handle);
                                                        foreach (Client occupant in occupants)
                                                        {
                                                            PlayerInfo occupantinfo = PlayerInfo.GetPlayerInfoObject(occupant);
                                                            occupantinfo.seat = API.getPlayerVehicleSeat(occupant);
                                                        }
                                                        API.setEntityPosition(vehgarage.handle, new Vector3(174.6938, -1004.355, -99.50674));
                                                        API.setEntityDimension(vehgarage.handle, Convert.ToInt32(row["ID"]));
                                                        API.setEntityInvincible(vehgarage.handle, true);
                                                        API.setEntityPositionFrozen(player.vehicle, true);
                                                        vehgarage.Dansgarage = true;
                                                        foreach (Client occupant in occupants)
                                                        {
                                                            API.setEntityDimension(occupant, Convert.ToInt32(row["ID"]));
                                                            PlayerInfo.GetPlayerInfoObject(occupant).dimension = Convert.ToInt32(row["ID"]);
                                                            API.setPlayerIntoVehicle(occupant, vehgarage.handle, PlayerInfo.GetPlayerInfoObject(occupant).seat);
                                                        }
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        API.sendChatMessageToPlayer(player, "~r~Ce garage est plein.");
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    API.sendChatMessageToPlayer(player, "~r~Tu n'as pas accès a ce garage.");
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        VehiculeInfo vobj = VehiculeInfo.GetVehicleInfoByNetHandle(player.vehicle);
                                        DataTable result4 = API.exported.database.executeQueryWithResult("SELECT * FROM Garages WHERE IDVeh1='"+ vobj.dbid +"' OR IDVeh2='"+ vobj.dbid +"'");
                                        foreach (DataRow row in result4.Rows)
                                        {
                                            if(Convert.ToInt32(row["IDVeh1"]) == vobj.dbid)
                                            {
                                                API.exported.database.executeQuery("UPDATE Garages SET IDveh1 = '-1' WHERE ID = '" + Convert.ToInt32(row["ID"]) + "'");
                                            }
                                            else
                                            {
                                                API.exported.database.executeQuery("UPDATE Garages SET IDveh2 = '-1' WHERE ID = '" + Convert.ToInt32(row["ID"]) + "'");
                                            }
                                            Client[] occupants = API.getVehicleOccupants(vobj.handle);
                                            foreach (Client occupant in occupants)
                                            {
                                                PlayerInfo occupantinfo = PlayerInfo.GetPlayerInfoObject(occupant);
                                                occupantinfo.seat = API.getPlayerVehicleSeat(occupant);
                                            }
                                            API.setEntityPosition(vobj.handle, new Vector3(Convert.ToDouble(row["PosX"]), Convert.ToDouble(row["PosY"]), Convert.ToDouble(row["PosZ"])));
                                            API.setEntityDimension(vobj.handle, 0);
                                            API.setEntityInvincible(vobj.handle, false);
                                            API.setEntityPositionFrozen(vobj.handle, false);
                                            vobj.Dansgarage = false;
                                            foreach (Client occupant in occupants)
                                            {
                                                API.setEntityDimension(occupant, 0);
                                                PlayerInfo.GetPlayerInfoObject(occupant).dimension = 0;
                                                API.setPlayerIntoVehicle(occupant, vobj.handle, PlayerInfo.GetPlayerInfoObject(occupant).seat);
                                            }
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    API.sendChatMessageToPlayer(player, "~r~Tu n'es pas propriétaire de ce vehicule.");
                                }
                                break;
                            #endregion
                        }
                    }
                    break;

                //touche R
                case "RechargementPistol":
                    foreach (WeaponHash i in Pistol)
                    {
                        if (API.getPlayerCurrentWeapon(player) == i)
                        {
                            if (Inventaire.GetItemNumberInBDD(player, 11) >= 1)
                            {
                                int Balles = API.getPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player));
                                API.setPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player), Balles + 12);
                                Inventaire.RemoveItemToPlayerInventaire(player, 11, 1);
                                API.sendChatMessageToPlayer(player, "Tu viens de ~g~recharger ~s~ton pistolet.");
                            }
                            else
                            {
                                API.sendChatMessageToPlayer(player, "Tu n'as pas de chargeur sur toi.");
                            }
                        }
                    }
                    break;
                case "RechargementSMG":
                    foreach (WeaponHash i in SMG)
                    {
                        if (API.getPlayerCurrentWeapon(player) == i)
                        {
                            if (Inventaire.GetItemNumberInBDD(player, 12) >= 1)
                            {
                                int Balles = API.getPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player));
                                API.setPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player), Balles + 30);
                                Inventaire.RemoveItemToPlayerInventaire(player, 12, 1);
                                API.sendChatMessageToPlayer(player, "Tu viens de ~g~recharger ~s~ton pistolet.");
                            }
                            else
                            {
                                API.sendChatMessageToPlayer(player, "Tu n'as pas de chargeur sur toi.");
                            }
                        }
                    }
                    break;
                case "RechargementRifle":
                    foreach (WeaponHash i in Rifle)
                    {
                        if (API.getPlayerCurrentWeapon(player) == i)
                        {
                            if (Inventaire.GetItemNumberInBDD(player, 13) >= 1)
                            {
                                int Balles = API.getPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player));
                                API.setPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player), Balles + 30);
                                Inventaire.RemoveItemToPlayerInventaire(player, 13, 1);
                                API.sendChatMessageToPlayer(player, "Tu viens de ~g~recharger ~s~ton pistolet.");
                            }
                            else
                            {
                                API.sendChatMessageToPlayer(player, "Tu n'as pas de chargeur sur toi.");
                            }
                        }
                    }
                    break;
                case "RechargementPompe":
                    foreach (WeaponHash i in Pompe)
                    {
                        if (API.getPlayerCurrentWeapon(player) == i)
                        {
                            if (Inventaire.GetItemNumberInBDD(player, 14) >= 1)
                            {
                                int Balles = API.getPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player));
                                API.setPlayerWeaponAmmo(player, API.getPlayerCurrentWeapon(player), Balles + 30);
                                Inventaire.RemoveItemToPlayerInventaire(player, 14, 1);
                                API.sendChatMessageToPlayer(player, "Tu viens de ~g~recharger ~s~ton fusil à pompe.");
                            }
                            else
                            {
                                API.sendChatMessageToPlayer(player, "Tu n'as pas de chargeur sur toi.");
                            }
                        }
                    }
                    break;
            }
        }

        public void EntityEnterColShape(ColShape colshape, NetHandle entity)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(API.getPlayerFromHandle(entity));
            if (colshape.getData("CheckpointAE") == true && objplayer.ConduiteEnCoursAutoEcole == true)
            {
                API.deleteColShape(objplayer.ColshapeAutoEcole);
                API.triggerClientEvent(objplayer.Handle, "DeleteMarkerAE");
                if( objplayer.CheckpointEnCoursAutoEcole == (objplayer.NombreCheckpointAutoEcole - 1))
                {
                    VehiculeInfo.Delete(objplayer.Handle.vehicle);
                    API.sendChatMessageToPlayer(objplayer.Handle, "~g~Félicitation~s~, tu as ~g~réussi~s~ ton épreuve de ~b~conduite~s~ !");
                    objplayer.PermisDeConduire = 12;
                    objplayer.ConduiteEnCoursAutoEcole = false;
                    objplayer.CheckpointEnCoursAutoEcole = 0;
                    objplayer.NombreCheckpointAutoEcole = 0;
                    return;
                }
                objplayer.CheckpointEnCoursAutoEcole += 1;
                DataTable resultAutoEcole = API.shared.exported.database.executeQueryWithResult("SELECT * FROM CheckpointAutoEcole");
                API.triggerClientEvent(objplayer.Handle, "pointconduiteAE", Convert.ToInt32(resultAutoEcole.Rows[objplayer.CheckpointEnCoursAutoEcole]["PosX"]), Convert.ToInt32(resultAutoEcole.Rows[objplayer.CheckpointEnCoursAutoEcole]["PosY"]), Convert.ToInt32(resultAutoEcole.Rows[objplayer.CheckpointEnCoursAutoEcole]["PosZ"]));
                ColShape test = API.createSphereColShape(new Vector3(Convert.ToInt32(resultAutoEcole.Rows[objplayer.CheckpointEnCoursAutoEcole]["PosX"]), Convert.ToInt32(resultAutoEcole.Rows[objplayer.CheckpointEnCoursAutoEcole]["PosY"]), Convert.ToInt32(resultAutoEcole.Rows[objplayer.CheckpointEnCoursAutoEcole]["PosZ"])), 5f);
                test.setData("CheckpointAE", true);
            }
        }
    }
}