using System;
using System.Reflection;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System.Data;

/* Vous pouvez get l'int de la touche via ce site : http://keycode.info/ */

namespace GenerationFiveRP
{
    public class KeyManager : Script
    {
        public KeyManager()
        {
            API.onClientEventTrigger += ClientEventTrigger;
        }
        public void ClientEventTrigger(Client sender, string eventName, params object[] args)
        {
            if (args == null)
                return;
            
            switch (eventName)
            {
                #region OnKeyDown
                case "keymanageronKeyDown":
                    {
                        PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(sender);
                        switch ((int)args[0])
                        {
                            case 69: /* Touche E */
                                {
                                    if (objplayer.Logged)
                                    {
                                        API.call("Logement", "ScriptEvent", sender, objplayer);
                                        API.call("convoyeur", "ScriptEvent", sender, objplayer, "DepotConvKey");
                                        API.call("Essence", "ScriptEvent", sender, objplayer, "RefuelKeyPressed");
                                        API.call("CreationMenus", "ClientEvent", sender, objplayer, "Bouton.E");
                                    }
                                    break;
                                }
                            case 87: /* Touche W */
                                {
                                    if (objplayer.Logged)
                                    {
                                        API.call("convoyeur", "ScriptEvent", sender, objplayer, "ConvoyeurKeyPressed");
                                    }
                                    break;
                                }
                            case 65: /* Touche A */
                                {
                                    break;
                                }
                            case 82: /* Touche R */
                                {
                                    API.call("CreationMenus", "ClientEvent", sender, objplayer, "Bouton.R");
                                    break;
                                }
                            case 112: /* Touche F1 */
                                {
                                    API.call("CreationMenus", "ClientEvent", sender, objplayer, "Bouton.F1");
                                    break;
                                }
                            case 113: /* Touche F2 */
                                {
                                    API.call("CreationMenus", "ClientEvent", sender, objplayer, "Bouton.F2");
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                #endregion
                #region OnKeyUp
                case "keymanageronKeyUp":
                    {
                        PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(sender);
                        switch ((int)args[0])
                        {
                            case 69: /* Touche E */
                                {
                                    API.call("Essence", "ScriptEvent", sender, objplayer, "RefuelKeyReleased");
                                    break;
                                }

                        }
                        break;
                    }
                #endregion
                default:
                    return;
            }
            
        }
    }
}
