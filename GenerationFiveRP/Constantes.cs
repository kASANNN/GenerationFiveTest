using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Shared.Math;

namespace GenerationFiveRP
{
    class Constante
    {
        public const int const_months = 12;

        #region Message
        public const string message_id_incorrect = "~r~ L'id ou le Prenom Nom renseigné est incorrect.";
        public const string message_idveh_incorrect = "~r~ L'id du vehicule renseigné est incorrect.";
        public static string Version = "0.1a";
        public static string PasEnService = "Tu n'es pas en ~r~service~s~.";
        public static string PasLSPD = "Tu ne fais pas partie du ~r~LSPD~s~.";
        public static string PasEMS = "Tu ne fais pas partie des ~r~médecins~s~.";
        public static string PasGardien = "Tu ne fais pas partie des ~r~gardiens de prison~s~.";
        public static string PasDeFact = "Tu n'as pas de ~r~faction~s~.";
        public static string PasDent = "Tu n'as pas d'~r~entreprise~s~.";
        public static string PasAdmin = "Tu n'as pas le droit d'utiliser cette ~r~commande admin~s~.";
        public static string PasDeRadioFact = "Tu n'as pas la ~r~radio de ton organisation ~s~sur toi.";
        public static string TuEsTropLoin = "Tu es ~r~trop loin ~s~de cette personne.";
        public static string PasLeader = "Tu n'as pas le ~r~rang nécessaire ~s~pour faire ça.";
        public static string PasDeKit = "Tu n'as pas le ~r~nécessaire pour assembler ~s~une arme.";
        public static string PasDarmeEnMain = "Tu n'as ~r~pas d'armes ~s~dans les mains.";
        public static string PasAssezEnBanque = "Tu n'as pas ~r~l'argent nécessaire en banque ~s~pour faire ça";
        #endregion

        #region Couleur
        public static string BleuClair = "~#2196F3~";
        public static string RadioFaction = "~#990000~";
        public static string JauneMegaphone = "~#C9B12C~";
        public static string VioletMe = "~#C2A2DA~";
        public static string RoseEMS = "#cc33ff";
        #endregion;

        #region Int fixe du GM
        public static int PrixMort = 3000;
        public static int PrixReaEMS = 500;
        public static int PrixSoinEMS = 50;
        public static int PayeReparer = 100;
        public static int PayeCouleur = 100;
        public static int Payday = 500;
        public static int Taxes = 50;
        public static int KitPisto = 5000;
        public static int KitPMitr = 6000;
        public static int KitPompe = 7500;
        public static int KitFusil = 10000;
        public static int PrixPlanqueArme = 40000;
        public static int PrixPlanqueDrogue = 15000;
        #endregion

        #region ID des Item Inventaire
        public static int Coffre_Item_Coke = 1;
        public static int Coffre_Item_Cannabis = 2;
        public static int Coffre_Item_Matos = 3;
        public static int Coffre_Item_Pelle = 4;
        #endregion

        #region ID des type de Coffre
        public static int Coffre_Type_Maison = 1;
        public static int Coffre_Type_Vehicule = 2;
        #endregion

        #region ID des factions

        public const int Faction_Police = 1;
        public const int Faction_Medecin = 2;
        public const int Faction_Gardien = 3;

        #endregion

        #region ID des job
        public const int Job_Convoyeur = 1;
        public const int Job_Eboueur = 2;

        #endregion

        #region Position

        public static readonly Vector3 Pos_EntrerPrison = new Vector3(1690.866, 2591.6, 45.9015); //Pos a prendre (pos pour entrer)
        public static readonly Vector3 Pos_SortiePrison = new Vector3(1691.561, 2565.715, 45.56486); //Pos a prendre (pos pour sortir)
        public static readonly Vector3 Pos_DepotConvoyeur = new Vector3(478.0145, -1397.768, 31.04201);
        public static readonly Vector3 Pos_BanqueConvoyeur = new Vector3(-23.92922, -718.1271, 32.59769);
        public static readonly Vector3 Pos_ServiceEboueur = new Vector3(348.657, -2650.925, 6.221565);
        public static readonly Vector3 Pos_CamionEboueur = new Vector3(342.9853, -2640.455, 6.221563);
        public static readonly Vector3 Pos_EntrerAutoEcole = new Vector3(320.2692, -1627.317, 32.53403);
        public static readonly Vector3 Pos_SortieAutoEcole = new Vector3(-141.1566, -620.8864, 168.8204);
        #endregion

        #region Porte
        public static int porte1 = 1;
        public static int porte2 = 2;
        public static int porte3 = 3;
        public static int porte4 = 4;
        public static int porte5 = 5;
        #endregion
    }
}
