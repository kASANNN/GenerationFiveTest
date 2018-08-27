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
    public class AmendeInfo
    {
        public static List<AmendeInfo> AmendeListe = new List<AmendeInfo>();
        public int id;
        public Client player;
        public int montant;
        public string raison;
        public string auteur;
        public int date;

        public AmendeInfo(Client player, int montant, string raison, string auteur, int date)
        {
            AmendeListe.Add(this);
            this.id = AmendeListe.IndexOf(this);
            this.player = player;
            this.montant = montant;
            this.raison = raison;
            this.auteur = auteur;
            this.date = date;
        }

        public static void Delete(AmendeInfo amende)
        {
            AmendeListe.Remove(amende);
            amende = null;
        }

        public static void DeleteAllForPlayer(Client player)
        {
            foreach (AmendeInfo amende in AmendeListe)
            {
                if (amende.player == player) Delete(amende);
            }
        }

        public static AmendeInfo GetAmendeInfoById(int id)
        {
            foreach (AmendeInfo amende in AmendeListe)
            {
                if (amende.id == id) return amende;
            }
            return null;
        }

        public static bool PlayerHasAmende(Client player)
        {
            foreach (AmendeInfo amende in AmendeListe)
            {
                if (amende.player == player) return true;
            }
            return false;
        }

        public static List<AmendeInfo> GetPlayerAmendeInfo(Client player)
        {
            List<AmendeInfo> liste = new List<AmendeInfo>();
            foreach (AmendeInfo amende in AmendeListe)
            {
                if (amende.player == player) liste.Add(amende);
            }
            return liste;
        }

        public static int GetPlayerAmendeMontantTotal(Client player)
        {
            int total = 0;
            foreach (AmendeInfo amende in AmendeListe)
            {
                if (amende.player == player) total += amende.montant;
            }
            return total;
        }
    }
}
