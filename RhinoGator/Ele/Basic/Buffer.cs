
// RhinoDevel, MT, 2023dec20

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// A NON-inverting buffer.
    /// </summary>
    internal class Buffer : Base
    {
        private static readonly int? _maxInputs = 1;
        private const OutputDep _dependencies = OutputDep.CurInputs;

        internal Buffer() : base(_maxInputs, _dependencies)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs[0] == State.High || inputs[0] == State.Falling;
        }
    }
}
