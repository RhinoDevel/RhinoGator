
// Marcel Timm, RhinoDevel, 2023dec20

using System.Diagnostics;

namespace RhinoGator
{
    internal static class GameLoop
    {
        private const int _w = 80;
        private const int _h = 25;

        private static readonly char[] _frameBuf = new char[_w * _h];

        private const int _freq = 25; // Hz / FPS.
        private const double _ms = 1000.0 * 1.0 / (double)_freq;
        private const long _ticks = (long)(10.0 * 1000.0 * _ms + 0.5); // Rounds

        private static void BlitToConsole()
        {
            Console.SetCursorPosition(0, 0);

            for(int row = 0; row < _h; ++row)
            {
                int rowIndex = row * _w;

                for (int col = 0; col < _w; ++col)
                {
                    Console.Write(_frameBuf[rowIndex + col]);
                }
                Console.WriteLine();
            }
        }

        internal static List<ConsoleKey> GetPressedKeys()
        {
            var pressedKeys = new List<ConsoleKey>();

            while(Console.KeyAvailable)
            {
                pressedKeys.Add(Console.ReadKey(true).Key);
            }
            return pressedKeys.Distinct().ToList();
        }

        internal static void Start(IGameLoop o)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.CursorVisible = false;

            do
            {
                var beginTicks = DateTime.Now.Ticks;

                if(o.HandleUserInput(GetPressedKeys()))
                {
                    break;
                }
                
                o.Update(_w, _h, _frameBuf);

                var endTicks = DateTime.Now.Ticks;

                var elapsedTicks = endTicks - beginTicks;

                Debug.Assert(_ticks >= elapsedTicks);

                var leftTicks = _ticks - elapsedTicks;

                Thread.Sleep(new TimeSpan(leftTicks));
                //
                // (each tick equals 100 nanoseconds)

                BlitToConsole();
            }while(true);
        }
    }
}
