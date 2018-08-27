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
    public class CommandesMedecin : Script
    {
        [Command("reanimer", "~p~MEDECIN: ~s~/reanimer [IdOuPartieDuNom]")]
        public void Reanimer(Client player, string idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (player.position.DistanceTo(API.getEntityPosition(target.Handle)) >= 2)
            {
                API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                return;
            }
            else if (!Fonction.IsPlayerInFaction(objplayer, "Medecin", true))
                return;
            else
            {
                API.stopPlayerAnimation(target.Handle);
                var anciennebank = target.bank;
                target.bank = anciennebank - Constante.PrixReaEMS;
                var PayeEMS = Constante.PrixReaEMS / 2;
                API.sendChatMessageToPlayer(target.Handle, "Tu viens d'être réanimer par un médecin, tu as régler la somme de ~g~" + Constante.PrixReaEMS + "~s~$.");
                API.sendChatMessageToPlayer(player, "Tu viens de réanimer cette personne, tu ajoutes ~g~" + PayeEMS + "~s~$ sur ta prochaine paye.");
                var PayeEnAttente = objplayer.pendingpaye;
                objplayer.pendingpaye = PayeEnAttente + PayeEMS;
                target.IsDead = false;
                API.setPlayerHealth(player, 50);
            }
        }

        [Command("soigner", "~p~MEDECIN: ~s~/soigner [IdOuPartieDuNom]")]
        public void Soigner(Client player, string idOrName)
        {
            PlayerInfo target = PlayerInfo.GetPlayerInfotByIdOrName(idOrName);
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (player.position.DistanceTo(API.getEntityPosition(target.Handle)) >= 2)
            {
                API.sendChatMessageToPlayer(player, Constante.TuEsTropLoin);
                return;
            }
            else if (!Fonction.IsPlayerInFaction(objplayer, "Medecin", true))
                return;
            else
            {
                var anciennebank = target.bank;
                target.bank = anciennebank - Constante.PrixSoinEMS;
                var PayeEMS = Constante.PrixSoinEMS / 2;
                API.sendChatMessageToPlayer(target.Handle, "Tu viens d'être soigner par un médecin, tu as régler la somme de ~g~" + Constante.PrixSoinEMS + "~s~$.");
                API.sendChatMessageToPlayer(player, "Tu viens de soigner cette personne, tu ajoutes ~g~" + PayeEMS + "~s~$ sur ta prochaine paye.");
                var PayeEnAttente = objplayer.pendingpaye;
                objplayer.pendingpaye = PayeEnAttente + PayeEMS;
                API.setPlayerHealth(player, 100);
            }
        }
    }
}