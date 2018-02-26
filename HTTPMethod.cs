using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SirHodlerBot
{
    public class HTTPMethod
    {
        public string Get()
        {
            using (var Client = new WebClient())
            {
                var json = Client.DownloadString("http://quotes.rest/qod.json");

                return json;
            }
        }
    }
}
