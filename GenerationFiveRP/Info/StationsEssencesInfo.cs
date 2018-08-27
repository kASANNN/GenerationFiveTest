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
    class StationsEssencesInfo
    {
        public static List<StationsEssencesInfo> StationsList = new List<StationsEssencesInfo>();
        public int ID;
        public int IDBDD;
        public TextLabel textlabel;
        public float PosX;
        public float PosY;
        public float PosZ;
        public int Stockage;
        public int Proprio;
        public int Argents;

        public StationsEssencesInfo(int IDBDD, float PosX, float PosY, float PosZ, int Stockage, int Proprio, int Argents)
        {
            StationsList.Add(this);
            this.ID = StationsList.IndexOf(this);
            this.IDBDD = IDBDD;
            this.PosX = PosX;
            this.PosY = PosY;
            this.PosZ = PosZ;
            this.Stockage = Stockage;
            this.Proprio = Proprio;
            this.Argents = Argents;
            this.textlabel = new TextLabelInfo("Station n°~g~" + this.ID + " ~s~| Stockage :~b~ " + this.Stockage + "~s~L", new Vector3(PosX, PosY, PosZ), 50f, 0.4f, true).handle;
        }

        public static StationsEssencesInfo GetStationInfoByID(int Stationid)
        {
            foreach (StationsEssencesInfo station in StationsList)
            {
                if (station.ID == Stationid) return station;
            }
            return null;
        }

        public static StationsEssencesInfo GetStationInfoByIDBDD(int Stationidbdd)
        {
            foreach (StationsEssencesInfo station in StationsList)
            {
                if (station.IDBDD == Stationidbdd) return station;
            }
            return null;
        }

        public static StationsEssencesInfo GetStationInfoByPos(Vector3 pos)
        {
            foreach (StationsEssencesInfo station in StationsList)
            {
                if (new Vector3(station.PosX, station.PosY, station.PosZ).DistanceTo(pos) < 2) return station;
            }
            return null;
        }
    }
}
