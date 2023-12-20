
// RhinoDevel, MT, 2023dec12

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// High-pass filter. Each time the input goes high, the output will
    /// generate a short spike.
    /// </summary>
    internal class HighPass : Base
    {
        internal HighPass() : base(1)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            return Output != State.High && inputs[0] == State.High;
        }
    }
}
