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
    public class Magasin : Script
    {
        public Magasin()
        {
            API.onResourceStart += onStart;
        }

        public static bool isMagasin(Client player)
        {
            if (player.position.DistanceTo(new Vector3(25.71328, -1345.572, 29.49702)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(1163.397, -322.2101, 69.20514)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(-707.4932, -912.8387, 19.21559)) < 2)
            {
                return true;
            }

            if (player.position.DistanceTo(new Vector3(-47.21673, -1756.61, 29.42099)) < 2)
            {
                return true;
            }
            return false;
        }

        public static bool isRevendeur(Client player)
        {
            if (player.position.DistanceTo(new Vector3(200.4324, -2002.58, 18.86158)) < 2)
            {
                return true;
            }
            return false;
        }

        public void onStart()
        {
            BlipsMagasin();
        }

        public class MagasinBlips
        {
            public Vector3 Position { get; set; }

            public MagasinBlips(Vector3 position)
            {
                Position = position;
                var b = API.shared.createBlip(Position);
                API.shared.setBlipSprite(b, 52);
                API.shared.setBlipTransparency(b, 125);
                API.shared.setBlipShortRange(b, true);
            }
        }

        public void BlipsMagasin()
        {
            new MagasinBlips(new Vector3(28.89022, -1347.248, 29.49702));
            new MagasinBlips(new Vector3(1159.325, -322.2101, 69.20514));
            new MagasinBlips(new Vector3(-711.7283, -914.6635, 19.21559));
            new MagasinBlips(new Vector3(-51.66341, -1754.796, 29.42101));
            API.createPed((PedHash)(824925120), new Vector3(24.20294, -1345.589, 29.49702), -98, 0);
            API.createPed((PedHash)(416176080), new Vector3(1164.552, -321.9401, 69.20514), 94, 0);
            API.createPed((PedHash)(824925120), new Vector3(-706.1612, -912.8073, 19.21559), 81, 0);
            API.createPed((PedHash)(416176080), new Vector3(-46.25598, -1757.323, 29.42101), 42, 0);
            API.createPed((PedHash)(275618457), new Vector3(200.4324, -2002.58, 18.86158), -144, 0);
        }
    }
}