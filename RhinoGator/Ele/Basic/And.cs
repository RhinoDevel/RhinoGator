
// RhinoDevel, MT, 2023dec12

namespace RhinoGator.Ele.Basic
{
    internal class And : Base
    {
        internal And() : base(null)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs.TrueForAll(
                s => s == State.High || s == State.Falling);
        }
    }
}
