
// RhinoDevel, MT, 2023dec12

using System.Diagnostics;

namespace RhinoGator.Ele.Basic
{
    internal class And : Base
    {
        internal void Update(List<State> inputs)
        {
            bool nextIsHigh = inputs.TrueForAll(
                s => s == State.High || s == State.Falling);

            Output = GetOutput(Output, nextIsHigh);
        }
    }
}
