
// Marcel Timm, RhinoDevel, 2023dec22

using System.Diagnostics;
using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;

namespace RhinoGator.Examples
{
    internal class ExampleFullAdder : IGameLoop
    {
        private readonly ToggleSwitch _tsA = new ToggleSwitch(true, true);
        private readonly ToggleSwitch _tsB = new ToggleSwitch(true, true);
        private readonly ToggleSwitch _tsC = new ToggleSwitch(true, true);
        private readonly FullAdder _adder = new FullAdder();

        void IGameLoop.Init(int w, int h, byte[] frameBuf)
        {
            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = (byte)' ';
            }
        }

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            if(pressedKeys.Contains(ConsoleKey.A))
            {
                _tsA.Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.B))
            {
                _tsB.Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.C))
            {
                _tsC.Toggle();
            }
        }

        void IGameLoop.Update(int steps, int w, int h, byte[] frameBuf)
        {
            Debug.Assert(
                (_tsA.Dependencies
                    & ~(OutputDep.User | OutputDep.CurInputs)) == 0);
            Debug.Assert(
                (_tsB.Dependencies
                    & ~(OutputDep.User | OutputDep.CurInputs)) == 0);
            Debug.Assert(
                (_tsC.Dependencies
                    & ~(OutputDep.User | OutputDep.CurInputs)) == 0);
            Debug.Assert(
                (_adder.GetDependencies()
                    & ~(OutputDep.User | OutputDep.CurInputs)) == 0);

            _tsA.Update(new List<State>{ State.Low });
            _tsB.Update(new List<State>{ State.Low });
            _tsC.Update(new List<State>{ State.Low });
            _adder.Update(_tsA.Output, _tsB.Output, _tsC.Output);
            
            frameBuf[0] = 
                Ele.Helper.IsHigh(_tsA.Output) ? (byte)'1' : (byte)'0';
            frameBuf[2] =
                Ele.Helper.IsHigh(_tsB.Output) ? (byte)'1' : (byte)'0';
            frameBuf[4] =
                Ele.Helper.IsHigh(_tsC.Output) ? (byte)'1' : (byte)'0';
            frameBuf[6] = 
                Ele.Helper.IsHigh(_adder.SumOutput) ? (byte)'1' : (byte)'0';
            frameBuf[8] = 
                Ele.Helper.IsHigh(_adder.CarryOutput) ? (byte)'1' : (byte)'0';
        }
    }
}
