using System;
using System.Collections.Generic;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

namespace GenerationFiveRP
{
    public class police : Script
    {
        public police()
        {
            API.onResourceStart += onStart;
            API.onEntityEnterColShape += ColShapeTrigger;
            API.onEntityExitColShape += ExitColShapeTrigger;
            Createcolshapedoorlspd();
        }

        public void Createcolshapedoorlspd()
        {
            ColShape doorvestiere = API.createSphereColShape(new Vector3(450.2961, -986.3849, 30.68961), 1);
            doorvestiere.setData("doorvestiere", true);
        }

        public void ColShapeTrigger(ColShape colshape, NetHandle entity)
        {
            if(colshape.getData("doorvestiere") == true && PlayerInfo.GetPlayerInfoObject(API.getPlayerFromHandle(entity)).factionid == Constante.Faction_Police)
            {
                API.sendNativeToAllPlayers(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, 1557126584, 450.1041f, -985.7384f, 30.8393f, false, 0);
                API.sendNotificationToPlayer(API.getPlayerFromHandle(entity), "~g~Porte Deverrouillee", true);
                return;
            }
        }

        public void ExitColShapeTrigger(ColShape colshape, NetHandle entity)
        {
            if (colshape.getData("doorvestiere") == true && PlayerInfo.GetPlayerInfoObject(API.getPlayerFromHandle(entity)).factionid == Constante.Faction_Police)
            {
                API.sendNativeToAllPlayers(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, 1557126584, 450.1041f, -985.7384f, 30.8393f, true, 0);
                API.sendNotificationToPlayer(API.getPlayerFromHandle(entity), "~r~Porte Verrouillee", true);
                return;
            }
        }

        public static bool isArmurerieLSPD(Client player)
        {
            if (player.position.DistanceTo(new Vector3(452.4519, -980.0848, 30.6896)) < 2)
            {
                return true;
            }
            return false;
        }

        public static bool isDistrib(Client player)
        {
            if (player.position.DistanceTo(new Vector3(436.2242, -986.8041, 30.6896)) < 1)
            {
                return true;
            }
            return false;
        }

        public static bool isService(Client player)
        {
            if (player.position.DistanceTo(new Vector3(457.7373, -990.8897, 30.6896)) < 2)
            {
                return true;
            }
            return false;
        }

        public static bool isCellule(Client player)
        {
            if (player.position.DistanceTo(new Vector3(459.7372, -988.9495, 24.91486)) < 1)
            {
                return true;
            }
            return false;
        }

        public void onStart()
        {
            BlipsPolice();
        }

        public class Police
        {
            public Vector3 Position { get; set; }

            public Police(Vector3 position)
            {
                Position = position;
                var b = API.shared.createBlip(Position);
                API.shared.setBlipSprite(b, 60);
                API.shared.setBlipTransparency(b, 125);
                API.shared.setBlipShortRange(b, true);
            }
        }

        public void BlipsPolice()
        {
            new Police(new Vector3(435.7972, -981.864, 30.69862));
            API.createPed((PedHash)(1581098148), new Vector3(454.1971, -980.0285, 30.68959), 87, 0);
            //Cellule//

            //DoorManager doormanager = new DoorManager();
            //DoorManager.registerDoor(631614199, new Vector3(464.5701, -992.6641, 25.06443));
            


            /*DoorManager doormanager = new DoorManager();
            int PremiereGrille = doormanager.registerDoor(631614199, new Vector3(464.5701, -992.6641, 25.06443));
            int PremiereCellule = doormanager.registerDoor(631614199, new Vector3(461.8065, -994.4086, 25.06443));
            int DeuxiemeCellule = doormanager.registerDoor(631614199, new Vector3(461.8065, -997.6583, 25.06443));
            int TroisiemeCellule = doormanager.registerDoor(631614199, new Vector3(461.8065, -1001.302, 25.06443));
            int PorteFond = doormanager.registerDoor(-1033001619, new Vector3(463.4782, -1003.538, 25.00599));
            doormanager.removeDoor(PremiereGrille);
            doormanager.removeDoor(PremiereCellule);
            doormanager.removeDoor(DeuxiemeCellule);
            doormanager.removeDoor(TroisiemeCellule);
            doormanager.removeDoor(PorteFond);*/
            //Cellule//
        }
    }
}