
// RhinoDevel, MT, 2023dec13

namespace RhinoGator.Ele.Basic
{
    internal class Or : Base
    {
        internal Or() : base(null)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs.Any(s => s == State.High || s == State.Falling);
        }
    }
}
