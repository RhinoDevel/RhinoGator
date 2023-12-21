
// Marcel Timm, RhinoDevel, 2023dec21

using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;

namespace RhinoGator.Examples
{
    internal class ExampleXor : IGameLoop
    {
        private readonly ToggleSwitch _tsA = new ToggleSwitch(true, true);
        private readonly ToggleSwitch _tsB = new ToggleSwitch(true, true);
        private readonly Xor _xor = new Xor();

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
        }

        void IGameLoop.Update(int w, int h, byte[] frameBuf)
        {
            _tsA.Update(new List<State>{ State.Low });
            _tsB.Update(new List<State>{ State.Low });
            _xor.Update(_tsA.Output, _tsB.Output);
            
            frameBuf[0] = 
                _tsA.Output == State.High || _tsA.Output == State.Falling
                    ? (byte)'1' : (byte)'0';
            frameBuf[2] =
                _tsB.Output == State.High || _tsB.Output == State.Falling
                    ? (byte)'1' : (byte)'0';
            frameBuf[4] = 
                _xor.Output == State.High || _xor.Output == State.Falling
                    ? (byte)'1' : (byte)'0';
        }
    }
}
