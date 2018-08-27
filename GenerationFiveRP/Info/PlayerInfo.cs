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
    public class PlayerInfo
    {
        public static List<PlayerInfo> PlayerList = new List<PlayerInfo>();
        public string PlayerName;
        public Client Handle;
        public bool Logged;
        public int id;
        public int dbid;
        public int idDBCompte;
        public int level;
        public bool Spawned;
        public int adminlvl { get; set; }
        public int bank { get; set; }
        public int money { get; set; }
        public int cuntdownpaye { get; set; }
        public int pendingpaye { get; set; }
        public int sante { get; set; }
        public int armure { get; set; }
        public int jobid { get; set; }
        public int factionid { get; set; }
        public int rangfaction { get; set; }
        public int entrepriseid { get; set; }
        public int rangentreprise { get; set; }
        public bool IsOnPlanqueArmes { get; set; }
        public bool IsOnPlanqueDrogues { get; set; }
        public int TimerKitArmes { get; set; }
        /* Situation Géographique */
        public Vector3 position = new Vector3();
        public Vector3 rotation = new Vector3();
        public bool IsOnInt { get; set; }
        public int dimension { get; set; }
        /* ---------*/
        public bool IsMenotter { get; set; }
        public bool IsDead { get; set; }
        public int sexe;
        public bool IsJobDuty;
        public bool IsFactionDuty;
        public bool sacbanque; //si il a un sac de banque sur lui
        public bool retourconv; //si il est sur le retour du job convoyeur
        public bool rappel;
        public bool eappel;
        public bool EnAppel;
        public Client Correspondant;
        public DateTime TimeDebutAppel;
        public int NumeroAppelRecu;
        public NetHandle ObjTel;
        public bool DemandeLocation;
        public bool DemandeColocation;
        public int IDLogementLocation;
        public int IDLogementColocation;
        public Client ClientDemandeMaison;
        public bool freeze;
        public bool IrcActif;
        public int seat;
        public int addquestion;
        public int IDBDDquestion;
        public int QuestionAutoEcole;
        public List<int> OrdreQuestionAutoEcole = new List<int>();
        public int QuestionEnCours;
        public bool DansQuestionnaire;
        public int BonneReponse;
        public int NombreCheckpointAutoEcole;
        public int CheckpointEnCoursAutoEcole;
        public bool ConduiteEnCoursAutoEcole;
        public ColShape ColshapeAutoEcole;
        public bool CodeDeLaRoute;
        public int PermisDeConduire { get; set; }
        public bool BaliseLSPD;
        public int IdATM;
        public int IdBanque;

        public PlayerInfo(Client player)
        {            
            this.PlayerName = player.name;
            this.Handle = player;
            this.Logged = false;
            this.EnAppel = false;
            this.rappel = false;
            this.eappel = false;
            this.CodeDeLaRoute = false;
            PlayerInfo.PlayerList.Add(this);
            this.id = PlayerList.IndexOf(this);
            this.dbid = -1;
            this.level = 0;
            this.adminlvl = 0;
            this.bank = 4500;
            this.money = 500;
            this.cuntdownpaye = 60;
            this.pendingpaye = 0;
            this.sante = 100;
            this.armure = 0;
            this.jobid = 0;
            this.factionid = 0;
            this.rangfaction = 0;
            this.entrepriseid = 0;
            this.rangentreprise = 0;
            this.IsOnPlanqueArmes = false;
            this.IsOnPlanqueDrogues = false;
            this.TimerKitArmes = 0;
            this.IsOnInt = false;
            this.dimension = 0;
            this.IsMenotter = false;
            this.IsDead = false;
            this.PermisDeConduire = -1;
            this.Spawned = false;
            this.sexe = 0;
            this.QuestionAutoEcole = 0;
            this.ColshapeAutoEcole = null;
        }

        public static void Delete(PlayerInfo player)
        {
            PlayerInfo.PlayerList.Remove(player);
            player = null;
        }
        public static PlayerInfo GetPlayerInfoObject(Client player)
        {
            foreach (PlayerInfo client in PlayerInfo.PlayerList)
            {
                if (client.Handle == player) return client;
            }
            return null;
        }

        public static PlayerInfo GetPlayerInfoByIdBDD(int PlayeridBDD) //Return PlayerInfo à partir de l'id bdd
        {
            foreach (PlayerInfo client in PlayerInfo.PlayerList)
            {
                if (client.dbid == PlayeridBDD) return client;
            }
            return null;
        }

        public static PlayerInfo GetPlayerInfotByIdOrName(string idOrName) //Return PlayerInfo a partir de l'id/Prenom_Nom
        {
            int id;
            if (int.TryParse(idOrName, out id))
            {
                if (PlayerList[id] == null)
                {
                    return null;
                }
                return PlayerList[id];
            }

            PlayerInfo returnClient = null;
            int playersCount = 0;
            foreach (var player in PlayerList)
            {
                if (player == null) continue;
                
                if (player.PlayerName.ToLower().Contains(idOrName.ToLower()))
                {
                    if ((player.PlayerName.Equals(idOrName, StringComparison.OrdinalIgnoreCase)))
                    {
                        return player;
                    }
                    else
                    {
                        playersCount++;
                        returnClient = player;
                    }
                }
            }
            if (playersCount != 1)
                return null;

            return returnClient;
        }

    }
}
