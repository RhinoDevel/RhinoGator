
// Marcel Timm, RhinoDevel, 2023dec25

using RhinoGator.Ele.Assembled;
using RhinoGator.Ele.Basic;

namespace RhinoGator.Examples
{
    internal class ExampleDLatch : IGameLoop
    {
        private readonly ToggleSwitch _ts = new ToggleSwitch(true, true);
        private readonly DLatch _latch = new DLatch();

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
            _ts.Update(new List<State>{ State.Low });
            _latch.Update(_ts.Output);
            
            for(int i = 0;i < steps; ++i)
            {
                FrameBuf.PushStateToRow(_ts.Output, 0, w, frameBuf);
                FrameBuf.PushStateToRow(_latch.Output, 2, w, frameBuf);
                FrameBuf.PushStateToRow(_latch.SecondOutput, 4, w, frameBuf);
            }
        }
    }
}
