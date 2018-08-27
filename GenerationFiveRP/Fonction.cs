using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Security.Cryptography;

namespace GenerationFiveRP
{
    public class Fonction : Script
    {
        public Fonction()
        {
            API.onClientEventTrigger += ClientEventTrigger;
        }
        public void ClientEventTrigger(Client sender, string eventName, params object[] args)
        {
            if (eventName == "RetirerATM")
            {
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(sender);
                if (objplayer.bank < (int)args[0])
                {
                    API.sendChatMessageToPlayer(sender, "~r~Tu n'as pas assez d'argent sur ton compte en banque.");
                    objplayer.IdATM = -1;
                }
                else if (ATMInfo.GetATMInfoById(objplayer.IdATM).argent < (int)args[0])
                {
                    API.sendChatMessageToPlayer(sender, "~r~Nous sommes désolé, cet ATM ne dispose pas de cette somme.");
                }
                else
                {
                    objplayer.bank -= (int)args[0];
                    objplayer.money += (int)args[0];
                    ATMInfo.GetATMInfoById(objplayer.IdATM).argent -= (int)args[0];
                    API.sendChatMessageToPlayer(sender, "~g~Tu viens de retirer " + (int)args[0] + "$.");
                    API.triggerClientEvent(sender, "update_money_display", objplayer.money);
                    objplayer.IdATM = -1;
                }
            }
            if (eventName == "ConsulterATM")
            {
                PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(sender);
                API.triggerClientEvent(sender, "retourConsulterATM", objplayer.bank);
            }
        }

        public static string RemoveUnderscore(string pseudo)
        {
            return pseudo.Replace("_", " ");
        }

        public static void SendCloseMessage(Client player, float radius, string sender, string msg)
        {
             List<PlayerInfo> joueurs = PlayerInfo.PlayerList;
            foreach(PlayerInfo joueur in joueurs)
            {
                if(joueur.Handle.position.DistanceTo(player.position) <= radius)
                {
                    API.shared.sendChatMessageToPlayer(joueur.Handle, sender, msg);
                }
            }
        }
        public static bool IsRoleplayName(string name)
        {
            string pattern = "^([A-Z][a-z]+_[A-Z][a-z]+)$";
            return System.Text.RegularExpressions.Regex.IsMatch(name, pattern);
        }
        public static string ConvertSHA256(string value)
        {
            SHA256 sha = SHA256.Create();
            byte[] data = sha.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static bool isArmurerieCivil(Client player)
        {
            if (player.position.DistanceTo(new Vector3(251.97, -50.09469, 69.94105)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(-661.8865, -934.9248, 21.82922)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(841.753, -1033.951, 28.19487)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(809.9454, -2157.674, 29.61901)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(22.64655, -1106.974, 29.79702)) < 2)
            {
                return true;
            }
            return false;
        }

        public static bool IsPlayerInFaction(PlayerInfo objplayer, string factionname, bool message)
        {
            switch (factionname)
            {
                case "Gardien": //gardien de prison
                    if (objplayer.factionid == Constante.Faction_Gardien)
                        return true;
                    else if (message)
                        API.shared.sendChatMessageToPlayer(objplayer.Handle, Constante.PasGardien);
                    break;
                case "Police":
                    if (objplayer.factionid == Constante.Faction_Police)
                        return true;
                    else if (message)
                        API.shared.sendChatMessageToPlayer(objplayer.Handle, Constante.PasLSPD);
                    break;
                case "Medecin":
                    if (objplayer.factionid == Constante.Faction_Medecin)
                        return true;
                    else if (message)
                        API.shared.sendChatMessageToPlayer(objplayer.Handle, Constante.PasEMS);
                    break;
                case "...":
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
