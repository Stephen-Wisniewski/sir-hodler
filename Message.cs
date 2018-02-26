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
            string JSONString = HTTPMethod.Get();
            JObject JSON = JObject.Parse(JSONString);

            return (string)JSON["contents"]["quotes"][0]["quote"];
        }
    }
}
