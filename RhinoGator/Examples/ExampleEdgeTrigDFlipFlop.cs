
// Marcel Timm, RhinoDevel, 2023dec25

using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;
using RhinoGator.Ele.Basic.Clock;

namespace RhinoGator.Examples
{
    internal class ExampleEdgeTripDFlipFlop : IGameLoop
    {
        private readonly ToggleSwitch _ts = new ToggleSwitch(true, true);
        private readonly Clock _clock = new Clock(
            new ClockParams
            {
                PulseSteps = 16,
                StartHigh = false
            });
        private readonly EdgeTrigDFlipFlop _flipFlop = new EdgeTrigDFlipFlop();

        void IGameLoop.Init(int w, int h, byte[] frameBuf)
        {
            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = (byte)' ';
            }
        }

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            if(pressedKeys.Contains(ConsoleKey.D))
            {
                _ts.Toggle();
            }
        }

        void IGameLoop.Update(int steps, int w, int h, byte[] frameBuf)
        {
            for(int i = 0;i < steps; ++i)
            {
                _ts.Update(new List<State>{ State.Low });
                _clock.Update(new List<State>());
                _flipFlop.Update(_ts.Output, _clock.Output);

                FrameBuf.PushStateToRow(_ts.Output, 0, w, frameBuf);
                FrameBuf.PushStateToRow(_clock.Output, 2, w, frameBuf);
                FrameBuf.PushStateToRow(_flipFlop.Output, 4, w, frameBuf);
                FrameBuf.PushStateToRow(_flipFlop.SecondOutput, 6, w, frameBuf);
            }
        }
    }
}
