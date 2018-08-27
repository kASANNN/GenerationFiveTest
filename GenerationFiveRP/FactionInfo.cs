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

namespace GenerationFiveRP
{
    public class FactionInfo
    {
        public static List<FactionInfo> FactionList = new List<FactionInfo>();
        public int ID;
        public string Nom;
        public int IDScript1;
        public int IDScript2;
        public int[] IDMembres;
        public bool HasRadio;

        

        public FactionInfo(int ID, string nom, int IDScript1, int IDScript2)
        {
            this.ID = ID;
            FactionList.Add(this);
            this.Nom = nom;
            this.IDScript1 = IDScript1;
            this.IDScript2 = IDScript2;
            this.HasRadio = true;
            API.shared.consoleOutput("Creation faction : " + this.Nom + " ID : " + this.ID);
        }

        public static void Delete(int ID)
        {
            FactionInfo Factionobj = GetFactionInfoById(ID);
            FactionList.Remove(Factionobj);
            Factionobj = null;
        }

        public static FactionInfo GetFactionInfoById(int IDFaction) //Return VehiculeInfo à partir de l'id
        {
            foreach (FactionInfo Faction in FactionList)
            {
                if (Faction.ID == IDFaction) return Faction;
            }
            return null;
        }
    }
}
