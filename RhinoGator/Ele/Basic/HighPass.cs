
// RhinoDevel, MT, 2023dec12

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// High-pass filter. Each time the input goes high, the output will
    /// generate a short spike.
    /// </summary>
    internal class HighPass : Base
    {
        private static readonly int? _maxInputs = 1;
        private const OutputDep _dependencies =
            OutputDep.LastOutputs | OutputDep.CurInputs;

        internal HighPass() : base(_maxInputs, _dependencies)
        {
            // Nothing to do.
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            switch(inputs[0])
            {
                case State.Rising:
                {
                    return true;
                }
                case State.High:
                {
                    return Output == State.Rising;
                }
                case State.Falling:
                {
                    return false;
                }
                case State.Low:
                {
                    return false;
                }

                case State.Unknown: // Falls through.
                case State.NotConnected: // Falls through.
                default:
                {
                    throw new NotSupportedException(
                        $"Unsupported input state {inputs[0]}!");
                }
            }
        }
    }
}
