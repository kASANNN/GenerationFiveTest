using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Threading;

namespace GenerationFiveRP
{
    public class DoorManager : Script
    {
        public DoorManager()
        {
            API.onEntityEnterColShape += ColShapeTrigger;
            API.onClientEventTrigger += ClientEventTrigger;
        }

        private int _doorCounter;
        private Dictionary<int, ColShape> _doorColShapes = new Dictionary<int, ColShape>();

        private bool _debugStatus = true;

        public const ulong SET_STATE_OF_CLOSEST_DOOR_OF_TYPE = 0xF82D8F1926A02C3D;

        [Command("getdoor")]
        public void Debug_GetDoorCMD(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                if (!_debugStatus) return;

                API.triggerClientEvent(player, "doormanager_debug");
            }
        }

        [Command("getdoorex")]
        public void Debug_GetDoorExCMD(Client player, string modelName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                if (!_debugStatus) return;

                var pos = API.getEntityPosition(player);

                API.triggerClientEvent(player, "doormanager_finddoor_return", API.getHashKey(modelName), pos);
            }
        }

        [Command("createDoor")]
        public void Debug_CreateDoorCMD(Client player, int model)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                if (!_debugStatus) return;

                var pos = API.getEntityPosition(player);

                var id = registerDoor(model, pos);

                API.sendChatMessageToPlayer(player, "Your door id is " + id);
            }
        }

        [Command("createDoorEx")]
        public void Debug_CreateDoorExCMD(Client player, string modelName, float x, float y, float z)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                if (!_debugStatus) return;

                var pos = new Vector3(x, y, z);
                var model = API.getHashKey(modelName);

                var id = registerDoor(model, pos);

                API.sendChatMessageToPlayer(player, "Your door id is " + id);
            }
        }

        [Command("setdoorstate")]
        public void Debug_SetDoorStateCMD(Client player, int doorId, bool locked, float heading)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                if (!_debugStatus) return;

                setDoorState(doorId, locked, heading);
            }
        }

        [Command("findobj")]
        public void Debug_FindDoorCMD(Client player, int model)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                if (!_debugStatus) return;

                var pos = API.getEntityPosition(player);

                API.triggerClientEvent(player, "doormanager_finddoor", model, pos);
            }
        }

        [Command("door")]
        public void IPL(Client sender)
        {
            //Hopefully all door objects :D
            int[] Doors =
            {
            -1884098102,
            -218019333,
            -850860783,
            357195902,
            34120519,
            -368655288,
            621101123,
            -31919505,
            1263238661,
            -1934393132,
            -1469164005,
            394409025,
            -1436200562,
            1853479348,
            1890297615,
            -1920147247,
            -894594569,
            -812777085,
            -63539571,
            110411286,
            2096238007,
            -667009138,
            1640157877,
            116180164,
            -415922858,
            2116359305,
            46734799,
            393167779,
            -1562944903,
            782871627,
            1356853431,
            -1483545996,
            -2009193533,
            -2051450263,
            1424372521,
            -1232996765,
            -1874351633,
            -2881618,
            224975209,
            330294775,
            -645206502,
            -284254006,
            -1290268385,
            -498077814,
            1450215542,
            1293907652,
            -134415992,
            -165117488,
            -459350339,
            1523529669,
            1596276849,
            -1454760130,
            1245831483,
            -403433025,
            1308911070,
            1071105235,
            -1635579193,
            277255495,
            -699424554,
            674546851,
            -204842037,
            -655196089,
            1713150633,
            -44475594,
            1183182250,
            1764111426,
            -1082334994,
            1056781042,
            -264464292,
            1291867081,
            913904359,
            -26664553,
            914592203,
            -582278602,
            1343686600,
            1742849246,
            2052512905,
            -1848368739,
            -1035763073,
            -190780785,
            -550347177,
            1500925016,
            1437126442,
            519594446,
            1413187371,
            1342464176,
            -948829372,
            338220432,
            1075555701,
            1107966991,
            -405152626,
            885756908,
            1781429436,
            -1248359543,
            1930882775,
            -1023447729,
            776184575,
            -1776185420,
            -197147162,
            368191321,
            254309271,
            2026076529,
            207200483,
            -2045308299,
            -42303174,
            393888353,
            -893114122,
            -1652821467,
            1013329911,
            -1212275031,
            -1223237597,
            -982531572,
            -728539053,
            -910962270,
            1946625558,
            2051508718,
            239492112,
            920306374,
            1888438146,
            272205552,
            -1992828732,
            1316648054,
            711901167,
            -227061755,
            1991494706,
            -1463743939,
            -1429437264,
            -1677789234,
            27033010,
            889818406,
            2120130511,
            838685283,
            -1020431159,
            -1447681559,
            1543931499,
            1701450624,
            340291898,
            -2041685008,
            -642608865,
            -1264354268,
            668467214,
            -502195954,
            1011598562,
            -891120940,
            -18398025,
            -872784146,
            -1306074314,
            -1890952940,
            1774846173,
            546827942,
            -2062889184,
            -176635891,
            130962589,
            1609617895,
            -752497691,
            1737094319,
            -1204251591,
            1373390714,
            996499903,
            1739173235,
            -5479653,
            761708175,
            456661554,
            -1428622127,
            -992845609,
            -1231371160,
            720693755,
            703855057,
            24002365,
            1833539318,
            -1902553960,
            1564471782,
            -1536154964,
            -725970636,
            827574885,
            308262790,
            -1904897132,
            -48868009,
            1818395686,
            2049718375,
            216030657,
            -2076287065,
            -374527357,
            -1857663329,
            -1867159867,
            911651337,
            -1116041313,
            1015012981,
            -1461908217,
            36810380,
            1427153555,
            283948267,
            -1859992197,
            320854256,
            -439452078,
            -1897431054,
            645774080,
            2120038965,
            899635523,
            -453852320,
            -684382235,
            -815851463,
            1795767067,
            -1042390945,
            645231946,
            1645674613,
            923341943,
            -1298716645,
            1350616857,
            1714199852,
            1425919976,
            9467943,
            1924030334,
            1150266519,
            1536367999,
            -849772278,
            -106323690,
            -2042007659,
            1755793225,
            239858268,
            749848321,
            -1666470363,
            -353187150,
            560831900,
            612934610,
            1956494919,
            964838196,
            961976194,
            1878909644,
            1709395619,
            19193616,
            -1572101598,
            161378502,
            -421709054,
            1282049587,
            -1844444717,
            -822900180,
            -1184592117,
            -1185205679,
            1438783233,
            -551608542,
            -311575617,
            -519068795,
            -1789571019,
            -1716946115,
            -1922281023,
            1780022985,
            -710818483,
            14722111,
            -283574096,
            -770740285,
            1653893025,
            2059227086,
            1417577297,
            1693207013,
            -664582244,
            868499217,
            -1148826190,
            -1207991715,
            -2083448347,
            -1726331785,
            1248599813,
            -1421582160,
            -1037226769,
            -2069558801,
            -543490328,
            -1474383439,
            -1881825907,
            -495720969,
            -1230442770,
            -1091549377,
            1770281453,
            520341586,
            -610054759,
            2000998394,
            -431157263,
            56642071,
            -1679881977,
            -1045015371,
            1104171198,
            -1425071302,
            969847031,
            1980513646,
            -359451089,
            -64988855,
            781635019,
            1413743677,
            308207762,
            -1154592059,
            -90456267,
            -1517873911,
            -2051651622,
            1709345781,
            -1821777087,
            -538477509,
            -1083130717,
            -1225363909,
            1219957182,
            -853859998,
            -662750590,
            -726591477,
            -1932297301,
            -1375589668,
            -1240156945,
            452874391,
            -8873588,
            97297972,
            -1152174184,
            73386408,
            -129553421,
            1242124150,
            -1033001619,
            -340230128,
            -1663512092,
            145369505,
            1436076651,
            1335309163,
            486670049,
            1145337974,
            -1647153464,
            190770132,
            747286790,
            -352193203,
            -995467546,
            -1815392278,
            2088680867,
            -1563640173,
            1204471037,
            159994461,
            -1686014385,
            -794543736,
            -384976104,
            -1663022887,
            574422567,
            1558432213,
            -320948292,
            458025182,
            320433149,
            -1215222675,
            -522504255,
            -1320876379,
            -543497392,
            1557126584,
            185711165,
            -131296141,
            1378348636,
            -1032171637,
            -52575179,
            736699661,
            -228773386,
            1504256620,
            262671971,
            174080689,
            362975687,
            812467272,
            -2023754432,
            1099436502,
            -1627599682,
            -1586611409,
            -199073634,
            202981272,
            1117236368,
            -626684119,
            1289778077,
            993120320,
            757543979,
            1083279016,
            -1501157055,
            -1765048490,
            -2030220382,
            1544229216,
            1388116908,
            551491569,
            933053701,
            1173348778,
            -658747851,
            1335311341,
            -681066206,
            245182344,
            1804626822,
            865041037,
            -1775213343,
            426403179,
            543652229,
            1243635233,
            464151082,
            -1977105237,
            -522980862,
            -1128607325,
            1575804630,
            -607040053,
            -122922994,
            -267021114,
            -1743257725,
            -1241212535,
            878161517,
            192682307,
            1196685123,
            997554217,
            -952356348,
            374758529,
            1415151278,
            580361003,
            1859711902,
            1194028902,
            479144380,
            995767216,
            -868672903,
            2065277225,
            825720073,
            -1212951353,
            -251167274,
            582134182

        };
            foreach (int Door in Doors)
            {
                API.triggerClientEvent(sender, "doormanager_finddoor_ex", Door);
            }
        }
        object[] currargs;
        [Command("y")]
        public void Save(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                File.AppendAllText("doors.txt", currargs[0].ToString() + " | " + currargs[1].ToString() + "\n");
                API.sendChatMessageToPlayer(player, "Model " + currargs[0].ToString() + " saved");
            }
        }

        [Command("findobjname")]
        public void Debug_FindDoorByNameCMD(Client player, string modelName)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                if (!_debugStatus) return;

                var model = API.getHashKey(modelName);

                var pos = API.getEntityPosition(player);

                API.triggerClientEvent(player, "doormanager_finddoor", model, pos);
            }
        }

        [Command("refresh")]
        public void Debug_refreshcmd(Client player)
        {
            refreshallDoor(player);
        }

        [Command("transition")]
        public void Debug_TransitionDoorCMD(Client player, int doorid, float target, int time)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (objplayer.adminlvl < 7)
            {
                API.sendChatMessageToPlayer(player, "~r~Tu n'as pas le droit d'utiliser cette commande admin.");
            }
            else
            {
                transitionDoor(doorid, target, time);
            }
        }

        private void ClientEventTrigger(Client sender, string eventName, object[] args)
        {
            if (eventName == "doormanager_debug_createdoor")
            {
                if (!_debugStatus) return;

                var model = (int)args[0];
                var pos = (Vector3)args[1];

                var id = registerDoor(model, pos);

                API.sendChatMessageToPlayer(sender, "Your door id is " + id + " " + pos.X + ", " + pos.Y + ", " + pos.Z);
            }
            else if (eventName == "SaveModel")
            {
                currargs = args;
            }
        }

        private void ColShapeTrigger(ColShape colshape, NetHandle entity)
        {
            var player = API.getPlayerFromHandle(entity);

            if (player == null) return;
            if (colshape != null && colshape.getData("IS_DOOR_TRIGGER") == true)
            {
                var id = colshape.getData("DOOR_ID");
                var info = colshape.getData("DOOR_INFO");

                float heading = 0f;

                if (info.State != null) heading = info.State;

                API.sendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                    info.Hash, info.Position.X, info.Position.Y, info.Position.Z,
                    info.Locked, heading, false);
            }
        }

        /* EXPORTED METHODS */

        public int registerDoor(int modelHash, Vector3 position)
        {
            var colShapeId = ++_doorCounter;

            var info = new DoorInfo();
            info.Hash = modelHash;
            info.Position = position;
            info.Locked = false; // Open by default;
            info.Id = colShapeId;
            info.State = 0;

            var colShape = API.createSphereColShape(position, 35f);
            colShape.setData("DOOR_INFO", info);
            colShape.setData("DOOR_ID", colShapeId);
            colShape.setData("IS_DOOR_TRIGGER", true);

            _doorColShapes.Add(colShapeId, colShape);
            API.consoleOutput("Creation porte : " + info.Id.ToString());
            return colShapeId;
        }

        public void transitionDoor(int doorId, float finish, int ms)
        {
            if (_doorColShapes.ContainsKey(doorId))
            {
                var info = _doorColShapes[doorId].getData("DOOR_INFO");

                info.Locked = true;

                foreach (var entity in _doorColShapes[doorId].getAllEntities())
                {
                    var player = API.getPlayerFromHandle(entity);

                    if (player == null) continue;

                    API.triggerClientEvent(player, "doormanager_transitiondoor",
                        info.Hash, info.Position, info.State, finish, ms);
                }

                info.State = finish;
            }
        }

        public void refreshDoorState(int doorId)
        {
            if (_doorColShapes.ContainsKey(doorId))
            {
                var info = _doorColShapes[doorId].getData("DOOR_INFO");

                float heading = info.State;

                foreach (var entity in _doorColShapes[doorId].getAllEntities())
                {
                    var player = API.getPlayerFromHandle(entity);

                    if (player == null) continue;

                    API.sendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                        info.Hash, info.Position.X, info.Position.Y, info.Position.Z,
                        info.Locked, heading, false);
                }
            }
        }

        public void refreshallDoor(Client player)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (_doorColShapes.ContainsKey(i))
                {
                    var info = _doorColShapes[i].getData("DOOR_INFO");

                    float heading = info.State;                    

                    API.sendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                        info.Hash, info.Position.X, info.Position.Y, info.Position.Z,
                        info.Locked, heading, false);
                }
            }
        }

        public void removeDoor(int id)
        {
            if (_doorColShapes.ContainsKey(id))
            {
                API.deleteColShape(_doorColShapes[id]);
                _doorColShapes.Remove(id);
            }
        }

        public void setDoorState(int doorId, bool locked, float heading)
        {
            if (_doorColShapes.ContainsKey(doorId))
            {
                var door = _doorColShapes[doorId];
                var data = door.getData("DOOR_INFO");
                data.Locked = locked;
                data.State = heading;

                door.setData("DOOR_INFO", data);

                foreach (var entity in door.getAllEntities())
                {
                    var player = API.getPlayerFromHandle(entity);

                    if (player == null) continue;

                    float cH = data.State;

                    API.sendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                        data.Hash, data.Position.X, data.Position.Y, data.Position.Z,
                        data.Locked, cH, false);
                }
            }
        }

        public int getCloseDoor(Client player)
        {
            var localCopy = new Dictionary<int, ColShape>(_doorColShapes);
            return localCopy.FirstOrDefault(pair => pair.Value.containsEntity(player)).Key;
        }

        public int[] getAllCloseDoors(Client player)
        {
            var localCopy = new Dictionary<int, ColShape>(_doorColShapes);
            var list = new List<int>();
            foreach (var sh in localCopy)
            {
                if (sh.Value.containsEntity(player))
                    list.Add(sh.Key);
            }

            return list.ToArray();
        }

        public void setDebug(bool status)
        {
            _debugStatus = status;
        }
    }

    public struct DoorInfo
    {
        public int Hash;
        public Vector3 Position;
        public int Id;

        public bool Locked;
        public float State;
    }
}