using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace WeatherBot.Commands
{
    class CommandVote : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "vote";

        public string Help => "vote";

        public string Syntax => "Use /vote [Airdrop] [Day] [Night] [Rain] [NoRain]";

        public List<string> Aliases => throw new NotImplementedException();

        public List<string> Permissions => throw new NotImplementedException();

        public void Execute(IRocketPlayer caller, string[] command)
        {
           
            if(command.Length < 1)
            {
               UnturnedChat.Say(caller, string.Format("{0}", Syntax)); return;
            }

            if(WeatherBotPlugin.Instance.VoteSelected > 0)
            {
                UnturnedChat.Say(caller, string.Format("{0}", WeatherBotPlugin.Instance.Translate("In_Process")));
            }

            switch (command[0].ToLower())
            {
                case "vote":
                    switch (command[1].ToLower())
                    {
                        case "day":
                            WeatherBotPlugin.Instance.VoteSelected = 1;
                            WeatherBotPlugin.Instance.VoteCount = 0; WeatherBotPlugin.Instance.timer.Start();
                            break;
                        case "night":
                            WeatherBotPlugin.Instance.VoteSelected = 2;
                            WeatherBotPlugin.Instance.VoteCount = 0; WeatherBotPlugin.Instance.timer.Start();
                            break;
                        case "rain":
                            WeatherBotPlugin.Instance.VoteSelected = 3;
                            WeatherBotPlugin.Instance.VoteCount = 0; WeatherBotPlugin.Instance.timer.Start();
                            break;
                        case "norain":
                            WeatherBotPlugin.Instance.VoteSelected = 4;
                            WeatherBotPlugin.Instance.VoteCount = 0; WeatherBotPlugin.Instance.timer.Start();
                            break;
                        case "airdrop":
                            WeatherBotPlugin.Instance.VoteSelected = 5;
                            WeatherBotPlugin.Instance.VoteCount = 0; WeatherBotPlugin.Instance.timer.Start();
                            break;
                        default:
                            UnturnedChat.Say(caller, string.Format("Incorrect Format: {0}", Syntax));
                            break;
                    }
                    break;
                case "admin":
                    switch (command[1].ToLower())
                    {
                        case "reset":
                            // Reset All Counters
                            break;
                    }
                    break;
            }



        }
    }
}
