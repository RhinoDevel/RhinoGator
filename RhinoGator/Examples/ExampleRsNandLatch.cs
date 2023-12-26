
// Marcel Timm, RhinoDevel, 2023dec22

using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;

namespace RhinoGator.Examples
{
    internal class ExampleRsNandLatch : IGameLoop
    {
        private readonly ToggleSwitch _tsR = new ToggleSwitch(true, false);
        private readonly ToggleSwitch _tsS = new ToggleSwitch(true, false);
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
            
            for(int i = 0;i < steps; ++i)
            {
                FrameBuf.PushStateToRow('R', _tsR.Output, 0, w, frameBuf);
                FrameBuf.PushStateToRow('S', _tsS.Output, 2, w, frameBuf);
                FrameBuf.PushStateToRow('Q', _latch.Output, 4, w, frameBuf);
                FrameBuf.PushStateToRow(
                    'q', _latch.SecondOutput, 6, w, frameBuf);
            }
        }
    }
}
