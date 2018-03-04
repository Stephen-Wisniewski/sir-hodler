using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SirHodlerBot
{
    public static class Message
    {
        public static string GetQuoteOfTheDay()
        {
            string JSONString = HTTPMethod.Get("http://quotes.rest/qod.json");
            JObject JSON = JObject.Parse(JSONString);

            string Quote = (string)JSON["contents"]["quotes"][0]["quote"];
            string Author = (string)JSON["contents"]["quotes"][0]["author"];

            return "\"" + Quote + "\" - " + Author;
        }

        // Next time add a paramter for picking which currency to check for volatility?
        public static string GetVolatilityMessage()
        {
            double ChangeIn24Hours = Get7DayPriceChange("ethereum");
            string Message = "";

            if (ChangeIn24Hours >= 2)
                Message = "Starting to moon, boys?";

            if (ChangeIn24Hours >= 5)
                Message = "Fasten your seat belts... We're going to the moon!";

            if (ChangeIn24Hours >= 10)
                Message = "Bitcoin is up " + ChangeIn24Hours + ". We're mooning! When lambo?";

            if (ChangeIn24Hours >= 15)
                Message = "Fuck the moon. We're going to mars.";

            return Message;
        }

        public static double Get1HourPriceChange(string Coin)
        {
            return GetCoinPriceChange(Coin, "1h");
        }

        public static double Get24HourPriceChange(string Coin)
        {
            return GetCoinPriceChange(Coin, "24h");
        }

        public static double Get7DayPriceChange(string Coin)
        {
            return GetCoinPriceChange(Coin, "7d");
        }

        private static double GetCoinPriceChange(string Coin, string Time)
        {
            string JSONString = HTTPMethod.Get("https://api.coinmarketcap.com/v1/ticker/" + Coin + "/");
            JSONString = JSONString.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });

            JObject JSON = JObject.Parse(JSONString);

            double hourchange = (double)JSON["percent_change_" + Time];

            return hourchange;
        }
    }
}
