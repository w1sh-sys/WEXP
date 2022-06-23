using Rocket.API;
using Rocket.Core;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChooseSide
{
    internal class Command : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "WEXP | by W1SH";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string> { "wexp"};

        public List<string> Permissions => new List<string> { "wexp.w1" };

        public UnturnedPlayer uplayer;


        public void Execute(IRocketPlayer caller, string[] command)
        {
            uplayer = (UnturnedPlayer)caller;


            if (command.Length == 1 & (command[0].StartsWith("-")))
            {
                uint exp_amount = Convert.ToUInt32(command[0].Substring(1));

                if (uplayer.Experience < exp_amount)
                {
                    uplayer.Experience = 0u;

                    UnturnedChat.Say(uplayer.CSteamID, $"Ваш опыт был обнулён!", UnityEngine.Color.yellow);
                    Rocket.Core.Logging.Logger.Log($"{uplayer.SteamName}[{uplayer.CSteamID}] обнулил свой счёт.");

                }

                else
                {
                    uplayer.Experience -= exp_amount;

                    UnturnedChat.Say(uplayer.CSteamID, $"C Вас было снято {exp_amount} опыта!", UnityEngine.Color.yellow);
                    Rocket.Core.Logging.Logger.Log($"{uplayer.SteamName}[{uplayer.CSteamID}] снял с себя {exp_amount} опыта.");
                }

            }

            else if (command.Length == 0 || command.Length > 2)
            {
                UnturnedChat.Say(uplayer.CSteamID, "Команда введена НЕВЕРНО!", UnityEngine.Color.red);
            }


            else if (command.Length == 2)
            {
                UnturnedPlayer subject = UnturnedPlayer.FromName(command[0]);

                if (command[1].StartsWith("-"))
                {

                    uint exp_amount = Convert.ToUInt32(command[1].Substring(1));

                    if (subject.Experience < exp_amount)
                    {
                        subject.Experience = 0u;

                        UnturnedChat.Say(subject.CSteamID, $"Ваш опыт был обнулён!", UnityEngine.Color.yellow);
                        Rocket.Core.Logging.Logger.Log($"{uplayer.SteamName}[{uplayer.CSteamID}] обнулил опыт {subject.SteamName}.");

                    }

                    else
                    {
                        subject.Experience -= exp_amount;

                        UnturnedChat.Say(subject.CSteamID, $"C Вас было снято {exp_amount} опыта!", UnityEngine.Color.yellow);
                        Rocket.Core.Logging.Logger.Log($"{uplayer.SteamName}[{uplayer.CSteamID}] снял с {subject.SteamName} {exp_amount} опыта.");

                    }

                }

                else
                {
                    uint exp_amount = Convert.ToUInt32(command[1]);

                    subject.Experience += exp_amount;

                    UnturnedChat.Say(subject.CSteamID, $"Вам было зачислено {exp_amount} опыта!", UnityEngine.Color.green);
                    Rocket.Core.Logging.Logger.Log($"{uplayer.SteamName}[{uplayer.CSteamID}] начислил {subject.SteamName} {exp_amount} опыта.");

                }

            }

            else
            {
                uint exp_amount = Convert.ToUInt32(command[0]);

                uplayer.Experience += exp_amount;

                UnturnedChat.Say(uplayer.CSteamID, $"Вам было зачислено {exp_amount} опыта!", UnityEngine.Color.green);
                Rocket.Core.Logging.Logger.Log($"{uplayer.SteamName}[{uplayer.CSteamID}] начислил себе {exp_amount} опыта.");

            }

                

        }
    }
}