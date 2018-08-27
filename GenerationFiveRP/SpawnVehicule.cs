using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Data;

namespace GenerationFiveRP
{

    public class SpawnVehicule : Script
    {
        public SpawnVehicule()
        {
            spawnvehicule();
        }

        [Command("testtuning")]
        public void Testalarm(Client player)
        {
            SaveMod(player.vehicle.handle);
        }

        public void spawnvehicule()
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM Vehicules");
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string vposx = Convert.ToString(row["PosX"]);
                    string vposy = Convert.ToString(row["PosY"]);
                    string vposz = Convert.ToString(row["PosZ"]);
                    string vrotx = Convert.ToString(row["RotX"]);
                    string vroty = Convert.ToString(row["RotY"]);
                    string vrotz = Convert.ToString(row["RotZ"]);
                    int model = Convert.ToInt32(row["model"]);
                    int color1 = Convert.ToInt32(row["color1"]);
                    int color2 = Convert.ToInt32(row["color2"]);                    
                    int vid = Convert.ToInt32(row["ID"]);
                    string plaque = Convert.ToString(row["Plaque"]);

                    Vector3 rotv = new Vector3(float.Parse(vrotx), float.Parse(vroty), float.Parse(vrotz));
                    Vector3 posv = new Vector3(float.Parse(vposx), float.Parse(vposy), float.Parse(vposz));
                    if(Convert.ToInt32(row["EnVente"]) == 1)
                    {
                        VehiculeInfo vehicleobj = new VehiculeInfo((VehicleHash)model, posv, rotv, color1, color2, plaque, 0);
                        vehicleobj.Dansgarage = false;
                        vehicleobj.EnVente = true;
                        vehicleobj.PrixVente = Convert.ToInt32(row["PrixVente"]);
                        vehicleobj.handle.invincible = true;
                        vehicleobj.handle.freezePosition = true;
                        vehicleobj.dbid = Convert.ToInt32(row["ID"]);
                        API.setVehicleNumberPlate(vehicleobj.handle, "A VENDRE");
                        VehiculeInfo.SetVehiculeEssence(vehicleobj, Convert.ToInt32(row["Essence"]));
                        LoadMods(vehicleobj.dbid);
                    }
                    if (Convert.ToBoolean(row["Spawned"]) == true && Convert.ToInt32(row["EnVente"]) == 0)
                    {
                        VehiculeInfo vehicleobj = new VehiculeInfo((VehicleHash)model, posv, rotv, color1, color2, plaque, 0);
                        VehiculeInfo.SetVehiculeEssence(vehicleobj, Convert.ToInt32(row["Essence"]));
                        API.setVehicleEngineStatus(vehicleobj.handle, false);
                        API.setEntityDimension(vehicleobj.handle, Convert.ToInt32(row["Dimension"]));
                        vehicleobj.Dansgarage = Convert.ToBoolean(row["DansGarage"]);
                        vehicleobj.spawned = Convert.ToBoolean(row["Spawned"]);
                        vehicleobj.jobid = Convert.ToInt32(row["jobid"]);
                        vehicleobj.factionid = Convert.ToInt32(row["factionid"]);
                        vehicleobj.IDBDDProprio = Convert.ToInt32(row["Proprio"]);
                        vehicleobj.dbid = Convert.ToInt32(row["ID"]);
                        API.setVehicleLocked(vehicleobj.handle, Convert.ToBoolean(row["Locked"]));
                        vehicleobj.locked = Convert.ToBoolean(row["Locked"]);
                        LoadMods(vehicleobj.dbid);
                    }
                }
            }
        }

        public static string CreateNewVehiclePlate()
        {
            bool variable = false;
            while (!variable)
            {
                Random aleatoire = new Random();
                int n1 = aleatoire.Next(0, 9);
                int n2 = aleatoire.Next(0, 9);
                int n3 = aleatoire.Next(0, 9);
                int n4 = aleatoire.Next(0, 9);
                int n5 = aleatoire.Next(0, 9);
                int n6 = aleatoire.Next(0, 9);
                int n7 = aleatoire.Next(0, 9);
                int n8 = aleatoire.Next(0, 9);
                string plaque = String.Format("{0}{1}{2}{3}.{4}{5}{6}{7}", n1, n2, n3, n4, n5, n6, n7, n8);
                if (VerifDispoPlaque(plaque))
                {
                    variable = true;
                    return plaque;
                }
            }
            return "null";
        }

        public static bool VerifDispoPlaque(string plaque)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Vehicules WHERE Plaque = '" + plaque + "'");
            if (result.Rows.Count != 0)
            {
                return false;
            }
            return true;
        }

        public static void SetVehSpawnPoint(NetHandle vehicule, Vector3 Pos, Vector3 Rot)
        {
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoByNetHandle(vehicule);
            API.shared.exported.database.executeQuery("UPDATE Vehicules SET PosX='"+ Pos.X +"', PosY='"+ Pos.Y +"', PosZ='"+ Pos.Z +"', RotX='"+ Rot.X +"', RotY='"+ Rot.Y +"', RotZ= '"+ Rot.Z +"' WHERE ID = '" + objveh.dbid + "'");
        }

        public static bool VerifDispoPlaceParking(Vector3 pos)
        {
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM Vehicules");
            foreach (DataRow row in result.Rows)
            {
                Vector3 Pos = new Vector3(Convert.ToInt32(row["PosX"]), Convert.ToInt32(row["PosY"]), Convert.ToInt32(row["PosZ"]));
                if (pos.DistanceTo(Pos) < 5)
                {
                    return false;
                }
            }
            return true;
        }

        public void SaveMod(NetHandle vehicle)
        {
            string[] mod = { "Spoilers", "FrontBumper", "RearBumper", "SideSkirt", "Exhaust", "Frame", "Grille", "Hood", "Fender", "RightFender", "Roof", "Engine", "Brakes", "Transmission", "Horns", "Suspension", "Armor", "Turbo", "Xenon", "FrontWheels", "BackWheels", "PlateHolders", "TrimDesign", "Ornaments", "DialDesign", "SteeringWheel", "ShiftLever", "Plaques", "Hydraulics", "Livery", "Plate", "Color1", "Color2", "WindowTint" };
            int[] mod2 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 22, 23, 24, 25, 27, 28, 30, 33, 34, 35, 38, 48, 62, 66, 67, 69 };
            for (int i = 0; i < mod.Length; i++)
            {
                if (API.getVehicleMod(vehicle, mod2[i]) != -1)
                {
                    API.setEntityData(vehicle, mod[i], API.getVehicleMod(vehicle, mod2[i]));
                }
            }
        }

        public void LoadMods(int IDBDDVeh)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM ModVehicules WHERE IDVeh = '" + IDBDDVeh + "'");
            foreach (DataRow row in result.Rows)
            {
                string[] mod = { "Spoilers", "FrontBumper", "RearBumper", "SideSkirt", "Exhaust", "Frame", "Grille", "Hood", "Fender", "RightFender", "Roof", "Engine", "Brakes", "Transmission", "Horns", "Suspension", "Armor", "Turbo", "Xenon", "FrontWheels", "BackWheels", "PlateHolders", "TrimDesign", "Ornaments", "DialDesign", "SteeringWheel", "ShiftLever", "Plaques", "Hydraulics", "Livery", "Plate", "Color1", "Color2", "WindowTint" };
                int[] mod2 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 22, 23, 24, 25, 27, 28, 30, 33, 34, 35, 38, 48, 62, 66, 67, 69 };
                for (int i = 0; i < mod.Length; i++)
                {
                    if(Convert.ToInt32(row[mod[i]]) != -1)
                    {
                        API.setEntityData(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, mod[i], Convert.ToInt32(row[mod[i]]));
                        API.setVehicleMod(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, mod2[i], Convert.ToInt32(row[mod[i]]));
                    }
                }
            }
        }
    }
}
