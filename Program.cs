using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SirHodlerBot;

namespace SirHodlerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Message.GetQuoteOfTheDay());

            Console.ReadLine();
        }
    }
}
