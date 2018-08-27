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
    public class Factions : Script
    {
        public Factions()
        {
            API.onResourceStart += onStart;
        }

        public void onStart()
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Factions");
            for (var i = 0; i < result.Rows.Count; i++)
            {
                FactionInfo objfaction = new FactionInfo(Convert.ToInt32(result.Rows[i]["ID"]), Convert.ToString(result.Rows[i]["Nom"]), Convert.ToInt32(result.Rows[i]["IDScript1"]), Convert.ToInt32(result.Rows[i]["IDScript2"]));
            }
        }

        [Command("creerfaction", "~y~UTILISATION: ~w~/creerfaction [Nom de la faction]", GreedyArg = true)]
        public void creerfaction(Client player, String NomFaction)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if(FactionExiste(NomFaction))
                {
                    API.sendChatMessageToPlayer(player, "~r~Une facion portant le même noms existe déjà.(" + NomFaction +")");
                    return;
                }
                else
                {
                    AddFaction(NomFaction);
                    API.sendChatMessageToPlayer(player, "~g~Tu viens de creer une faction.(" + NomFaction + ")");
                }
            }
        }

        [Command("deletefaction", "~y~UTILISATION: ~w~/deletefaction [IDFaction]")]
        public void deletefaction(Client player, int IDFaction)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                if (FactionExiste(GetFactionNameByID(IDFaction)))
                {
                    RemoveFaction(GetFactionNameByID(IDFaction));
                    API.sendChatMessageToPlayer(player, "~g~La faction a bien été supprimée.");
                    return;
                }
                else API.sendChatMessageToPlayer(player, "~r~La faction indiquée n'existe pas.");
            }
        }

        [Command("fsetscript", "~y~UTILISATION: ~w~/fsetscript [ID Faction] [Slot 1 ou 2] [ID Script]")]
        public void AddScriptFaction(Client player, int IDFaction, int slot, int IDScript)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                FactionInfo objfaction = FactionInfo.GetFactionInfoById(IDFaction);
                if (objfaction == null)
                {
                    API.sendChatMessageToPlayer(player, "~r~Cette faction n'existe pas.");
                    return;
                }
                if (slot != 1 || slot != 2)
                {
                    API.sendChatMessageToPlayer(player, "~r~Le numero de slot doit etre 1 ou 2.");
                    return;
                }
                else
                {

                    if (slot == 1)
                    {
                        objfaction.IDScript1 = IDScript;
                    }
                    else
                    {
                        objfaction.IDScript2 = IDScript;
                    }
                }
            }
        }

        [Command("fsetradio", "~y~UTILISATION: ~w~/fsetscript [ID Faction]")]
        public void AddRadioFaction(Client player, int IDFaction)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 5)
            {
                API.sendChatMessageToPlayer(player, Constante.PasAdmin);
            }
            else
            {
                FactionInfo objfaction = FactionInfo.GetFactionInfoById(IDFaction);
                if (objfaction == null)
                {
                    API.sendChatMessageToPlayer(player, "~r~Cette faction n'existe pas.");
                    return;
                }
                if(objfaction.HasRadio)
                {
                    objfaction.HasRadio = false;
                    API.sendChatMessageToPlayer(player, "~r~Tu viens de retiré la radio de cette faction.");
                }
                else
                {
                    objfaction.HasRadio = true;
                    API.sendChatMessageToPlayer(player, "~r~Tu viens d'autorisé la radio dans cette faction.");
                }
            }
        }

        public bool FactionExiste(String NomFaction)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Factions WHERE Nom = '" + NomFaction + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            else return false;
        }

        public void AddFaction(String NomFaction)
        {
            API.shared.exported.database.executeQuery("INSERT INTO Factions VALUES ('','" + NomFaction + "')");
        }

        public void RemoveFaction(String Nomfaction)
        {
            API.exported.database.executeQuery("DELETE * FROM Factions WHERE Nom ='" + Nomfaction + "'");
        }

        public string GetFactionNameByID(int IDFaction)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Factions WHERE ID = '" + IDFaction + "'");
            foreach(DataRow row in result.Rows)
            {
                return Convert.ToString(row["Nom"]);
            }
            return "Aucun";
        }

        public void AddScriptToFaction(int IDFaction, int Slot, int IDScript)
        {
            FactionInfo objfaction = FactionInfo.GetFactionInfoById(IDFaction);
            if (objfaction == null) return;
            if (Slot == 1)
            {
                API.shared.exported.database.executeQuery("UPDATE Factions SET IDScript1 ='" + IDScript + "' WHERE ID = '" + objfaction.ID + "'");
                objfaction.IDScript1 = IDScript;
            }
            if (Slot == 2)
            {
                API.shared.exported.database.executeQuery("UPDATE Factions SET IDScript2 ='" + IDScript + "' WHERE ID = '" + objfaction.ID + "'");
                objfaction.IDScript2 = IDScript;
            }
            else return;
        }

        public bool IfFactionAccesScript(int IDFaction, int IDScript)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Factions WHERE ID = '" + IDFaction + "'");
            foreach (DataRow row in result.Rows)
            {
                if (Convert.ToInt32(row["IDScript1"]) == IDScript || Convert.ToInt32(row["IDScript2"]) == IDScript)
                {
                    return true;
                }
            }
            return false;
        }

        public void SendMessageToFaction(int factionid, string message, string couleur)
        {
            List<Client> PlayerDuty = API.getAllPlayers();
            foreach (Client target in PlayerDuty)
            {
                PlayerInfo objtarget = PlayerInfo.GetPlayerInfoObject(target);
                if (objtarget.factionid == factionid && objtarget.IsFactionDuty == true)
                {
                    API.shared.sendChatMessageToPlayer(target, couleur + message);
                }
            }
            return;
        }

    }
}
