using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Data;
using System.Collections.Generic;


namespace GenerationFiveRP
{
    public class CoffreInfo
    {
        public static List<CoffreInfo> CoffreList = new List<CoffreInfo>();
        public static List<ItemInfo> ItemInfoList = new List<ItemInfo>();
        public int Type;
        public int Id;

        //Creer des log pour les item et le coffre

        public CoffreInfo(int type)
        {
            this.Type = type;
            //insert item in db and get / set id
            CoffreList.Add(this);
            this.Id = CoffreList.IndexOf(this);
            string requete = "INSERT INTO Coffre SET id={this.Id}, type={type};";
            API.shared.exported.database.executeQuery(requete);
        }

        public void DeleteCoffre(CoffreInfo coffre)
        {
            //Delete les items
        }

        public static void ShowCoffre(CoffreInfo coffre, Client player)
        {
            API.shared.triggerClientEvent(player, "ShowCoffre");

        }


        //load coffre au demarrage du serveur

        //load coffre vehicule
        //load coffre ...

    }
}
