
// Marcel Timm, RhinoDevel, 2023dec28

using System.Diagnostics;
using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;
using RhinoGator.Ele.Basic.Clock;
using RhinoGator.Ele.Basic.Led;

namespace RhinoGator.Examples
{
    internal class ExampleRippleCounter : IGameLoop
    {
        private readonly ToggleSwitch _tsC = new ToggleSwitch(true, false);
        private readonly Clock _clock = new Clock(
            new ClockParams
            {
                StartHigh = false,
                PulseSteps = 2
            });
        private readonly RippleCounter _counter = new RippleCounter(4);
        private readonly List<Led> _leds = new List<Led>();
        private readonly SevenSegment _sevSeg = new SevenSegment();

        void IGameLoop.Init(int w, int h, byte[] frameBuf)
        {
            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = (byte)' ';
            }

            _leds.Clear(); // (not necessary)
            for(int i = 0; i < 4; ++i) // (hard-coded 4..)
            {
                _leds.Add(new Led(true, LedColor.Green));
            }
        }

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            if(pressedKeys.Contains(ConsoleKey.C))
            {
                _tsC.Toggle();
            }
        }

        void IGameLoop.Update(int steps, int w, int h, byte[] frameBuf)
        {   
            _tsC.Update(new List<State>{ State.Low });

            for(int i = 0; i < steps; ++i)
            {
                List<State> counterOutputs;

                _clock.Update(new List<State>());
                _counter.Update(_clock.Output, _tsC.Output);

                FrameBuf.PushStateToRow('c', _tsC.Output, 0, w, frameBuf);
                FrameBuf.PushStateToRow('C', _clock.Output, 2, w, frameBuf);

                counterOutputs = _counter.Outputs;
                Debug.Assert(counterOutputs.Count == 4);
                FrameBuf.PushStateToRow('0', counterOutputs[0], 4, w, frameBuf);
                FrameBuf.PushStateToRow('1', counterOutputs[1], 6, w, frameBuf);
                FrameBuf.PushStateToRow('2', counterOutputs[2], 8, w, frameBuf);
                FrameBuf.PushStateToRow(
                    '3', counterOutputs[3], 10, w, frameBuf);

                Debug.Assert(counterOutputs.Count == _leds.Count);
                for(int j = 0;j < _leds.Count; ++j)
                {
                    _leds[j].Update(new List<State>{ counterOutputs[j] });
                    
                    FrameBuf.DrawLed(
                        _leds[j],
                        12,
                        _leds.Count - j - 1,
                        w,
                        frameBuf);
                }

                _sevSeg.Update(
                    counterOutputs[3],
                    counterOutputs[2],
                    counterOutputs[1],
                    counterOutputs[0]);

                FrameBuf.DrawSevenSegment(
                    _sevSeg.LedA,
                    _sevSeg.LedB,
                    _sevSeg.LedC,
                    _sevSeg.LedD,
                    _sevSeg.LedE,
                    _sevSeg.LedF,
                    _sevSeg.LedG,
                    14,
                    0,
                    w,
                    frameBuf);
            }
        }
    }
}
