using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GenerationFiveRP
{
    public static class SystemePaye
    {
        public static void CuntdownPayday(object sender, ElapsedEventArgs e)
        {
            List<Client> Cuntdown = API.shared.getAllPlayers();
            foreach (Client target in Cuntdown)
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfoObject(target);
                objtarget.cuntdownpaye = objtarget.cuntdownpaye - 1;
                if (objtarget.TimerKitArmes >= 1) objtarget.TimerKitArmes = objtarget.TimerKitArmes - 1;
                if (objtarget.cuntdownpaye <= 0 & objtarget.Logged == true & objtarget.Spawned == true)
                {
                    API.shared.sendChatMessageToPlayer(target, "~b~—————————— Relevé de compte ——————————");
                    API.shared.sendChatMessageToPlayer(target, "Solde de ton compte en banque avant le virement : ~g~" + objtarget.bank + "~s~$.");
                    API.shared.sendChatMessageToPlayer(target, "Montant reçu de la part de la mairie : ~g~" + Constante.Payday + "~s~$.");
                    objtarget.bank = objtarget.bank + Constante.Payday;
                    if (objtarget.pendingpaye != 0)
                    {
                        API.shared.sendChatMessageToPlayer(target, "Montant perçu grâce à ton job : ~g~" + objtarget.pendingpaye + "~s~$.");
                        objtarget.bank = objtarget.bank + objtarget.pendingpaye;
                        objtarget.pendingpaye = 0;
                    }
                    API.shared.sendChatMessageToPlayer(target, "Montant des taxes dues à la mairie : ~r~" + Constante.Taxes + "~s~$.");
                    objtarget.bank = objtarget.bank - Constante.Taxes;
                    //API.shared.sendChatMessageToPlayer(target, "Montant du loyer de ton habitation : ~r~" + PrixLoyer + "~s~$.");
                    //objtarget.bank = objtarget.bank - PrixLoyer;
                    if(AmendeInfo.PlayerHasAmende(target))
                    {
                        int pendingamende = AmendeInfo.GetPlayerAmendeMontantTotal(target);
                        if (pendingamende >= 1000)
                        {
                            API.shared.sendChatMessageToPlayer(target, "Montant des amendes impayées : ~r~" + pendingamende + "~s~$.");
                            objtarget.bank -= pendingamende;
                            AmendeInfo.DeleteAllForPlayer(target);
                            API.shared.exported.database.executeQuery("DELETE FROM UtilisateurAmende WHERE PlayerName='" + objtarget.PlayerName +"'");
                        }
                    }
                    API.shared.sendChatMessageToPlayer(target, "Ton nouveau solde en banque est de : ~g~" + objtarget.bank + "~s~$.");
                    API.shared.sendChatMessageToPlayer(target, "~b~—————————————————————————————");
                    objtarget.cuntdownpaye = 60;
                }
            }
        }
    }
}