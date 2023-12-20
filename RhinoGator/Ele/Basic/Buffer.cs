
// RhinoDevel, MT, 2023dec20

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// A NON-inverting buffer.
    /// </summary>
    internal class Buffer : Base
    {
        internal Buffer() : base(1)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs[0] == State.High || inputs[0] == State.Falling;
        }
    }
}
