
// Marcel Timm, RhinoDevel, 2023dec22

using RhinoGator.Ele.Basic;
using RhinoGator.Ele.Basic.Clock;

namespace RhinoGator.Examples
{
    internal class ExampleClock : IGameLoop
    {
        private readonly Clock _clock = new Clock(
            new ClockParams
            {
                StartHigh = false,
                PulseSteps = 8
            });

        private readonly HighPass _hp = new HighPass();

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

        private static void PushStateToRow(
            State state, int row, int w, byte[] frameBuf)
        {
            // Scroll to the left and add state:
            
            ScrollRowToLeft(row, w, frameBuf);
            frameBuf[row * w + w - 1] = (byte)GetStateChar(state);
        }

        void IGameLoop.Init(int w, int h, byte[] frameBuf)
        {
            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = (byte)' ';
            }
        }

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            // Nothing to do.
        }

        void IGameLoop.Update(int steps, int w, int h, byte[] frameBuf)
        {
            for(int i = 0;i < steps; ++i)
            {
                _clock.Update(new List<State>());
                _hp.Update(new List<State>{ _clock.Output });

                PushStateToRow(_clock.Output, 0, w, frameBuf);
                PushStateToRow(_hp.Output, 2, w, frameBuf);
            }
        }
    }
}
