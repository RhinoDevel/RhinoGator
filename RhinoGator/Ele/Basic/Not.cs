
// RhinoDevel, MT, 2023dec19

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// An inverting buffer.
    /// </summary>
    internal class Not : Base
    {
        internal Not() : base(1)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs[0] == State.Low || inputs[0] == State.Rising;
        }
    }
}
