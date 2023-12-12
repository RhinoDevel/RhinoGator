
// RhinoDevel, MT, 2023dec12

namespace Ele
{
    /// <summary>
    /// Clock generator.
    /// </summary>
    /// <remarks>
    /// Always generates a symmetric square wave.
    /// 
    /// Always starts with <see cref="ClockOutput.Low"/> or
    /// <see cref="ClockOutput.High"/>.
    /// </remarks>
    internal class Clock
    {
        private readonly ClockParams _p;

        private readonly uint _cycleSteps;
        private readonly uint _cyclePosRising;
        private readonly uint _cyclePosFalling;
        private readonly ClockOutput _firstOutput;
        private readonly ClockOutput _secondOutput;

        internal ClockOutput Output { get; private set; }

        internal Clock(ClockParams p)
        {
            _p = p;

            _cycleSteps = _p.PulseSteps << 1;
            _cyclePosRising =
                _p.StartHigh ? (_cycleSteps - 1) : (_p.PulseSteps - 1);
            _cyclePosFalling =
                _p.StartHigh ? (_p.PulseSteps - 1) : (_cycleSteps - 1);
            _firstOutput = _p.StartHigh ? ClockOutput.High : ClockOutput.Low;
            _secondOutput = _p.StartHigh ? ClockOutput.Low : ClockOutput.High;

            // Not necessary (unknown time), but for clarification:
            //
            Output = _p.StartHigh ? ClockOutput.High : ClockOutput.Low;
        }

        internal void Forward(uint timeSteps)
        {
            uint cyclePos = timeSteps % _cycleSteps;

            if(cyclePos == _cyclePosRising)
            {
                Output = ClockOutput.Rising;
                return;
            }
            if(cyclePos == _cyclePosFalling)
            {
                Output = ClockOutput.Falling;
                return;
            }
            if(cyclePos < _p.PulseSteps)
            {
                Output = _firstOutput;
                return;
            }
            Output = _secondOutput;
        }
    }
}
