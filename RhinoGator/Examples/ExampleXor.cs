
// Marcel Timm, RhinoDevel, 2023dec21

namespace RhinoGator.Examples
{
    internal class ExampleXor : IGameLoop
    {
        private bool _aWasPressed = false;
        private bool _bWasPressed = false;

        private bool _aGotReleased = false;
        private bool _bGotReleased = false;

        private char c = '.';

        bool IGameLoop.HandleUserInput(List<ConsoleKey> pressedKeys)
        {
            _aGotReleased = false;
            _bGotReleased = false;

            if(pressedKeys.Contains(ConsoleKey.Escape))
            {
                return true;
            }

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

            return false;
        }

        void IGameLoop.Update(int w, int h, char[] frameBuf)
        {
            if(_aGotReleased && _bGotReleased)
            {
                c = c == '!' ? '.' : '!';
            }
            else
            {
                if(_aGotReleased)
                {
                    c = c == 'A' ? '.' : 'A';
                }
                else
                {
                    if(_bGotReleased)
                    {
                        c = c == 'B' ? '.' : 'B';
                    }
                }
            }

            for(int i = 0; i < frameBuf.Length; ++i)
            {
                frameBuf[i] = c;
            }
        }
    }
}
