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
    public class AutoEcole : Script
    {
        public AutoEcole()
        {
            LoadAutoEcole();
            //API.onChatMessage += OnChatMessageHandler;
        }

        public void LoadAutoEcole()
        {
            API.requestIpl("ex_dt1_02_office_03c");
            API.createPed((PedHash)(-1358701087), new Vector3(-138.9804, -633.9983, 168.8203), 2.439642f);
            var b = API.createBlip(Constante.Pos_EntrerAutoEcole);
            API.setBlipTransparency(b, 150);
            API.setBlipColor(b, 2);
            API.setBlipSprite(b, 498);
            API.setBlipShortRange(b, true);
            API.setBlipName(b, "Auto-Ecole");
        }

        [Command("addquestion")]
        public void Addquestion(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            objplayer.addquestion = 1;
            API.sendChatMessageToPlayer(player, "[1/4] Ecris la question.");
        }

        public static void LoadQuestion(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM QuestionAutoEcole WHERE ID='" + objplayer.OrdreQuestionAutoEcole[objplayer.QuestionEnCours] + "'");
            //Affiche la question
            foreach (DataRow row in result.Rows)
            {
                int[] tableau = new int[3];
                Random aleatoire = new Random();
                for (int i = 0; i < tableau.Length; i++)
                {
                    bool ok = false;
                    while (ok == false)
                    {
                        int nbalea = aleatoire.Next(1, 4);
                        for (int z = 0; z < tableau.Length; z++)
                        {
                            if (nbalea == tableau[z]) break;
                            if (z == 2)
                            {
                                tableau[i] = nbalea;
                                API.shared.consoleOutput(Convert.ToString(tableau[i]));
                                ok = true;
                            }
                        }                        
                    }
                }
                API.shared.sendChatMessageToPlayer(player, Convert.ToString(row["Question"]));
                for (int z = 0; z < tableau.Length; z++)
                {
                    if(tableau[z] == 1)
                    {
                        API.shared.sendChatMessageToPlayer(player, "~b~" + (z + 1) + ")~s~ " + Convert.ToString(row["BR"]));
                        objplayer.BonneReponse = z + 1;
                    }
                    if(tableau[z] == 2)
                    {
                        API.shared.sendChatMessageToPlayer(player, "~b~" + (z + 1) + ")~s~ " + Convert.ToString(row["R2"]));
                    }
                    if(tableau[z] == 3)
                    {
                        API.shared.sendChatMessageToPlayer(player, "~b~" + (z + 1) + ")~s~ " + Convert.ToString(row["R3"]));
                    }
                }
            }
        }

        public static void PreparerQuestionnaire(Client player)
        {
            PlayerInfo objplayer = PlayerInfo.GetPlayerInfoObject(player);
            DataTable result = API.shared.exported.database.executeQueryWithResult("SELECT * FROM QuestionAutoEcole ORDER BY RAND()");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                //objplayer.OrdreQuestionAutoEcole[i] = Convert.ToInt32(result.Rows[i]["ID"]);
                objplayer.OrdreQuestionAutoEcole.Add(Convert.ToInt32(result.Rows[i]["ID"]));
            }
        }
    }
}
