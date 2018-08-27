using System.Collections.Generic;
using System.IO;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using Newtonsoft.Json;

namespace GenerationFiveRP
{
    public class SaveWeapons : Script
    {
        public class CWeaponData
        {
            public int Ammo { get; set; }
            public WeaponTint Tint { get; set; }
            public string Components { get; set; }
        }

        const string DIR_NAME = "playerWeapons/";

        public SaveWeapons()
        {
            API.onResourceStart += SaveWeapons_DirCheck;
        }

        public void SaveWeapons_DirCheck()
        {
            if (!Directory.Exists(DIR_NAME)) Directory.CreateDirectory(DIR_NAME);
        }

        public string GetFileName(string name)
        {
            return DIR_NAME + name + ".json";
        }

        public void Save(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            if (!Directory.Exists(DIR_NAME)) Directory.CreateDirectory(DIR_NAME);
            string fileName = GetFileName(player.name);
            Dictionary<WeaponHash, CWeaponData> weaponData = new Dictionary<WeaponHash, CWeaponData>();

            foreach (WeaponHash wepHash in API.getPlayerWeapons(player))
            {
                weaponData.Add(wepHash, new CWeaponData { Ammo = API.getPlayerWeaponAmmo(player, wepHash), Tint = API.getPlayerWeaponTint(player, wepHash), Components = JsonConvert.SerializeObject(API.getPlayerWeaponComponents(player, wepHash)) });
            }

            string jsonData = JsonConvert.SerializeObject(weaponData, Formatting.Indented);

            if (objplayer.IsFactionDuty != true)
            {
                File.WriteAllText(fileName, jsonData);
            }
        }

        public void Load(Client player)
        {
            string fileName = GetFileName(player.name);

            if (File.Exists(fileName))
            {
                string jsonData = File.ReadAllText(fileName);
                Dictionary<WeaponHash, CWeaponData> weaponData = JsonConvert.DeserializeObject<Dictionary<WeaponHash, CWeaponData>>(jsonData);

                foreach (KeyValuePair<WeaponHash, CWeaponData> weapon in weaponData)
                {
                    API.givePlayerWeapon(player, weapon.Key, weapon.Value.Ammo, false, true);
                    API.setPlayerWeaponTint(player, weapon.Key, weapon.Value.Tint);

                    List<WeaponComponent> weaponMods = JsonConvert.DeserializeObject<List<WeaponComponent>>(weapon.Value.Components);
                    foreach (WeaponComponent compID in weaponMods) API.givePlayerWeaponComponent(player, weapon.Key, compID);
                }
            }
            else
            {
                API.consoleOutput("[SaveWeapons_Load] " + fileName + " n'existe pas!");
            }
        }
    }
}