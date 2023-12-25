
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

        private readonly uint _pulseSteps;

        private uint _curPulseStep;

        internal HighPass(uint pulseSteps) : base(_maxInputs, _dependencies)
        {
            _pulseSteps = pulseSteps;

            _curPulseStep = 0;
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            switch(inputs[0])
            {
                case State.Rising:
                {
                    _curPulseStep = 0;
                    return true;
                }
                case State.High:
                {
                    if(_curPulseStep == _pulseSteps - 1)
                    {
                        return false;
                    }
                    ++_curPulseStep;
                    return true;
                }
                case State.Falling:
                {
                    _curPulseStep = 0;
                    return false;
                }
                case State.Low:
                {
                    _curPulseStep = 0;
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
