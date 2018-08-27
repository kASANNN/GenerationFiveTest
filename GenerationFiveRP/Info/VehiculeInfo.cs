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
    public class VehiculeInfo
    {
        public static List<VehiculeInfo> VehiculeList = new List<VehiculeInfo>();
        public int ID;
        public int IDBDDProprio;
        public Vehicle handle;
        public VehicleHash model;
        public Vector3 pos;
        public Vector3 rot;
        private TextLabel label;
        public int color1;
        public int color2;
        public int dbid;
        public int factionid;
        public int jobid;
        public int sacs; //nombre de sacs de banque dans le vehicule
        public int essence;
        public bool EnVente;
        public int PrixVente;
        public bool stopessence;
        public bool spawned;
        public String plaque;
        public bool locked;
        public bool Dansgarage;
        public CoffreInfo coffre;

        public VehiculeInfo(VehicleHash model, Vector3 pos, Vector3 rot, int color1, int color2, string plaque, int dimension = 0)
        {
            Vehicle vehicule = API.shared.createVehicle(model, pos, rot, color1, color2, dimension);
            API.shared.sleep(100);
            this.handle = vehicule;
            VehiculeList.Add(this);
            this.ID = VehiculeList.IndexOf(this);
            this.label = API.shared.createTextLabel("", API.shared.getEntityPosition(vehicule), 50f, 0.4f, true);
            API.shared.attachEntityToEntity(this.label, vehicule, null, new Vector3(0, 0, 1f), new Vector3());
            this.pos = pos;
            this.rot = rot;
            this.color1 = color1;
            this.color2 = color2;
            this.model = model;
            this.coffre = null;
            API.shared.setVehicleNumberPlate(this.handle, plaque);

        }

        public static void Delete(NetHandle vehicule)
        {
            VehiculeInfo vehicleobj = GetVehicleInfoByNetHandle(vehicule);
            //API.shared.explodeVehicle(vehicule);
            API.shared.deleteEntity(vehicleobj.label);
            API.shared.deleteEntity(vehicule);
            VehiculeList.Remove(vehicleobj);
            vehicleobj = null;
        }

        public static VehiculeInfo GetVehicleInfoByObject(Vehicle vehiculeid)
        {
            foreach (VehiculeInfo vehicle in VehiculeList)
            {
                if (vehicle.handle == vehiculeid) return vehicle;
            }
            return null;
        }

        public static VehiculeInfo GetVehicleInfoById(int vehiculeid) //Return VehiculeInfo à partir de l'id
        {
            foreach (VehiculeInfo vehicle in VehiculeList)
            {
                if (vehicle.ID == vehiculeid) return vehicle;
            }
            return null;
        }
        /* Get le premier vehicule pres du joueur dans un rayon de range*/
        public static VehiculeInfo GetVehicleArroundPlayer(Client player, float range = 2f)
        {
            foreach (VehiculeInfo vehicle in VehiculeList)
            {
                if (vehicle.handle.position.DistanceTo(player.position) < range) return vehicle;
            }
            return null;
        }
        /* Get le vehicule le plus pres du joueurs dans un rayon de range */
        public static VehiculeInfo GetVehicleCloserPlayer(Client sender, float range = 2.0f)
        {
            VehiculeInfo returned = null;
            float distance = range;
            foreach (VehiculeInfo vehicle in VehiculeInfo.VehiculeList)
            {
                if (sender.position.DistanceTo(vehicle.handle.position) < range && sender.position.DistanceTo(vehicle.handle.position) < distance)
                {
                    returned = vehicle;
                    distance = sender.position.DistanceTo(vehicle.handle.position);
                }
            }
            return returned;
        }

        public static VehiculeInfo GetVehicleInfoByIdBDD(int vehiculeidbdd) //Return VehiculeInfo à partir de l'id BDD
        {
            foreach (VehiculeInfo vehicle in VehiculeList)
            {
                if (vehicle.dbid == vehiculeidbdd) return vehicle;
            }
            return null;
        }

        public static VehiculeInfo GetVehicleInfoByPlaque(String plaque) //Return VehiculeInfo à partir de la plaque
        {
            foreach (VehiculeInfo vehicle in VehiculeList)
            {
                if (vehicle.plaque == plaque) return vehicle;
            }
            return null;
        }

        public static Vehicle GetVehicleById(int vehiculeid) //Return Vehicle à partir de l'id
        {
            foreach (VehiculeInfo vehicle in VehiculeList)
            {
                if (vehicle.ID == vehiculeid) return vehicle.handle;
            }
            return null;
        }

        public static VehiculeInfo GetVehicleInfoByNetHandle(NetHandle vehicle)
        {
            foreach (VehiculeInfo vehicule in VehiculeList)
            {
                if (vehicule.handle == vehicle) return vehicule;
            }
            return null;
        }

        public static void SetVehiculeEssence(VehiculeInfo veh, int value)
        {
            veh.essence = value;
            API.shared.setEntitySyncedData(veh.handle, "essence", value);
            return;
        }

        public static void ActivateDL(bool value)
        {
            if (value)
            {
                foreach (VehiculeInfo vehicule in VehiculeList)
                {
                    API.shared.setTextLabelText(vehicule.label, string.Format("IDveh:{0}", vehicule.ID));
                    vehicule.label.transparency = 0;
                }
            }
            else
            {
                foreach (VehiculeInfo vehicule in VehiculeList)
                {
                    API.shared.setTextLabelText(vehicule.label, "");
                }
            }
        }
    }
}
