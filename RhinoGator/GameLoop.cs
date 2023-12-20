
// Marcel Timm, RhinoDevel, 2023dec20

namespace RhinoGator
{
    internal static class GameLoop
    {
        private const byte _consoleWidth = 80;
        private const byte _consoleHeight = 25;

        private const int _freq = 60; // Hz
        private const int _ms = 
            (int)(1000.0 * 1.0 / (double)_freq + 0.5); // Rounds

        private static void BlitToConsole(int left, int top)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.SetCursorPosition(left, top);
            Console.CursorVisible = false;

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
            int top = -1;

            // BAD: Just expecting the delay to be exact (no expl. game time)!
            //
            while (true)
            {
                // TODO: Call input handling routine!
                
                // TODO: Call (graphical) output updating/creating routine!

                if(top == -1) // To keep (debug) output above graphics.
                {
                    top = Console.CursorTop;
                }
                BlitToConsole(0, top);

                Thread.Sleep(_ms);
            }
        }
    }
}
