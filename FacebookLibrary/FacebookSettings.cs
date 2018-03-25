using SirHodlerBot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ConsoleApplication
{
    public static class FacebookSettings
    {
        public static string AccessToken = "EAACZADZClcmjYBAFhmnrVQKAkgjrfVWU6pAfiW9jcvxR0Y3t8ZBEBaMWFYKB0a70XbBozf2YEFZCZCWCxZCddCeEj1nvkCZAFauCLcAlaZAdCcMlMnsU9Om3KuIHFYc8t3uoZC6OQspbgIAN8nARn5ANoqCTqqS79JpZCoLH5c5b8vVAZDZD";
        public static string PageAccessToken = "";

        static FacebookSettings()
        {
            string JSONString = HTTPMethod.Get($"https://graph.facebook.com/v2.12/me/accounts?access_token={AccessToken}&fields=id%2C%20global_brand_page_name%2C%20access_token&format=json&method=get&pretty=0&suppress_http_code=1");
            JObject Obj = JObject.Parse(JSONString);
            PageAccessToken = (string)Obj["data"][0]["access_token"];
        }
    }
}