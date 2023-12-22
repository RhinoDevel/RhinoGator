
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

                // Scroll to the left and add state for current step:
                //
                for(int p = 0;p < w - 1; ++p)
                {
                    frameBuf[p] = frameBuf[p + 1];
                }
                frameBuf[w - 1] = (byte)GetStateChar(_clock.Output);

                // Scroll to the left and add state for current step:
                //
                for(int p = 0;p < w - 1; ++p)
                {
                    frameBuf[2 * w + p] = frameBuf[2 * w + p + 1];
                }
                frameBuf[2 * w + w - 1] = (byte)GetStateChar(_hp.Output);
            }
        }
    }
}
