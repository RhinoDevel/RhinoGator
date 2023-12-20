
// Marcel Timm, RhinoDevel, 2023dec20

using System.Diagnostics;

namespace RhinoGator
{
    internal static class GameLoop
    {
        private const byte _consoleWidth = 80;
        private const byte _consoleHeight = 25;

        private const int _freq = 25; // Hz / FPS.
        private const double _ms = 1000.0 * 1.0 / (double)_freq;
        private const long _ticks = (long)(10.0 * 1000.0 * _ms + 0.5); // Rounds

        private static void BlitToConsole()
        {
            Console.SetCursorPosition(0, 0);

            for(int row = 0; row < _consoleHeight; ++row)
            {
                //int rowIndex = row * _consoleWidth;

                for (int col = 0; col < _consoleWidth; ++col)
                {
                    Console.Write('.'); // TODO: Implement correctly!
                }
                Console.WriteLine();
            }
        }

        internal static void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.CursorVisible = false;

            while (true)
            {
                var beginTicks = DateTime.Now.Ticks;

                // TODO: Call input handling routine!
                
                // TODO: Call (graphical) output updating/creating routine!

                BlitToConsole();

                var endTicks = DateTime.Now.Ticks;

                var elapsedTicks = endTicks - beginTicks;

                Debug.Assert(_ticks >= elapsedTicks);

                var leftTicks = _ticks - elapsedTicks;

                Thread.Sleep(new TimeSpan(leftTicks));
                //
                // (each tick equals 100 nanoseconds)
            }
        }
    }
}
