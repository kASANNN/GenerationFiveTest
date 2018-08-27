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
    class OnResourceStop : Script
    {
        public OnResourceStop()
        {
            API.onResourceStop += OnResourceStopHandler;
        }

        private void OnResourceStopHandler()
        {
            foreach (var veh in API.getAllVehicles())
            {
                VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoByNetHandle(veh);
                if(objveh == null)
                {
                    API.deleteEntity(veh);
                }
                else
                {
                    SaveTuningVehicule(objveh.dbid);
                    if(objveh.factionid != 0 || objveh.jobid != 0)
                    {
                        SaveEssenceVehicule(objveh.dbid, objveh.essence);
                        VehiculeInfo.Delete(veh);
                    }
                    else
                    {
                        SavePosVehicule(objveh.dbid);
                        SaveEssenceVehicule(objveh.dbid, objveh.essence);
                        VehiculeInfo.Delete(veh);
                    }     
                }
            }
            API.consoleOutput("[SAVE] Vehicules OK.");
            foreach (var label in API.getAllLabels())
            {
                API.deleteEntity(label);
            }
            API.consoleOutput("[DELETE] Labels OK.");
            foreach (var blip in API.getAllBlips())
            {
                API.deleteEntity(blip);
            }
            API.consoleOutput("[DELETE] Blips OK.");
            foreach (StationsEssencesInfo station in StationsEssencesInfo.StationsList)
            {
                API.exported.database.executeQuery("UPDATE StationsEssences SET Stockage = '"+ station.Stockage +"', Argents = '"+ station.Argents +"' WHERE ID = '"+ station.IDBDD +"'");
            }
            API.consoleOutput("[SAVE] Station Essence OK.");
            SaveATM();
            SaveClefs();
        }

        public void SaveEssenceVehicule(int IDBDDVeh, int EssenceLevel)
        {
            API.exported.database.executeQuery("UPDATE Vehicules SET Essence='"+ EssenceLevel +"' WHERE ID = '"+ IDBDDVeh +"'");
        }

        public void SaveTuningVehicule(int IDBDDVeh)
        {
            string[] mod = { "FrontBumper", "RearBumper", "SideSkirt", "Exhaust", "Frame", "Grille", "Hood", "Fender", "RightFender", "Roof", "Engine", "Brakes", "Transmission", "Horns", "Suspension", "Armor", "Turbo", "Xenon", "FrontWheels", "BackWheels", "PlateHolders", "TrimDesign", "Ornaments", "DialDesign", "SteeringWheel", "ShiftLever", "Plaques", "Hydraulics", "Livery", "Plate", "Color1", "Color2", "WindowTint" };
            int[] mod2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 22, 23, 24, 25, 27, 28, 30, 33, 34, 35, 38, 48, 62, 66, 67, 69 };
            if(VerifTableModVehExiste(IDBDDVeh))
            {
                string requete = "UPDATE ModVehicules SET ";
                if(API.getEntityData(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, "Spoilers") != null)
                {
                    requete += String.Format("Spoilers={0}", API.getVehicleMod(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, 0));
                }
                else
                {
                    requete += "Spoilers=-1";
                }
                for (int i = 0; i < mod.Length; i++)
                {
                    if (API.getEntityData(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, mod[i]) == null)
                    {
                        requete += String.Format(",{0}=-1", mod[i]);
                    }
                    else
                    {
                        requete += String.Format(",{0}={1}", mod[i], API.getVehicleMod(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, mod2[i]));
                    }
                }
                requete += " WHERE IDVeh = '" + IDBDDVeh + "'";
                API.exported.database.executeQuery(requete);
            }
            else
            {
                string requete;
                if (API.getEntityData(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, "Spoilers") != null)
                {
                    requete = "INSERT INTO ModVehicules VALUES ('','" + IDBDDVeh + "','" + API.getEntityData(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, "Spoilers") + "'";
                }
                else
                {
                    requete = "INSERT INTO ModVehicules VALUES ('','" + IDBDDVeh + "','-1'";
                }
                for(int i = 0; i < mod2.Length; i++)
                {
                    if(API.getEntityData(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, mod[i]) == null)
                    {
                        requete += ", '-1'";
                    } 
                    else
                    {
                        requete += String.Format(", {0}", API.getVehicleMod(VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh).handle, mod2[i]));
                    }
                }
                requete += ");";
                API.exported.database.executeQuery(requete);
            }
            
        }

        public bool VerifTableModVehExiste(int IDBDDVeh)
        {
            DataTable result = API.exported.database.executeQueryWithResult("SELECT * FROM ModVehicules WHERE IDVeh = '" + IDBDDVeh + "'");
            if (result.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public void SavePosVehicule(int IDBDDVeh)
        {
            VehiculeInfo objveh = VehiculeInfo.GetVehicleInfoByIdBDD(IDBDDVeh);
            Vector3 vpos = API.getEntityPosition(objveh.handle);
            Vector3 vrot = API.getEntityRotation(objveh.handle);
            API.exported.database.executeQuery("UPDATE Vehicules SET Locked='"+ Convert.ToInt32(API.getVehicleLocked(objveh.handle)) +"',PosX='" + vpos.X + "',PosY='"+ vpos.Y +"',PosZ='"+ vpos.Z +"',RotX='"+ vrot.X +"',RotY='"+ vrot.Y +"',RotZ='"+ vrot.Z +"',Dimension='"+ API.getEntityDimension(objveh.handle) +"',DansGarage='"+ Convert.ToInt32(objveh.Dansgarage) +"'  WHERE ID = '" + IDBDDVeh + "'");
        }

        public void SaveATM()
        {
            foreach(ATMInfo atm in ATMInfo.ATMList)
            {
                API.exported.database.executeQuery("UPDATE ATM SET Argent = '" + atm.argent + "' WHERE ID = '" + atm.IDBDD + "'");
            }
            API.consoleOutput("[SAVE] ATM OK.");
        }

        public void SaveClefs()
        {
            API.exported.database.executeQuery("TRUNCATE TABLE Clefs");
            foreach (ClefInfo clef in ClefInfo.ClefList)
            {
                API.exported.database.executeQuery("INSERT INTO Clefs VALUE ('', '"+ clef.PlayerIDBDD +"', '" + clef.Type + "', '" + clef.ObjetID + "')");
            }
            API.consoleOutput("[SAVE] Clefs OK.");
        }
    }
}
