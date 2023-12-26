
// Marcel Timm, RhinoDevel, 2023dec22

using System.Diagnostics;
using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;

namespace RhinoGator.Examples
{
    internal class ExampleBinaryAdder : IGameLoop
    {
        private readonly List<ToggleSwitch> _tsAs;
        private readonly List<ToggleSwitch> _tsBs;

        private readonly BinaryAdder _adder;

        internal ExampleBinaryAdder()
        {
            _tsAs = new List<ToggleSwitch>();
            _tsBs = new List<ToggleSwitch>();

            for(int i = 0; i < 4; ++i)
            {
                _tsAs.Add(new ToggleSwitch(true, true));
                _tsBs.Add(new ToggleSwitch(true, true));
            }

            _adder = new BinaryAdder(_tsAs.Count);
        }

        void IGameLoop.Init(int w, int h, byte[] frameBuf)
        {
            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = (byte)' ';
            }

            frameBuf[0] = (byte)'Q';
            frameBuf[1] = (byte)'W';
            frameBuf[2] = (byte)'E';
            frameBuf[3] = (byte)'R';

            frameBuf[7] = (byte)'A';
            frameBuf[8] = (byte)'S';
            frameBuf[9] = (byte)'D';
            frameBuf[10] = (byte)'F';

            frameBuf[w + 5] = (byte)'+';
            frameBuf[w + 12] = (byte)'=';
        }

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            if(pressedKeys.Contains(ConsoleKey.R))
            {
                _tsAs[0].Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.E))
            {
                _tsAs[1].Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.W))
            {
                _tsAs[2].Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.Q))
            {
                _tsAs[3].Toggle();
            }

            if(pressedKeys.Contains(ConsoleKey.F))
            {
                _tsBs[0].Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.D))
            {
                _tsBs[1].Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.S))
            {
                _tsBs[2].Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.A))
            {
                _tsBs[3].Toggle();
            }
        }

        void IGameLoop.Update(int steps, int w, int h, byte[] frameBuf)
        {
            Debug.Assert(_tsAs.Count == _tsBs.Count);

            var l = new List<Tuple<State, State>>();
            List<State> sumOutputs;

            _tsAs.ForEach(ts => ts.Update(new List<State>{ State.Low }));
            _tsBs.ForEach(ts => ts.Update(new List<State>{ State.Low }));

            for(int i = 0;i < _tsAs.Count; ++i)
            {
                var t = new Tuple<State, State>(
                            _tsAs[i].Output, _tsBs[i].Output);

                l.Add(t);

                frameBuf[w + 0 + 3 - i] =
                    t.Item1 == State.High || t.Item1 == State.HighRising
                        ? (byte)'1' : (byte)'0';

                frameBuf[w + 7 + 3 - i] =
                    t.Item2 == State.High || t.Item2 == State.HighRising
                        ? (byte)'1' : (byte)'0';
            }

            _adder.Update(l);

            sumOutputs = _adder.SumOutputs;

            Debug.Assert(sumOutputs.Count == _tsAs.Count);

            frameBuf[w + 14] = _adder.CarryOutput == State.High
                        || _adder.CarryOutput == State.HighRising
                            ? (byte)'1' : (byte)'0';

            for(int i = 0;i < sumOutputs.Count; ++i)
            {
                frameBuf[w + 15 + 3 - i] =
                    sumOutputs[i] == State.High
                        || sumOutputs[i] == State.HighRising
                            ? (byte)'1' : (byte)'0';
            }
        }
    }
}
