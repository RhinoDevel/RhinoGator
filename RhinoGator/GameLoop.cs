
// Marcel Timm, RhinoDevel, 2023dec20

using System.Diagnostics;

namespace RhinoGator
{
    internal static class GameLoop
    {
        private const int _w = 80;
        private const int _h = 25;

        private static readonly byte[] _frameBuf = new byte[_w * _h];

        private const int _freq = 25; // Hz / FPS.
        private const double _ms = 1000.0 * 1.0 / (double)_freq;
        private const long _ticks = (long)(10.0 * 1000.0 * _ms + 0.5); // Rounds

        private const int _stepNs = 10; // Nanoseconds per (time-)step.
        private const int _steps = // (Time-)steps per iteration.
            (int)((1000.0 * 1000.0 * _ms) / _stepNs + 0.5); // Rounds

        private static void BlitToConsole()
        {
            Console.SetCursorPosition(0, 0);

            // Much faster:
            //
            using (var stdout = Console.OpenStandardOutput())
            {
                for(int row = 0; row < _h; ++row)
                {
                    stdout.Write(_frameBuf, row * _w, _w);

                    Console.WriteLine();
                }
            }
            //
            // for(int row = 0; row < _h; ++row)
            // {
            //     int rowIndex = row * _w;
            //
            //     for (int col = 0; col < _w; ++col)
            //     {
            //         Console.Write((char)_frameBuf[rowIndex + col]);
            //     }
            //     Console.WriteLine();
            // }
        }

        /// <summary>
        /// Returns a list of keys that are "currently" being pressed by the
        /// user.
        /// </summary>
        /// <remarks>
        /// Elements of returned list may not be distinct (not sure about that).
        /// </remarks>
        internal static List<ConsoleKey> GetPressedKeys()
        {
            var retVal = new List<ConsoleKey>();

            while(Console.KeyAvailable)
            {
                retVal.Add(Console.ReadKey(true).Key);
            }
            return retVal;
        }

        internal static void Start(IGameLoop o)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.CursorVisible = false;

            // *********************************************************
            // *** Initialize (e.g. static frame buffer background): ***
            // *********************************************************

            o.Init(_w, _h, _frameBuf);

            // ****************************
            // *** Start infinite loop: ***
            // ****************************

            do
            {
                long beginTicks, elapsedTicks, leftTicks;

                beginTicks = DateTime.Now.Ticks;

                // ****************************************
                // *** Handle key presses / user input: ***
                // ****************************************

                { // (limits scope)
                    var pressedKeys = GetPressedKeys();

                    if(pressedKeys.Contains(ConsoleKey.Escape))
                    {
                        break; // Exits game loop.
                    }

                    o.HandleUserInput(pressedKeys);
                }

                // **********************
                // *** Update output: ***
                // **********************

                o.Update(_steps, _w, _h, _frameBuf);
    
                // ******************************************
                // *** Copy from frame buffer to console: ***
                // ******************************************

#if DEBUG
                var debBlitStart = DateTime.Now.Ticks;
#endif //DEBUG

                BlitToConsole();

#if DEBUG
                var debBlitTicks = DateTime.Now.Ticks - debBlitStart;
#endif //DEBUG

                // **********************************
                // *** Wait until next iteration: ***
                // **********************************

                elapsedTicks = DateTime.Now.Ticks - beginTicks;
                Debug.Assert(_ticks >= elapsedTicks);
                leftTicks = _ticks - elapsedTicks;
                Thread.Sleep(new TimeSpan(leftTicks)); // 1 tick = 100 ns.

                // (assuming that this debug output takes 0 ticks..)
                //
#if DEBUG
                Console.Write($"{leftTicks} ({debBlitTicks})");
#endif //DEBUG
            }while(true);
        }
    }
}
