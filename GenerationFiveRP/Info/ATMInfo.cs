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
    class ATMInfo
    {
        public static List<ATMInfo> ATMList = new List<ATMInfo>();
        public int ID;
        public int IDBDD;
        public Vector3 Pos;
        public int Dimension;
        public int argent;
        public Blip blip;

        public ATMInfo(int IDBDD, Vector3 Pos, int Dimension, int argent)
        {
            ATMList.Add(this);
            this.ID = ATMList.IndexOf(this);
            this.Pos = Pos;
            this.argent = argent;
            this.IDBDD = IDBDD;
            this.Dimension = Dimension;
            this.blip = API.shared.createBlip(Pos, Dimension);
            API.shared.setBlipSprite(this.blip, 500);
            API.shared.setBlipColor(this.blip, 2);
            API.shared.setBlipTransparency(this.blip, 125);
            API.shared.setBlipShortRange(this.blip, true);
        }

        public static ATMInfo GetATMInfoClosePos(Vector3 Pos, float radius)
        {
            foreach (ATMInfo ATM in ATMList)
            {
                if (Pos.DistanceTo(ATM.Pos) <= radius) return ATM;
            }
            return null;
        }

        public static ATMInfo GetATMInfoById(int ID)
        {
            foreach (ATMInfo ATM in ATMList)
            {
                if (ID == ATM.ID) return ATM;
            }
            return null;
        }
    }


}
