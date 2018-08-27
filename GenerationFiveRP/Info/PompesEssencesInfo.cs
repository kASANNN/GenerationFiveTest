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
    class PompesEssencesInfo
    {
        public static List<PompesEssencesInfo> PompesList = new List<PompesEssencesInfo>();
        public int ID;
        public int IDBDDStation;
        public int IDBDD;
        public float PosX;
        public float PosY;
        public float PosZ;
        public TextLabel textlabel; 

        public PompesEssencesInfo(int IDBDD, int IDBDDStation, float PosX, float PosY, float PosZ)
        {
            PompesList.Add(this);
            this.ID = PompesList.IndexOf(this);
            this.IDBDD = IDBDD;
            this.IDBDDStation = IDBDDStation;
            this.PosX = PosX;
            this.PosY = PosY;
            this.PosZ = PosZ;
            this.textlabel = new TextLabelInfo("Pompe n°~g~" + this.ID + "~s~ Touche E", new Vector3(PosX, PosY, PosZ), 30f, 0.4f, true).handle;
        }

        public static PompesEssencesInfo GetPompeInfoByID(int Pompeid)
        {
            foreach (PompesEssencesInfo station in PompesList)
            {
                if (station.ID == Pompeid) return station;
            }
            return null;
        }

        public static PompesEssencesInfo GetPompeInfoByPos(Vector3 pos)
        {
            foreach(PompesEssencesInfo pompe in PompesList)
            {
                if (new Vector3(pompe.PosX, pompe.PosY, pompe.PosZ).DistanceTo(pos) < 2) return pompe;
            }
            return null;
        }

    }
}
