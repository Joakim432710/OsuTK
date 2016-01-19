using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuTK
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new OsuTK())
            {
                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
