using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatercolorGames.Thelma;

namespace WatercolorGames.Thelma.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var ai = new TestAI();
            while(true)
            {
                Console.Write("> ");
                Console.WriteLine(ai.Respond(Console.ReadLine()));
            }
        }
    }
}
