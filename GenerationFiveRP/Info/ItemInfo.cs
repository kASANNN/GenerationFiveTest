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
    public class ItemInfo
    {
        public int Type;
        public int Id;
        public int IdCoffre;
        public int Nombre;
        public int Data1;
        public int Data2;
        public int Data3;
        public bool Updatable;

        public ItemInfo(int coffreid, int type, int nombre, int data1 = 0, int data2 = 0, int data3 = 0, bool updatable = true)
        {
            CoffreInfo.ItemInfoList.Add(this);
            this.IdCoffre = coffreid;
            this.Nombre = nombre;
            this.Type = type;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Updatable = updatable;
            string requete = "INSERT INTO Item SET id='', coffreid={coffreid}, type={type}, nombre={nombre}, data1={data1}, data2={data2}, data3={data3}, updatable={(updatable ? 1 : 0)};";
            API.shared.exported.database.executeQuery(requete);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT LAST_INSERT_ID();");
            foreach (DataRow row in result.Rows)
            {
                this.Id = Convert.ToInt32(row[0]);
                break;
            }
        }

        public static void PrintCoffreContent(Client sender, int coffreid)
        {
            int moneys = 0;
            string output;
            foreach (ItemInfo item in CoffreInfo.ItemInfoList)
            {
                if (item.IdCoffre == coffreid)
                {
                    output = "type:{item.Type}, nombre:{item.Nombre}";
                    //getitemdescription
                    API.shared.sendChatMessageToPlayer(sender, output);
                    moneys++;
                }
            }
            if(moneys == 0) API.shared.sendChatMessageToPlayer(sender, "il n'y a rien dans ce coffre");
        }

        public static void AddItem(int itemtype, CoffreInfo coffreid, int nombre, int data1 = 0, int data2 = 0, int data3 = 0, bool updatable = true)
        {
            ItemInfo moneys = ItemExiste(itemtype, coffreid);
            if (moneys != null)
            {
                if (moneys.Updatable)
                {
                    moneys.Nombre += nombre;
                    moneys.Data1 += data1;
                    moneys.Data2 += data2;
                    moneys.Data3 += data3;
                    string requete = "UPDATE Item SETnombre={moneys.Nombre}, data1={moneys.Data1}, data2={moneys.Data2}, data3={moneys.Data3} WHERE id={moneys.Id};";
                    API.shared.exported.database.executeQuery(requete);
                    return;
                }
                else
                {
                    new ItemInfo(coffreid.Id, itemtype, nombre, data1, data2, data3, updatable);
                    return;
                }
            }
            else
            {
                new ItemInfo(coffreid.Id, itemtype, nombre, data1, data2, data3, updatable);
            }
            return;
        }

        public static void SetItem(int itemtype, CoffreInfo coffreid, int nombre, int data1 = 0, int data2 = 0, int data3 = 0, bool updatable = true)
        {
            ItemInfo moneys = ItemExiste(itemtype, coffreid);
            if (moneys != null)
            {
                if (moneys.Updatable)
                {
                    moneys.Nombre = nombre;
                    moneys.Data1 = data1;
                    moneys.Data2 = data2;
                    moneys.Data3 = data3;
                    string requete = "UPDATE Item SETnombre={moneys.Nombre}, data1={moneys.Data1}, data2={moneys.Data2}, data3={moneys.Data3} WHERE id={moneys.Id};";
                    API.shared.exported.database.executeQuery(requete);
                    return;
                }
                else
                {
                    new ItemInfo(coffreid.Id, itemtype, nombre, data1, data2, data3, updatable);
                    return;
                }
            }
            return;
        }
        
        public static void RemoveItem(int itemtype, CoffreInfo coffreid, int nombre, int data1 = 0, int data2 = 0, int data3 = 0, bool updatable = true)
        {
            ItemInfo moneys = ItemExiste(itemtype, coffreid);
            if (moneys != null)
            {
                if (moneys.Updatable)
                {
                    moneys.Nombre -= nombre;
                    moneys.Data1 -= data1;
                    moneys.Data2 -= data2;
                    moneys.Data3 -= data3;
                    string requete = "UPDATE Item SETnombre={moneys.Nombre}, data1={moneys.Data1}, data2={moneys.Data2}, data3={moneys.Data3} WHERE id={moneys.Id};";
                    API.shared.exported.database.executeQuery(requete);
                    return;
                }
                else
                {
                    CoffreInfo.ItemInfoList.Remove(moneys);
                    API.shared.exported.database.executeQuery("DELETE FROM Item WHERE id=" + moneys.Id + "");
                    return;
                }
            }
            return;
        }

        public static void DeleteItemInCoffre(CoffreInfo coffreid)
        {
            foreach (ItemInfo item in CoffreInfo.ItemInfoList)
            {
                if (item.IdCoffre != coffreid.Id) continue;
                CoffreInfo.ItemInfoList.Remove(item);
            }
            API.shared.exported.database.executeQuery("DELETE FROM Item WHERE coffreid=" + coffreid.Id + "");
            return;
        }

        public static ItemInfo ItemExiste(int itemtype, CoffreInfo coffreid)
        {
            foreach (ItemInfo item in CoffreInfo.ItemInfoList)
            {
                if (item.IdCoffre != coffreid.Id) continue;
                if (item.Type != itemtype) continue;
                return item;
            }
            return null;
        }

        //load item au demarage du serveur
    }
}
