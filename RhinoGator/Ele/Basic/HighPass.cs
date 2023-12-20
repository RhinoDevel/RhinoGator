
// RhinoDevel, MT, 2023dec12

using System.Diagnostics;

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// High-pass filter.
    /// </summary>
    internal class HighPass : Base
    {
        internal void Update(State input)
        {
            bool nextIsHigh = Output != State.High && input == State.High;

            Output = GetOutput(Output, nextIsHigh);
        }
    }
}
