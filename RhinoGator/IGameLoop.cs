
// Marcel Timm, RhinoDevel, 2023dec21

namespace RhinoGator
{
    internal interface IGameLoop
    {
        internal void HandleUserInput(List<ConsoleKey> pressedKeys);
        internal void Update(int w, int h, char[] frameBuf);
    }
}
