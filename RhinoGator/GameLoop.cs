
// Marcel Timm, RhinoDevel, 2023dec20

using System.Diagnostics;

namespace RhinoGator
{
    internal static class GameLoop
    {
        private const int _w = 80;
        private const int _h = 25;

        private const int _defaultFps = 25; // FPS / Hz.

        // "Realistic" count of (time-)steps per frame currently not in use:
        //
        //private const int _stepNs = 10; // Nanoseconds per (time-)step.

        private const int _defaultStepsPerFrame = 1; // Kind of random.

        private const ConsoleKey _keyExit = ConsoleKey.Escape;
        private const ConsoleKey _keyPause = ConsoleKey.Spacebar;

        private static readonly byte[] _frameBuf = new byte[_w * _h];

        private static void BlitToConsole()
        {
            // TODO: Add drawing in color (e.g. for LEDs)!

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
            double fps = (double)_defaultFps,
                stepsPerFrame = (double)_defaultStepsPerFrame;
            bool isPaused = false;

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
                long beginTicks = DateTime.Now.Ticks,
                    elapsedTicks, leftTicks;

                // ****************************************
                // *** Handle key presses / user input: ***
                // ****************************************

                { // (limits scope)
                    var pressedKeys = GetPressedKeys();

                    if(pressedKeys.Contains(_keyExit))
                    {
                        break; // Exits game loop.
                    }

                    if(pressedKeys.Contains(_keyPause))
                    {
                        isPaused = !isPaused;
                    }

                    if(pressedKeys.Contains(ConsoleKey.Add))
                    {
                        fps = 2.0 * fps; // Set maximum?
                    }
                    else
                    {
                        if(pressedKeys.Contains(ConsoleKey.Subtract))
                        {
                            fps = fps / 2.0;
                            if(fps < 1.0)
                            {
                                fps = 1.0; // Kind of random, but OK.
                            }
                        }
                    }

                    if(pressedKeys.Contains(ConsoleKey.Multiply))
                    {
                        stepsPerFrame = 2.0 * stepsPerFrame; // Set maximum?
                    }
                    else
                    {
                        if(pressedKeys.Contains(ConsoleKey.Divide))
                        {
                            stepsPerFrame = stepsPerFrame / 2.0;
                            if(stepsPerFrame < 1.0)
                            {
                                stepsPerFrame = 1.0;
                            }
                        }
                    }

                    o.HandleUserInput(pressedKeys);
                }

                // **********************
                // *** Update output: ***
                // **********************

                // "Realistic" count of (time-)steps per frame not in use:
                //
                //stepsPerFrame = // (Time-)steps per iteration.
                //        (int)((1000.0 * 1000.0 * msPerFrame)
                //                / _stepNs + 0.5); // Rounds

                if(!isPaused)
                {
                    o.Update(
                        (int)(stepsPerFrame + 0.5), // Rounds
                        _w,
                        _h,
                        _frameBuf);
                }
    
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

                // *************************************
                // *** Print some additional output: ***
                // *************************************

                if(isPaused)
                {
                    Console.Write("PAUSED | ");
                }
                Console.Write($"FPS: {fps} | Steps/frame: {stepsPerFrame}");

                // **********************************
                // *** Wait until next iteration: ***
                // **********************************

                { // (limits scope)
                    double msPerFrame = 1000.0 / fps;
                    long ticksPerFrame =
                        (long)(10.0 * 1000.0 * msPerFrame + 0.5); // Rounds

                    elapsedTicks = DateTime.Now.Ticks - beginTicks;
                    Debug.Assert(ticksPerFrame >= elapsedTicks);
                    leftTicks = ticksPerFrame - elapsedTicks;
                    Thread.Sleep(new TimeSpan(leftTicks)); // 1 tick = 100 ns.
                }

                // *********************
                // *** DEBUG output: ***
                // *********************

                // (assuming that this debug output takes 0 ticks..)
                //
#if DEBUG
                Console.Write(
                    $" | Left: {leftTicks} ticks | Blit: {debBlitTicks} ticks");
#endif //DEBUG
            }while(true);
        }
    }
}
