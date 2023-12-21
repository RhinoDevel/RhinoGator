
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

        private bool _aWasPressed = false;
        private bool _bWasPressed = false;

        void IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            bool _aGotReleased = false,
                _bGotReleased = false;

            if(pressedKeys.Contains(ConsoleKey.A))
            {
                _aWasPressed = true;
            }
            else
            {
                if(_aWasPressed)
                {
                    _aWasPressed = false;
                    _aGotReleased = true;
                }
            }

            if(pressedKeys.Contains(ConsoleKey.B))
            {
                _bWasPressed = true;
            }
            else
            {
                if(_bWasPressed)
                {
                    _bWasPressed = false;
                    _bGotReleased = true;
                }
            }

            if(_aGotReleased)
            {
                _tsA.Toggle();
            }
            if(_bGotReleased)
            {
                _tsB.Toggle();
            }
        }

        void IGameLoop.Update(int w, int h, char[] frameBuf)
        {
            _tsA.Update(new List<State>{ State.Low });
            _tsB.Update(new List<State>{ State.Low });

            _xor.Update(_tsA.Output, _tsB.Output);

            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = ' ';
            }
            
            frameBuf[0] = 
                _tsA.Output == State.High || _tsA.Output == State.Falling
                    ? '1' : '0';
            frameBuf[2] =
                _tsB.Output == State.High || _tsB.Output == State.Falling
                    ? '1' : '0';
            frameBuf[4] = 
                _xor.Output == State.High || _xor.Output == State.Falling
                    ? '1' : '0';
        }
    }
}
