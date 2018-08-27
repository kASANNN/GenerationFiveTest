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
    class LogementInfo
    {
        public static List<LogementInfo> LogementList = new List<LogementInfo>();
        public int ID;
        public int IDBDD;
        public string Proprietaire;
        public float Pos;
        public string Model;
        public int Prix;
        public bool Ouvert;
        public int IDBDDLocataire;
        public int IDBDDColocataire;
        public bool EnLocation;
        public int PrixLocation;
        public List<Client> PlayerInside = new List<Client>();

        public LogementInfo(int IDBDD, string Proprio, float Pos, string Model, int Prix, bool Ouvert, int IDBDDLocataire, int IDBDDColocataire, bool EnLocation, int PrixLocation)
        {
            LogementList.Add(this);
            this.ID = LogementList.IndexOf(this);
            this.IDBDD = IDBDD;
            this.Proprietaire = Proprio;
            this.Pos = Pos;
            this.Model = Model;
            this.Prix = Prix;
            this.Ouvert = Ouvert;
            this.IDBDDLocataire = IDBDDLocataire;
            this.IDBDDColocataire = IDBDDColocataire;
            this.EnLocation = EnLocation;
            this.PrixLocation = PrixLocation;
        }

        public static LogementInfo GetProprioByID(int IDLogement)
        {
            foreach (LogementInfo logement in LogementList)
            {
                if (logement.ID == IDLogement)
                {
                    return logement;
                }
            }
            return null;
        }

        public static LogementInfo GetLogementByProprio(string NomCompletProprio)
        {
            foreach (LogementInfo logement in LogementList)
            {
                if (logement.Proprietaire == NomCompletProprio)
                {
                    return logement;
                }
            }
            return null;
        }
    }
}
