﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication;
using Newtonsoft.Json.Linq;
using SirHodlerBot;

namespace SirHodlerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var getAccountTask = facebookService.GetAccountAsync(FacebookSettings.AccessToken);
            Task.WaitAll(getAccountTask);
            var account = getAccountTask.Result;
            Console.WriteLine(account.Id);

            var postOnWallTask = facebookService.PostOnWallAsync(FacebookSettings.AccessToken, "I'm alive!");


            Console.ReadLine();
        }
    }
}
