
// Marcel Timm, RhinoDevel, 2023dec21

namespace RhinoGator
{
    internal interface IGameLoop
    {
        /// <returns>
        /// If game loop shall be exited.
        /// </returns>
        internal bool HandleUserInput();
        internal void Update(int w, int h, char[] frameBuf);
    }
}
