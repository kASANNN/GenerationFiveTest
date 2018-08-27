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
    class ClefInfo
    {
        public static List<ClefInfo> ClefList = new List<ClefInfo>();
        public int ID;
        public int PlayerIDBDD;
        public string Type;
        public int ObjetID;

        public ClefInfo(int PLayerIDBDD, string Type, int ObjetID)
        {
            ClefList.Add(this);
            this.ID = ClefList.IndexOf(this);
            this.Type = Type;
            this.ObjetID = ObjetID;
            this.PlayerIDBDD = -1;
        }

        public static List<ClefInfo> GetAllPlayerKeys(int PlayerIDBDD)
        {
            var listclef = new List<ClefInfo>();
            foreach (ClefInfo clef in ClefList)
            {
                if (clef.PlayerIDBDD == PlayerIDBDD)
                {
                    listclef.Add(clef);
                }
            }
            return listclef;
        }

        public static List<ClefInfo> GetAllObjetIDKeys(int objetID)
        {
            var listclef = new List<ClefInfo>();
            foreach(ClefInfo clef in ClefList)
            {
                if(clef.ObjetID == objetID)
                {
                    listclef.Add(clef);
                }
            }
            return listclef;
        }

        public static List<ClefInfo> GetPlayerKeysByType(int PlayerIDBDD, string type)
        {
            var listclef = new List<ClefInfo>();
            foreach(ClefInfo clef in ClefList)
            {
                if(clef.PlayerIDBDD == PlayerIDBDD && clef.Type == type)
                {
                    listclef.Add(clef);
                }
            }
            return listclef;
        }

        public static List<ClefInfo> GetPlayerKeysByObjetID(int PlayerIDBDD, int ObjetID)
        {
            var listclef = new List<ClefInfo>();
            foreach (ClefInfo clef in ClefList)
            {
                if (clef.PlayerIDBDD == PlayerIDBDD && clef.ObjetID == ObjetID)
                {
                    listclef.Add(clef);
                }
            }
            return listclef;
        }
    }
}
