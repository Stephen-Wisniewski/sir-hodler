﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SirHodlerBot
{
    public static class HTTPMethod
    {
        public static string Get(string URL)
        {
            using (var Client = new WebClient())
            {
                return Client.DownloadString(URL);
            }
        }
    }
}
