using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GenerationFiveRP
{
    public class Inventaire : Script
    {

        //Objets divers

        public static int Cigarettes = 1;
        public static int Briquet = 2;
        public static int Alcool = 3;
        public static int Outils = 4;
        public static int Scie = 5;
        public static int Ordinateur = 6;
        public static int Routeur = 7;
        public static int Puce = 8;
        public static int FausseCarte = 9;
        public static int RouteurPuce = 10;
        public static int ChargeurPistol = 11;
        public static int ChargeurSMG = 12;
        public static int ChargeurRifle = 13;
        public static int ChargeurPompe = 14;
        public static int Pince = 15;
        public static int KitPistolet = 16;
        public static int KitPMitrailleur = 17;
        public static int KitPompe = 18;
        public static int KitFusil = 19;

        public void AddItemToPlayerInventaire(Client player, int Item, int nombre)
        {
            int IDinventaire = GetPlayerIDinventaire(player);
            if (PlayerHaveItemInBDD(player, Item))
            {
                int NombreBDD = GetItemNumberInBDD(player, Item);
                int newnombre = NombreBDD + nombre;
                API.exported.database.executeQuery("UPDATE Inventaire SET Nombre='" + newnombre + "' WHERE IDinventaire = '" + IDinventaire + "' AND Item = '" + Item + "'");
                return;
            }
            else
            {
                API.exported.database.executeQuery("INSERT INTO Inventaire VALUE ('', '1', '" + IDinventaire + "', '" + Item + "', '" + nombre + "')");
                return;
            }
        }

        public static void RemoveItemToPlayerInventaire(Client player, int Item, int nombre)
        {
            int IDinventaire = GetPlayerIDinventaire(player);
            if (PlayerHaveItemInBDD(player, Item))
            {
                int NombreBDD = GetItemNumberInBDD(player, Item);
                int newnombre = NombreBDD - nombre;
                API.shared.exported.database.executeQuery("UPDATE Inventaire SET Nombre='" + newnombre + "' WHERE IDinventaire = '" + IDinventaire + "' AND Item = '" + Item + "'");
                return;
            }
            else
            {
                API.shared.exported.database.executeQuery("INSERT INTO Inventaire VALUE ('', '1', '" + IDinventaire + "', '" + Item + "', '" + nombre + "')");
                return;
            }
        }

        public static int GetPlayerIDinventaire(Client player)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Utilisateur WHERE PlayerName = '" + player.name + "'");
            foreach (DataRow row in result.Rows)
            {
                int IDinventaire = Convert.ToInt32(row["ID"]);
                return IDinventaire;
            }
            return 0;
        }

        public static bool PlayerHaveItemInBDD(Client player, int Item)
        {
            int IDinventaire = GetPlayerIDinventaire(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Inventaire WHERE IDinventaire = '" + IDinventaire + "' AND Item = '" + Item + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static int GetItemNumberInBDD(Client player, int Item)
        {
            int IDinventaire = GetPlayerIDinventaire(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Inventaire WHERE IDinventaire = '" + IDinventaire + "' AND Item = '" + Item + "'");
            foreach (DataRow row in result.Rows)
            {
                int nombre = Convert.ToInt32(row["Nombre"]);
                return nombre;
            }
            return -1;
        }

        [Command("testinv")]
        public void Testinv(Client player)
        {
            int IDinventaire = GetPlayerIDinventaire(player);
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Inventaire WHERE IDinventaire = '" + IDinventaire + "'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    API.sendChatMessageToPlayer(player, String.Format("Item = {0}, Nombre = {1}", row["Item"], row["Nombre"]));
                }
            }
            else API.sendChatMessageToPlayer(player, "il n'y a rien dans ton inventaire");
        }

    }


}
