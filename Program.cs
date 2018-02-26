using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirHodlerBot;

namespace SirHodlerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            HTTPMethod Method = new HTTPMethod();
            Console.WriteLine(Method.Get());
            Console.ReadLine();
        }
    }
}
