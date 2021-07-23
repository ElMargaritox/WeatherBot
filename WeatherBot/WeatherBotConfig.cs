using System;
using Rocket.API;

namespace WeatherBot
{
    public class WeatherBotConfig : IRocketPluginConfiguration
    {
        public string IMG_URL { get; set; }
        public int Interval { get; set; }
        public int Vote_Airdrop { get; set; }
        public int Vote_Day { get; set; }
        public int Vote_Night { get; set; }
        public int Vote_Rain { get; set; }
        public int Vote_NoRain { get; set; }
        public void LoadDefaults()
        {
            Interval = 10;
            IMG_URL = "IMG";
            Vote_Airdrop = 10;
            Vote_Day = 5;
            Vote_Night = 10;
            Vote_Rain = 5;
            Vote_NoRain = 5;
            
        }
    }
}
