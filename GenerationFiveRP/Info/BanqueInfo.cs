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
    class BanqueInfo
    {
        public static List<BanqueInfo> BanqueList = new List<BanqueInfo>();
        public int ID;
        public int IDBDD;
        public Vector3 Pos;

        public BanqueInfo(int IDBDD, Vector3 Pos)
        {
            BanqueList.Add(this);
            this.ID = BanqueList.IndexOf(this);
            this.Pos = Pos;
            this.IDBDD = IDBDD;
        }

        public static BanqueInfo GetBanqueInfoClosePos(Vector3 Pos, float radius)
        {
            foreach (BanqueInfo Banque in BanqueList)
            {
                if (Pos.DistanceTo(Banque.Pos) <= radius) return Banque;
            }
            return null;
        }
    }


}
