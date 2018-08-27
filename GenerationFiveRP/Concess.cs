using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Data;
using System;

namespace GenerationFiveRP
{
    public class Concess : Script
    {
        int[] modelvehconcess = { 1039032026, -1883002148, -14495224, 1078682497, 1762279763, -1311240698, 523724515 };
        int[] prixvehconcess = { 6500, 4000, 4000, 8799, 3000, 2500, 3599 };

        public Concess()
        {
            API.onResourceStart += OnResourceStartHandler;
            API.delay(300000, false, () =>
            {
                //RemplacementConcessVendu();
            });
        }

        public void OnResourceStartHandler()
        {
            BlipsConcessPauvre();
        }

        public class ConcessPauvre
        {
            public Vector3 Position { get; set; }

            public ConcessPauvre(Vector3 position)
            {
                Position = position;
                var b = API.shared.createBlip(Position);
                API.shared.setBlipSprite(b, 225);
                API.shared.setBlipTransparency(b, 125);
                API.shared.setBlipShortRange(b, true);
                API.shared.setBlipName(b, "Concession d'occasion");
            }
        }

        public void BlipsConcessPauvre()
        {
            new ConcessPauvre(new Vector3(-41.04974, -1675.08, 29.44744));
        }

        [Command("saveconcess")]
        public void VehConcess(Client player, int PrixVente)
        {
            if (!API.isPlayerInAnyVehicle(player))
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'es pas dans un vehicule");
                return;
            }
            else
            {
                var idvehicule = API.getPlayerVehicle(player);
                Vector3 vpos = API.getEntityPosition(idvehicule);
                Vector3 vrot = API.getEntityRotation(idvehicule);
                int model = API.getEntityModel(idvehicule);
                int color1 = API.getVehiclePrimaryColor(idvehicule);
                int color2 = API.getVehicleSecondaryColor(idvehicule);
                API.exported.database.executeQuery("INSERT INTO Vehicules VALUES ('','0','0','" + model + "','" + color1 + "','" + color2 + "','" + vpos.X + "','" + vpos.Y + "','" + vpos.Z + "','" + vrot.X + "','" + vrot.Y + "','" + vrot.Z + "', '1', '" + PrixVente + "')");
                DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Vehicules WHERE PosX = '" + vpos.X + "' AND PosY = '" + vpos.Y + "' AND PosZ = '" + vpos.Z + "'");
                foreach (DataRow row in result.Rows)
                {
                    API.exported.database.executeQuery("INSERT INTO Concess VALUES ('', '" + Convert.ToInt32(row["ID"]) + "', '0', '" + PrixVente + "'");
                }
            }
        }

        [Command("creerempl")]
        public void creerempl(Client player)
        {
            if (!API.isPlayerInAnyVehicle(player))
            {
                API.sendChatMessageToPlayer(player, "~r~Tu dois être dans un vehicule pour sauvegarder un emplacement");
                return;
            }
            else
            {
                var idvehicule = API.getPlayerVehicle(player);
                Vector3 vpos = API.getEntityPosition(idvehicule);
                Vector3 vrot = API.getEntityRotation(idvehicule);
                API.exported.database.executeQuery("INSERT INTO Concess VALUES ('', '0', '1', '0', '"+ vpos.X + "', '" + vpos.Y + "', '" + vpos.Z + "', '" + vrot.X + "', '" + vrot.Y + "', '" + vrot.Z + "')");
                API.sendChatMessageToPlayer(player, "~g~N'oublie pas de /reloadconcess pour faire spawn les vehicules sur les emplacements vide");
            }
        }

        [Command("reloadconcess")]
        public void reloadconcess(Client player)
        {
            RemplacementConcessVendu();
        }

