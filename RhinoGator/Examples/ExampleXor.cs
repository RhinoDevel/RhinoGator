
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

        bool IGameLoop.HandleUserInput()
        {
            bool aIsPressed = false,
                bIsPressed = false;

            _aGotReleased = false;
            _bGotReleased = false;

            while(Console.KeyAvailable)
            {
                switch(Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                    {
                        return true;
                    }

                    case ConsoleKey.A:
                    {
                        aIsPressed = true;
                        break;
                    }
                    case ConsoleKey.B:
                    {
                        bIsPressed = true;
                        break;
                    }

                    default:
                    {
                        break;
                    }
                }
            }

            if(aIsPressed)
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

            if(bIsPressed)
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
