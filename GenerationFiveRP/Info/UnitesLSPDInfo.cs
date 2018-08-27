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
    class UnitesLSPDInfo
    {
        public static List<UnitesLSPDInfo> UnitesList = new List<UnitesLSPDInfo>();
        public int ID;
        public List<Client> Membres = new List<Client>(3);
        public Vehicle Vehicule;
        public string Status;

        public UnitesLSPDInfo(Client player, Vehicle Vehicule)
        {
            UnitesList.Add(this);
            this.ID = UnitesList.IndexOf(this);
            this.Membres.Add(player);
            this.Vehicule = Vehicule;
            this.Status = "Stand-By";
        }

        public static UnitesLSPDInfo GetUniteLSPDInfoByID(int ID)
        {
            foreach (UnitesLSPDInfo unite in UnitesList)
            {
                if (unite.ID == ID) return unite;
            }
            return null;
        }

        public static UnitesLSPDInfo GetUniteLSPDInfoByMembre(Client player)
        {
            
            foreach(UnitesLSPDInfo unite in UnitesList)
            {
                for (int i = 0; i < unite.Membres.Count; i++)
                {
                    if (unite.Membres[i] == player) return unite;
                }
            }
            return null;
        }

        public static UnitesLSPDInfo GetuniteLSPDInfoByVehicle(Vehicle Vehicule)
        {
            foreach(UnitesLSPDInfo unite in UnitesList)
            {
                if (unite.Vehicule == Vehicule) return unite;
            }
            return null;
        }

        public static string GetUniteName(int ID)
        {
            foreach(UnitesLSPDInfo unite in UnitesList)
            {
                if(unite.ID == ID)
                {
                    string name = String.Format("[{0}-", (ID + 1));
                    if(unite.Membres.Count == 1)
                    {
                        name += "Lincoln-";
                    }
                    if (unite.Membres.Count == 2)
                    {
                        name += "Adams-";
                    }
                    if (unite.Membres.Count == 3)
                    {
                        name += "Xray-";
                    }
                    name += String.Format("{0}]", VehiculeInfo.GetVehicleInfoByObject(unite.Vehicule).ID);
                    return name;
                }
            }
            return null;
        }
    }
}
