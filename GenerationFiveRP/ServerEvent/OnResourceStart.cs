using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

namespace GenerationFiveRP
{
    class OnResourceStart : Script
    {
        public OnResourceStart()
        {
            API.onResourceStart += OnResourceStartHandler;
        }

        private void OnResourceStartHandler()
        {
            LoadClefs();
            LoadATM();
            LoadBanque();
            LoadPorte();
            TextLabelInfo.LoadTextLabel();
        }

        public void LoadClefs()
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Clefs");
            foreach (DataRow row in result.Rows)
            {
                ClefInfo clef = new ClefInfo(Convert.ToInt32(row["PlayerID"]), Convert.ToString(row["Type"]), Convert.ToInt32(row["ObjetID"]));
            }
        }

        public void LoadATM()
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM ATM");
            foreach (DataRow row in result.Rows)
            {
                ATMInfo ATM = new ATMInfo(Convert.ToInt32(row["idATM"]), new Vector3(Convert.ToInt32(row["PosX"]), Convert.ToInt32(row["PosY"]), Convert.ToInt32(row["PosZ"])), 0, Convert.ToInt32(row["Argent"]));
            }
        }

        public void LoadBanque()
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Banque");
            foreach (DataRow row in result.Rows)
            {
                BanqueInfo Banque = new BanqueInfo(Convert.ToInt32(row["id"]), new Vector3(Convert.ToInt32(row["PosX"]), Convert.ToInt32(row["PosY"]), Convert.ToInt32(row["PosZ"])));
            }
        }

        public void LoadPorte()
        {
            //Porte des cellule
            API.call("DoorManager", "registerDoor", 631614199, new Vector3(464.5701, -992.6641, 25.06443));
            API.call("DoorManager", "setDoorState", Constante.porte1, true, 0);
            API.call("DoorManager", "registerDoor", 631614199, new Vector3(461.8065, -994.4086, 25.06443));
            API.call("DoorManager", "setDoorState", Constante.porte2, true, 0);
            API.call("DoorManager", "registerDoor", 631614199, new Vector3(461.8065, -997.6583, 25.06443));
            API.call("DoorManager", "setDoorState", Constante.porte3, true, 0);
            API.call("DoorManager", "registerDoor", 631614199, new Vector3(461.8065, -1001.302, 25.06443));
            API.call("DoorManager", "setDoorState", Constante.porte4, true, 0);
            API.call("DoorManager", "registerDoor", -1033001619, new Vector3(463.4782, -1003.538, 25.00599));
            API.call("DoorManager", "setDoorState", Constante.porte5, true, 0);
            //Grande porte d'entrer de la prison
            API.call("DoorManager", "registerDoor", 741314661, new Vector3(1844.998, 2604.813, 44.63978));
            API.call("DoorManager", "registerDoor", 741314661, new Vector3(1818.543, 2604.813, 44.611));
            //Armurerie
            API.call("DoorManager", "registerDoor", 97297972, new Vector3(845.3694, -1024.539, 28.34478));
            API.call("DoorManager", "registerDoor", -8873588, new Vector3(18.572, -1115.495, 29.94694));
            API.call("DoorManager", "registerDoor", 97297972, new Vector3(16.12787, -1114.606, 29.94694));
            API.call("DoorManager", "registerDoor", -8873588, new Vector3(243.8379, -46.52324, 70.09098));
            API.call("DoorManager", "registerDoor", 97297972, new Vector3(244.7275, -44.07911, 70.09098));
            API.call("DoorManager", "registerDoor", -8873588, new Vector3(-662.6415, -944.3256, 21.97915));
            API.call("DoorManager", "registerDoor", 97297972, new Vector3(-665.2424, -944.3256, 21.97915));
        }
    }
}
