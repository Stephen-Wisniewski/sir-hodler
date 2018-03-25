using SirHodlerBot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ConsoleApplication
{
    public static class FacebookSettings
    {
        public static string AccessToken = "EAACEdEose0cBAN9vxdoFAqJbPJbY72IkgsZCXYAkJlIYNzcihTQum9xQ27bsBMJk517IERK3GbR045zXcBirK962EJabYHWWLVIQ7PS7pmwWfDOjO7lrKjMc1hMMGxncveIXFZBp8U1GUv8suYIQjMQPZBt12sobG6cELPJbtF9LZCtuOoKM2aEstBJwboZBubGrOM2e1SwZDZD";
        public static string PageAccessToken = "";

        static FacebookSettings()
        {
            string JSONString = HTTPMethod.Get($"https://graph.facebook.com/v2.12/me/accounts?access_token={AccessToken}&fields=id%2C%20global_brand_page_name%2C%20access_token&format=json&method=get&pretty=0&suppress_http_code=1");
            JObject Obj = JObject.Parse(JSONString);
            PageAccessToken = (string)Obj["data"][0]["access_token"];
        }
    }
}