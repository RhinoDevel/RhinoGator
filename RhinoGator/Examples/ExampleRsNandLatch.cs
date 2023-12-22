
// Marcel Timm, RhinoDevel, 2023dec22

using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;

namespace RhinoGator.Examples
{
    internal class ExampleRsNandLatch : IGameLoop
    {
        private readonly ToggleSwitch _tsR = new ToggleSwitch(true, true);
        private readonly ToggleSwitch _tsS = new ToggleSwitch(true, true);
        private readonly RsNandLatch _latch = new RsNandLatch();

        void IGameLoop.Init(int w, int h, byte[] frameBuf)
        {
            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = (byte)' ';
            }
        }

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            if(pressedKeys.Contains(ConsoleKey.R))
            {
                _tsR.Toggle();
            }
            if(pressedKeys.Contains(ConsoleKey.S))
            {
                _tsS.Toggle();
            }
        }

        void IGameLoop.Update(int steps, int w, int h, byte[] frameBuf)
        {
            _tsR.Update(new List<State>{ State.Low });
            _tsS.Update(new List<State>{ State.Low });
            _latch.Update(_tsR.Output, _tsS.Output);
            
            frameBuf[0] = 
                _tsR.Output == State.High || _tsR.Output == State.Falling
                    ? (byte)'1' : (byte)'0';
            frameBuf[2] =
                _tsS.Output == State.High || _tsS.Output == State.Falling
                    ? (byte)'1' : (byte)'0';
            frameBuf[4] = 
                _latch.Output == State.High || _latch.Output == State.Falling
                    ? (byte)'1' : (byte)'0';
        }
    }
}
