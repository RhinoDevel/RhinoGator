
// RhinoDevel, MT, 2023dec22

using RhinoGator.Ele.Basic.Led;

namespace RhinoGator
{
    /// <summary>
    /// Holds helper methods to draw to the frame buffer.
    /// </summary>
    internal static class FrameBuf
    {
        private static char GetStateChar(State s)
        {
            switch(s)
            {
                case State.Low:
                {
                    return '_';
                }
                case State.HighRising:
                {
                    return '/';
                }
                case State.High:
                {
                    return '^';
                }
                case State.LowFalling:
                {
                    return '\\';
                }

                case State.NotConnected:
                {
                    return 'o';
                }
                case State.Unknown:
                {
                    return '?';
                }

                default:
                {
                    throw new NotSupportedException($"Unsupported state {s}!");
                }
            }
        }

        private static int ScrollRowToLeft(
            int colOffset,
            int row,
            int rowWidth,
            int scrollWidth,
            byte[] frameBuf)
        {
            int retVal = colOffset,
                rowOffset = row * rowWidth,
                colLimit = colOffset + scrollWidth - 1;

            // Output:       C   _ / ...  ^  \     2
            // Column index: 0 1 2 3 ... 76 77 78 79

            for(;retVal < colLimit; ++retVal)
            {
                frameBuf[rowOffset + retVal] = frameBuf[rowOffset + retVal + 1];
            }
            return retVal;
        }

        internal static void PushStateToRow(
            char title, State state, int row, int rowWidth, byte[] frameBuf)
        {
            // Scroll to the left and add state:
            
            int col = 0,
                rowOffset = row * rowWidth,
                scrollWidth;

            frameBuf[rowOffset + col] = (byte)title;

            col += 2;
            scrollWidth = rowWidth - col - 2;

            col = ScrollRowToLeft(col, row, rowWidth, scrollWidth, frameBuf);
            frameBuf[rowOffset + col] = (byte)GetStateChar(state);
            col += 2;
            frameBuf[rowOffset + col] =
                (byte)(((int)state).ToString()[0]); // Kind of hard-coded..
        }

        internal static void DrawLed(
            Led led, int row, int col, int rowWidth, byte[] frameBuf)
        {
            int offset = row * rowWidth + col;

            if(!led.IsOn)
            {
                frameBuf[offset] = (byte)'O';
                return;
            }

            switch(led.GetColor())
            {
                case LedColor.Red:
                {
                    frameBuf[offset] = (byte)'r';
                    return;
                }
                case LedColor.Green:
                {
                    frameBuf[offset] = (byte)'g';
                    return;
                }
                case LedColor.Blue:
                {
                    frameBuf[offset] = (byte)'b';
                    return;
                }

                default:
                {
                    throw new NotSupportedException(
                        $"LED color {(int)led.GetColor()} is not supported!");
                }
            }
        }

        internal static void DrawSevenSegment(
            Led a,
            Led b,
            Led c,
            Led d,
            Led e,
            Led f,
            Led g,
            int row, int col, int rowWidth, byte[] frameBuf)
        {
            DrawLed(a, row + 0, col + 1, rowWidth, frameBuf);
            DrawLed(b, row + 1, col + 2, rowWidth, frameBuf);
            DrawLed(c, row + 3, col + 2, rowWidth, frameBuf);
            DrawLed(d, row + 4, col + 1, rowWidth, frameBuf);
            DrawLed(e, row + 3, col + 0, rowWidth, frameBuf);
            DrawLed(f, row + 1, col + 0, rowWidth, frameBuf);
            DrawLed(g, row + 2, col + 1, rowWidth, frameBuf);
        }
    }
}
