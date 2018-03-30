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
            string UpOrDown = ((Func<string>)(() => { if (Double.Parse(Change24Hours) < 0) return "down"; return "up"; }))();

            return $"Today's coin of of the day is {Coin}.\n\nIt's gone {UpOrDown} by {Change24Hours} percent in the last day!";
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
            string UpOrDown = ((Func<string>)(() => { if (Double.Parse(Change7Days) < 0) return "down"; return "up"; }))();

            return $"This weeks coin of of the week is {Coin}.\n\nIt's gone {UpOrDown} by {Change7Days} percent in the last 7 days!";
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
            double Weight = CalculateCoinWeight(Coin);
            string Message = "";

            if (ChangeIn24Hours >= (-2 * Weight))
                Message = $"Slight dip. We're down by: {ChangeIn24Hours}";

            if (ChangeIn24Hours >= (-5 * Weight))
                Message = $"Uh-oh. We're down by: {ChangeIn24Hours}";

            if (ChangeIn24Hours >= (-10 * Weight))
                Message = $"Bills lost 2 dollars. :( We're down by {ChangeIn24Hours}!";

            if (ChangeIn24Hours >= (-15 * Weight))
                Message = $"BOGGED";

            if (ChangeIn24Hours >= (2*Weight))
                Message = $"{Coin} is up {ChangeIn24Hours}. It's a green day.)";

            if (ChangeIn24Hours >= (5*Weight))
                Message = $"{Coin} is up {ChangeIn24Hours}. Fasten your seat belts...";

            if (ChangeIn24Hours >= (10*Weight))
                Message = $"{Coin} is up {ChangeIn24Hours}. We're mooning! When lambo?";

            if (ChangeIn24Hours >= (15*Weight))
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
            string JSONString = HTTPMethod.Get($"https://api.coinmarketcap.com/v1/ticker/{Coin}/");
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

        private static long GetCoinMarketCap(string Coin)
        {
            string JSONString = HTTPMethod.Get("https://api.coinmarketcap.com/v1/ticker/" + Coin + "/");
            JSONString = JSONString.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            JObject JSON = JObject.Parse(JSONString);

            return (long)JSON["market_cap_usd"];
        }

        private static double CalculateCoinWeight(string Coin)
        {
            long MarketCap = GetCoinMarketCap(Coin);
            double Weight = 4;

            //TODO: Make marketcap comparison below relative to Bitcoin/rank 1 price

            if (MarketCap > 250000000)
                Weight = 2.5;

            if (MarketCap > 750000000)
                Weight = 1.75;

            if (MarketCap > 2000000000)
                Weight = 1.25;

            if (MarketCap > 7500000000)
                Weight = 1.1;

            if (MarketCap > 40000000000)
                Weight = 1;

            return Weight;
        }
    }
}