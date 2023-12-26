
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

        private readonly HighPass _hp = new HighPass(4);

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

                FrameBuf.PushStateToRow('C', _clock.Output, 0, w, frameBuf);
                FrameBuf.PushStateToRow('H', _hp.Output, 2, w, frameBuf);
            }
        }
    }
}
