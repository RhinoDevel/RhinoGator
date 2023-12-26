
// RhinoDevel, MT, 2023dec22

namespace RhinoGator
{
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
    }
}
