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
    public class Money : Script
    {
        public const int ATM_ROOT = 18;
        public const int ATM_TRANSFER = 19;

        //Commandes ATM

        [Command("tpbanque", "~y~UTILISATION: ~w~/tpbanque")]
        public void tpbanque(Client player)
        {
            Vector3 tppos = new Vector3(-31.45808, -744.6152, 32.80514);
            API.setEntityPosition(player, tppos);
        }
        [Command("tpconv", "~y~UTILISATION: ~w~/tpconv")]
        public void tpconv(Client player)
        {
            Vector3 tppos = new Vector3(496.4382, -1417.271, 29.29212);
            API.setEntityPosition(player, tppos);
        }

        //Paye + level

        public void onStart()
        {
            API.delay(1800000, false, () =>
            {
                foreach (Client player in API.getAllPlayers())
                {
                    PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                    objplayer.level = objplayer.level + 1;
                    int amount = objplayer.level * 70;
                    objplayer.bank = objplayer.bank + amount;
                    API.sendPictureNotificationToPlayer(player, "De: Department of Social Security \n Montant: $" + amount + "- \n Solde: $" + objplayer.bank.ToString(), "CHAR_BANK_MAZE", 1, 1, "Maze Bank Co.", "TRANSFERT RECU");
                }

            });

            API.delay(450000, true, () =>
            {
                foreach (Client player in API.getAllPlayers())
                {
                    PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
                    objplayer.level = objplayer.level + 1;
                    int amount = objplayer.level * 70;
                    objplayer.bank = objplayer.bank + amount;
                    API.sendPictureNotificationToPlayer(player, "De: Department of Social Security \n Montant: $" + amount + "- \n Solde: $" + objplayer.bank.ToString(), "CHAR_BANK_MAZE", 1, 1, "Maze Bank Co.", "TRANSFERT RECU");
                }

            });
        }
    }
}
