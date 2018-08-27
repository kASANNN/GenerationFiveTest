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
    public class armurerie : Script
    {
        public armurerie()
        {
            API.onResourceStart += OnResourceStartHandler;
        }

        public void OnResourceStartHandler()
        {
            BlipsArmurerie();
        }

        public class Armurerie
        {
            public Vector3 Position { get; set; }

            public Armurerie(Vector3 position)
            {
                Position = position;
                var b = API.shared.createBlip(Position);
                API.shared.setBlipSprite(b, 110);
                API.shared.setBlipTransparency(b, 125);
                API.shared.setBlipShortRange(b, true);
            }
        }

        public void BlipsArmurerie()
        {
            new Armurerie(new Vector3(251.97, -50.09469, 69.94105));
            new Armurerie(new Vector3(-661.8865, -934.9248, 21.82922));
            new Armurerie(new Vector3(841.753, -1033.951, 28.19487));
            new Armurerie(new Vector3(809.9454, -2157.674, 29.61901));
            new Armurerie(new Vector3(22.64655, -1106.974, 29.79702));
            API.createPed((PedHash)(-1643617475), new Vector3(253.6227, -51.88472, 69.9410), 62, 0);
            API.createPed((PedHash)(233415434), new Vector3(-660.982, -933.3273, 21.82922), 176, 0);
            API.createPed((PedHash)(-1643617475), new Vector3(841.0432, -1035.523, 28.19485), 0);
            API.createPed((PedHash)(233415434), new Vector3(808.8241, -2159.223, 29.619), 0);
            API.createPed((PedHash)(-1643617475), new Vector3(23.86027, -1105.851, 29.79701), 149, 0);
        }
    }
}