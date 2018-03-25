using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            DateTime Day7TempTime = DateTime.Now;
            DateTime Day1TempTime = DateTime.Now;
            DateTime Minute10TempTime = DateTime.Now;

            while (true)
            {
                DateTime CurrentTime = DateTime.Now;

                if (CurrentTime > Day7TempTime.AddDays(7))
                {
                    PostMessage(Message.GetCoinOfTheWeek());
                    Day7TempTime = CurrentTime; 
                }

                if (CurrentTime > Day1TempTime.AddDays(1))
                {
                    PostMessage(Message.GetCoinOfTheDay());
                    int milliseconds = 20000;
                    Thread.Sleep(milliseconds);
                    PostMessage(Message.GetQuoteOfTheDay());
                    Day1TempTime = CurrentTime;
                }

                if (CurrentTime > Minute10TempTime.AddMinutes(10))
                {
                    PostMessage(Message.GetVolatilityMessage("bitcoin"));
                    Minute10TempTime = CurrentTime;
                }
            }
        }

        private static void PostMessage(string Message)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var getAccountTask = facebookService.GetAccountAsync(FacebookSettings.PageAccessToken);
            var postOnWallTask = facebookService.PostOnWallAsync(FacebookSettings.PageAccessToken, Message);

            Task.WaitAll(getAccountTask);
        }
    }
}
