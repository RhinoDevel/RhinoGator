
// Marcel Timm, RhinoDevel, 2023dec25

using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;
using RhinoGator.Ele.Basic.Clock;

namespace RhinoGator.Examples
{
    internal class ExampleEdgeTripJKFlipFlop : IGameLoop
    {
        private readonly ToggleSwitch _tsJ = new ToggleSwitch(true, true);
        private readonly ToggleSwitch _tsK = new ToggleSwitch(true, true);
        private readonly Clock _clock = new Clock(
            new ClockParams
            {
                PulseSteps = 16,
                StartHigh = true
            });
        private readonly EdgeTrigJKFlipFlop _flipFlop =
            new EdgeTrigJKFlipFlop();

        void IGameLoop.Init(int w, int h, byte[] frameBuf)
        {
            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = (byte)' ';
            }
        }

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            if(pressedKeys.Contains(ConsoleKey.J))
            {
                _tsJ.Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.K))
            {
                _tsK.Toggle();
            }
        }

        void IGameLoop.Update(int steps, int w, int h, byte[] frameBuf)
        {
            for(int i = 0;i < steps; ++i)
            {
                _tsJ.Update(new List<State>{ State.Low });
                _tsK.Update(new List<State>{ State.Low });
                _clock.Update(new List<State>());
                _flipFlop.Update(_tsJ.Output, _tsK.Output, _clock.Output);

                FrameBuf.PushStateToRow('C', _clock.Output, 0, w, frameBuf);
                FrameBuf.PushStateToRow('J', _tsJ.Output, 2, w, frameBuf);
                FrameBuf.PushStateToRow('K', _tsK.Output, 4, w, frameBuf);
                FrameBuf.PushStateToRow('Q', _flipFlop.Output, 6, w, frameBuf);
                //FrameBuf.PushStateToRow(
                //    'q', _flipFlop.SecondOutput, 8, w, frameBuf);
            }
        }
    }
}
