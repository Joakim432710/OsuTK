using System;

namespace OsuTK
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            using (var game = new OsuTK())
            {
                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}