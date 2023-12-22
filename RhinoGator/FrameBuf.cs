
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
                case State.Rising:
                {
                    return '/';
                }
                case State.High:
                {
                    return '-';
                }
                case State.Falling:
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

        private static void ScrollRowToLeft(int row, int w, byte[] frameBuf)
        {
            int rowOffset = row * w;

            for(int col = 0;col < w - 1; ++col)
            {
                frameBuf[rowOffset + col] = frameBuf[rowOffset + col + 1];
            }
        }

        internal static void PushStateToRow(
            State state, int row, int w, byte[] frameBuf)
        {
            // Scroll to the left and add state:
            
            ScrollRowToLeft(row, w, frameBuf);
            frameBuf[row * w + w - 1] = (byte)GetStateChar(state);
        }
    }
}
