using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace WeatherBot
{
    public class WeatherBotPlugin : RocketPlugin<WeatherBotConfig>
    {
        public static WeatherBotPlugin Instance { get; set; }
        public Timer timer { get; set; }
        
        public int VoteCount { get; set; }
        public int VoteSelected { get; set; }

        

        public override TranslationList DefaultTranslations
        {
            get
            {
                TranslationList translationList = new TranslationList();
                translationList.Add("Start", "x");
                translationList.Add("End", "d");
                translationList.Add("Day", "Day");
                translationList.Add("Night", "Night");
                translationList.Add("Rain", "Night");
                translationList.Add("NoRain", "NoRain");
                translationList.Add("Airdrop", "Airdrop");
                translationList.Add("Voting", "Se Ha Iniciado La Votacion De {0}. Escriban {0} para votar [{2}/{3}]");
                translationList.Add("Voting_Finish", "Ha Terminado La Votacion");
                translationList.Add("Voting_Failed", "Ha fallado el voto :(");
                translationList.Add("In_Process", "A vote is already in process");
                return translationList;
            }
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerChatted -= OnPlayerChatted;
                timer.Elapsed -= Timer_Elapsed;
            timer = null;
            Instance = null;
        }

        protected override void Load()
        {
            Instance = this; VoteCount = 0; VoteSelected = 0;
            timer = new Timer(Configuration.Instance.Interval * 1000); timer.Elapsed += Timer_Elapsed;
            UnturnedPlayerEvents.OnPlayerChatted += OnPlayerChatted;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Rocket.Core.Utils.TaskDispatcher.QueueOnMainThread(() =>
            {

                Instance.VoteCount = 0;
                Instance.VoteSelected = 0;

                timer.Stop();
                ChatManager.serverSendMessage(Translate("Voting_Failed"), Color.white, null, null, 
                    EChatMode.GLOBAL, Configuration.Instance.IMG_URL, true);

            });
        }

        private void MessageFinish()
        {
            ChatManager.serverSendMessage(Translate("Voting_Finish"), Color.white, null, null, EChatMode.GLOBAL, Configuration.Instance.IMG_URL, true);
        }

        private void OnPlayerChatted(UnturnedPlayer player, ref Color color, string message, EChatMode chatMode, ref bool cancel)
        {
            // 1 - Day
            // 2 - Night
            // 3 - Rain
            // 4 - NoRain
            // 5 - Airdrop

            string votetext = string.Empty;

            if(VoteSelected > 0)
            {
                if (VoteSelected == 1) {

                    string message_check = message.ToLower();

                    if (message_check.Contains(Translate("Day").ToLower()))
                    {
                        VoteCount++; votetext = Translate("Day").ToUpper();
                    }

                    if(VoteCount >= Configuration.Instance.Vote_Day)
                    {
                        R.Commands.Execute(new ConsolePlayer(), "/day"); MessageFinish();
                    }
                }
                else if(VoteSelected == 2)
                {
                    string message_check = message.ToLower();

                    if (message_check.Contains(Translate("Night").ToLower()))
                    {
                        VoteCount++; votetext = Translate("Night").ToUpper();
                    }

                    if (VoteCount >= Configuration.Instance.Vote_Night)
                    {
                        R.Commands.Execute(new ConsolePlayer(), "/night"); MessageFinish();
                    }

                }
                else if (VoteSelected == 3)
                {
                    string message_check = message.ToLower();

                    if (message_check.Contains(Translate("Rain").ToLower()))
                    {
                        VoteCount++; votetext = Translate("Rain").ToUpper();
                    }


                    if (VoteCount >= Configuration.Instance.Vote_Rain)
                    {
                        R.Commands.Execute(new ConsolePlayer(), "/weather rain"); MessageFinish();
                    }
                }
                else if (VoteSelected == 4)
                {
                    string message_check = message.ToLower();

                    if (message_check.Contains(Translate("NoRain").ToLower()))
                    {
                        VoteCount++; votetext = Translate("NoRain").ToUpper();
                    }


                    if (VoteCount >= Configuration.Instance.Vote_NoRain)
                    {
                        R.Commands.Execute(new ConsolePlayer(), "/weather none"); MessageFinish();
                    }
                }
                else if (VoteSelected == 5)
                {
                    string message_check = message.ToLower();

                    if (message_check.Contains(Translate("Airdrop").ToLower()))
                    {
                        VoteCount++; votetext = Translate("Airdrop").ToUpper();
                    }

                    if (VoteCount >= Configuration.Instance.Vote_Airdrop)
                    {
                        R.Commands.Execute(new ConsolePlayer(), "/airdrop"); MessageFinish();
                    }
                }
            }


            ChatManager.serverSendMessage(Translate("Voting", votetext), UnityEngine.Color.white, null, null, EChatMode.GLOBAL, Configuration.Instance.IMG_URL, true);

            
            

        }
    }
}
