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
    public class SystemeMort : Script
    {
        public SystemeMort()
        {
            API.onPlayerDeath += OnPlayerDeathHandler;
        }

        [Flags]
        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 0,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        }

        public void OnPlayerDeathHandler(Client player, NetHandle entityKiller, int weapon)
        {
            // Event pour qu'il respawn sur place
            API.sendNativeToPlayer(player, Hash._RESET_LOCALPLAYER_STATE, player);
            API.sendNativeToPlayer(player, Hash.RESET_PLAYER_ARREST_STATE, player);

            API.sendNativeToPlayer(player, Hash.IGNORE_NEXT_RESTART, true);
            API.sendNativeToPlayer(player, Hash._DISABLE_AUTOMATIC_RESPAWN, true);

            API.sendNativeToPlayer(player, Hash.SET_FADE_IN_AFTER_DEATH_ARREST, true);
            API.sendNativeToPlayer(player, Hash.SET_FADE_OUT_AFTER_DEATH, false);
            API.sendNativeToPlayer(player, Hash.NETWORK_REQUEST_CONTROL_OF_ENTITY, player);

            API.sendNativeToPlayer(player, Hash.FREEZE_ENTITY_POSITION, player, false);
            API.sendNativeToPlayer(player, Hash.NETWORK_RESURRECT_LOCAL_PLAYER, player.position.X, player.position.Y, player.position.Z, player.rotation.Z, false, false);
            API.sendNativeToPlayer(player, Hash.RESURRECT_PED, player);
            // Fin de l'event
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);

            API.playPlayerAnimation(player, (int)(AnimationFlags.StopOnLastFrame), "combat@death@from_writhe", "death_c");
            objplayer.IsDead = true;
            API.setPlayerHealth(player, 10);
            API.sendChatMessageToPlayer(player, "~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            API.sendChatMessageToPlayer(player, "Tu viens de tomber au sol, attends l'arrivée des secours, si tu ne veux pas");
            API.sendChatMessageToPlayer(player, "attendre tu peux accepter ta mort via le /acceptermort, tu payeras en contrepartie");
            API.sendChatMessageToPlayer(player, "la somme de ~r~" + Constante.PrixMort + "~s~$. Tu devras oublier tout ce qui s'est passé avant ta mort.");
            API.sendChatMessageToPlayer(player, "~#aea996~", "- - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
        }
    }
}