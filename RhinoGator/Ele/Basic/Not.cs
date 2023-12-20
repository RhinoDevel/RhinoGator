
// RhinoDevel, MT, 2023dec19

using System.Diagnostics;

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// An inverting buffer.
    /// </summary>
    internal class Not : Base
    {
        internal void Update(State input)
        {
            bool nextIsHigh = input == State.Low || input == State.Rising;

            Output = GetOutput(Output, nextIsHigh);
        }
    }
}
