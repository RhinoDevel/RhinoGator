
// RhinoDevel, MT, 2023dec13

namespace RhinoGator.Ele.Basic
{
    internal class Or : Base
    {
        internal void Update(List<State> inputs)
        {
            bool nextIsHigh = inputs.Any(
                s => s == State.High || s == State.Falling);

            Output = GetOutput(Output, nextIsHigh);
        }
    }
}
