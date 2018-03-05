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
        public static string GetCoinOfTheDay()
        {
            string JSONString = HTTPMethod.Get("https://api.coinmarketcap.com/v1/ticker/?limit=100");
            JArray Array = JArray.Parse(JSONString);
            int Index = -1;
            double BiggestChange = 0;

            for (int i = 0; i < 100; i++)
            {
                double Change24HoursComparison = (double)Array.ElementAt(i)["percent_change_24h"];
                if (Change24HoursComparison > BiggestChange)
                {
                    Index = i;
                    BiggestChange = Change24HoursComparison;
                }
            }
            if (Index == -1)
                return "Coin not found.";

            JObject JObj = (JObject)Array.ElementAt(Index);

            string Coin = (string)JObj["name"];
            string Change24Hours = (string)JObj["percent_change_24h"];

            return $"Today's coin of of the day is {Coin}. \n\n It's gone up by {Change24Hours} percent in the last day!";
        }

        public static string GetCoinOfTheWeek()
        {
            string JSONString = HTTPMethod.Get("https://api.coinmarketcap.com/v1/ticker/?limit=100");
            JArray Array = JArray.Parse(JSONString);
            int Index = -1;
            double BiggestChange = 0;

            for (int i = 0; i < 100; i++)
            {
                double Change24HoursComparison = (double)Array.ElementAt(i)["percent_change_7d"];
                if (Change24HoursComparison > BiggestChange)
                {
                    Index = i;
                    BiggestChange = Change24HoursComparison;
                }
            }
            if (Index == -1)
                return "Coin not found.";

            JObject JObj = (JObject)Array.ElementAt(Index);

            string Coin = (string)JObj["name"];
            string Change7Days = (string)JObj["percent_change_7d"];

            return $"This weeks coin of of the week is {Coin}. \n\n It's gone up by {Change7Days} percent in the last 7 days!";
        }

        public static string GetQuoteOfTheDay()
        {
            string JSONString = HTTPMethod.Get("http://quotes.rest/qod.json");
            JObject JSON = JObject.Parse(JSONString);

            string Quote = (string)JSON["contents"]["quotes"][0]["quote"];
            string Author = (string)JSON["contents"]["quotes"][0]["author"];

            return $"\"{Quote}\" - {Author}";
        }

        public static string GetVolatilityMessage(string Coin)
        {
            double ChangeIn24Hours = Get7DayPriceChange(Coin);
            double Weight = CalculateCoinWeight(GetCoinRank(Coin));
            string Message = "";

            if (ChangeIn24Hours >= 2)
                Message = $"{Coin} is up {ChangeIn24Hours}. It's a green day.)";

            if (ChangeIn24Hours >= 5)
                Message = $"{Coin} is up {ChangeIn24Hours}. Fasten your seat belts...";

            if (ChangeIn24Hours >= 10)
                Message = $"{Coin} is up {ChangeIn24Hours}. We're mooning! When lambo?";

            if (ChangeIn24Hours >= 15)
                Message = $"{Coin} is up {ChangeIn24Hours}. We're going to mars!";

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

            double hourchange = (double)JSON[$"percent_change_{Time}"];

            return hourchange;
        }

        private static int GetCoinRank(string Coin)
        {
            string JSONString = HTTPMethod.Get("https://api.coinmarketcap.com/v1/ticker/" + Coin + "/");
            JSONString = JSONString.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            JObject JSON = JObject.Parse(JSONString);

            return (int)JSON["rank"];
        }

        private static double CalculateCoinWeight(int Rank)
        {
            return (Rank / 10);
        }
    }
}
