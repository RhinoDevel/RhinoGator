
// RhinoDevel, MT, 2023dec13

namespace RhinoGator.Ele.Basic
{
    internal class Or : Base
    {
        private static readonly int? _maxInputs = null;
        private const OutputDep _dependencies = OutputDep.CurInputs;

        internal Or() : base(_maxInputs, _dependencies)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs.Any(s => s == State.High || s == State.Falling);
        }
    }
}
