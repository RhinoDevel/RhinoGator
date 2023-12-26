
// RhinoDevel, MT, 2023dec12

namespace RhinoGator.Ele.Basic
{
    internal class And : Base
    {
        private static readonly int? _maxInputs = null;
        private const OutputDep _dependencies = OutputDep.CurInputs;
        
        internal And() : base(_maxInputs, _dependencies)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return inputs.TrueForAll(
                s => s == State.HighRising || s == State.High);
        }
    }
}