        public void RemplacementConcessVendu()
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Concess WHERE Vendu = '1'");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Random aleatoire = new Random();
                    int model = aleatoire.Next(0, modelvehconcess.Length);
                    int color1 = aleatoire.Next(0, 159);
                    int color2 = aleatoire.Next(0, 159);
                    string vposx = String.Format("" + row["PosX"]);
                    string vposy = String.Format("" + row["PosY"]);
                    string vposz = String.Format("" + row["PosZ"]);
                    string vrotx = String.Format("" + row["RotX"]);
                    string vroty = String.Format("" + row["RotY"]);
                    string vrotz = String.Format("" + row["RotZ"]);
                    Vector3 rotv = new Vector3(float.Parse(vrotx), float.Parse(vroty), float.Parse(vrotz));
                    Vector3 posv = new Vector3(float.Parse(vposx), float.Parse(vposy), float.Parse(vposz));
                    VehiculeInfo vehicleobj = new VehiculeInfo((VehicleHash)modelvehconcess[model], posv, rotv, color1, color2, "A VENDRE", 0);
                    API.exported.database.executeQuery("INSERT INTO Vehicules VALUES ('','0','0','0','"+ modelvehconcess[model] +"','" + color1 + "','" + color2 + "','" + posv.X + "','" + posv.Y + "','" + posv.Z + "','" + rotv.X + "','" + rotv.Y + "','" + rotv.Z + "', '1', '"+ prixvehconcess[model] +"', '100', 'A VENDRE', '1', '0', '0', '0')");
                    DataTable result2 = API.shared.exported.database.executeQueryWithResult("SELECT LAST_INSERT_ID();");
                    foreach (DataRow row2 in result2.Rows)
                    {
                        vehicleobj.dbid = Convert.ToInt32(row2[0]);
                        break;
                    }
                    vehicleobj.dbid = GetVehiculeIDBBD(posv);
                    API.exported.database.executeQuery("UPDATE Concess SET Vendu ='0' WHERE ID = '" + Convert.ToInt32(row["ID"]) + "'");
                    API.exported.database.executeQuery("UPDATE Concess SET IDVeh ='"+ vehicleobj.dbid + "' WHERE ID = '" + Convert.ToInt32(row["ID"]) + "'");
                    vehicleobj.EnVente = true;
                    vehicleobj.PrixVente = prixvehconcess[model];
                    vehicleobj.handle.invincible = true;
                    vehicleobj.handle.freezePosition = true;
                    API.setVehicleNumberPlate(vehicleobj.handle, "A VENDRE");
                }
            }
        }

        public static bool PlayerHaveVehicleKeys(Client player, int IDBDDVehicule)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            VehiculeInfo objvehicule = VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVehicule);
            if (ClefInfo.GetPlayerKeysByObjetID(objplayer.dbid, IDBDDVehicule).Count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int GetVehKeyNumberPlayer(Client player, int IDBDDVehicule)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM ClefsVehicules WHERE IDVehicule = '"+ IDBDDVehicule +"' AND IDJoueur = '" + objplayer.dbid + "'");
            if (result.Rows.Count != 0)
            {
                foreach(DataRow row in result.Rows)
                {
                    return Convert.ToInt32(row["Nombre"]);
                }
            }
            return 0;
        }

        public static bool PlayerIsProprio(Client player, int IDBDDVehicule)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Vehicules WHERE ID = '" + IDBDDVehicule + "' AND Proprio = '" + objplayer.dbid + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static int GetVehiculeIDBBD(Vector3 pos)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Vehicules WHERE PosX = "+ pos.X +" AND PosY = "+ pos.Y +" AND PosZ = "+ pos.Z +"");
            foreach (DataRow row in result.Rows)
            {
                return Convert.ToInt32(row["ID"]);
            }
            return 0;
        }

        public static void AddKeyVehToPlayer(Client player, int IDBDDVehicule)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            int NombreClef = GetVehKeyNumberPlayer(player, IDBDDVehicule);
            if(NombreClef == 0)
            {
                API.shared.exported.database.executeQuery("INSERT INTO ClefsVehicules VALUES ('', '" + IDBDDVehicule + "', '" + objplayer.dbid + "', '1')");
                return;
            }
            else
            {
                API.shared.exported.database.executeQuery("UPDATE ClefsVehicules SET Nombre='"+ NombreClef + 1 + "' WHERE IDJoueur = '" + objplayer.dbid + "' AND IDVehicule ='"+ IDBDDVehicule +"'");
            }
        }

        public static void RemoveKeyVehToPlayer(Client player, int IDBDDVehicule)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            int NombreClef = GetVehKeyNumberPlayer(player, IDBDDVehicule);
            if (NombreClef == 1)
            {
                API.shared.exported.database.executeQuery("DELETE * FROM ClefsVehicules WHERE IDJoueur ='" + objplayer.dbid + "' AND IDVehicule='" + IDBDDVehicule + "'");
                return;
            }
            else
            {
                int newnombre = NombreClef - 1;
                API.shared.exported.database.executeQuery("UPDATE ClefsVehicules SET Nombre='" + newnombre + "' WHERE IDJoueur = '" + objplayer.dbid + "' AND IDVehicule ='" + IDBDDVehicule + "'");
            }
        }
    }
}
