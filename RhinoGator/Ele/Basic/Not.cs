
// RhinoDevel, MT, 2023dec19

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// An inverting buffer.
    /// </summary>
    internal class Not : Base
    {
        private static readonly int? _maxInputs = 1;
        private const OutputDep _dependencies = OutputDep.CurInputs;

        internal Not() : base(_maxInputs, _dependencies)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs[0] == State.Low || inputs[0] == State.LowFalling;
        }
    }
}
