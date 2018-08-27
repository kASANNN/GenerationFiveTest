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
    public class TextLabelInfo
    {
        public static List<TextLabelInfo> TextLabelList = new List<TextLabelInfo>();
        public int id;
        public TextLabel handle;
        public string text;
        public Vector3 position;
        public float range;
        public float size;
        public bool entityseethrough; //Voir a travers
        public int dimension;

        public TextLabelInfo(string text, Vector3 position, float range, float size, bool entityseethrough = true, int dimension = 0)
        {
            TextLabelList.Add(this);
            this.id = TextLabelList.IndexOf(this);
            this.text = text;
            this.position = position;
            this.range = range;
            this.size = size;
            this.entityseethrough = entityseethrough;
            this.dimension = dimension;
            this.handle = API.shared.createTextLabel(text, position, range, size, entityseethrough, dimension);
        }

        public static void Delete(TextLabelInfo objtextlabel)
        {
            API.shared.deleteEntity(objtextlabel.handle.handle);
            TextLabelList.Remove(objtextlabel);
            objtextlabel = null;
            return;
        }

        public static void LoadTextLabel()
        {
            new TextLabelInfo("[~g~Prison~s~] Appuyez sur ~b~E~n~/emprisonner", Constante.Pos_EntrerPrison, 20f, 0.5f);
            new TextLabelInfo("[~g~Prison~s~] Appuyez sur ~b~E", Constante.Pos_SortiePrison, 20f, 0.5f);
            new TextLabelInfo("[~g~Auto-Ecole~s~] Appuyez sur ~b~E", Constante.Pos_EntrerAutoEcole, 20f, 1f);
            new TextLabelInfo("[~g~Auto-Ecole~s~] Appuyez sur ~b~E", Constante.Pos_SortieAutoEcole, 20f, 1f);
            new TextLabelInfo("Acces Vestiaires, Touche 'E' pour te changer", Constante.Pos_DepotConvoyeur, 25, 1);
            new TextLabelInfo("Zone de chargement", Constante.Pos_BanqueConvoyeur, 25, 1);
            new TextLabelInfo("Appuie sur la touche '~b~E~s~' pour commencer ton job", Constante.Pos_ServiceEboueur, 25, 1);
            new TextLabelInfo("Appuie sur la touche '~b~E~s~' pour ouvrir le menu du job", Constante.Pos_CamionEboueur, 25, 1);
            return;
        }
    }
}
