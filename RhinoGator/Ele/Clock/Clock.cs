
// RhinoDevel, MT, 2023dec12

namespace Ele
{
    /// <summary>
    /// Clock generator.
    /// </summary>
    /// <remarks>
    /// Always generates a symmetric square wave.
    /// 
    /// Always starts with <see cref="State.Low"/> or
    /// <see cref="State.High"/>.
    /// 
    /// Always rises and falls in one time step.
    /// </remarks>
    internal class Clock
    {
        private readonly ClockParams _p;

        private readonly uint _cycleSteps;
        private readonly uint _cyclePosRising;
        private readonly uint _cyclePosFalling;
        private readonly State _firstOutput;
        private readonly State _secondOutput;

        internal State Output { get; private set; }

        internal Clock(ClockParams p)
        {
            _p = p;

            _cycleSteps = _p.PulseSteps << 1;
            _cyclePosRising =
                _p.StartHigh ? (_cycleSteps - 1) : (_p.PulseSteps - 1);
            _cyclePosFalling =
                _p.StartHigh ? (_p.PulseSteps - 1) : (_cycleSteps - 1);
            _firstOutput = _p.StartHigh ? State.High : State.Low;
            _secondOutput = _p.StartHigh ? State.Low : State.High;

            Output = State.Unknown;
        }

        internal void Forward(uint timeSteps)
        {
            uint cyclePos = timeSteps % _cycleSteps;

            if(cyclePos == _cyclePosRising)
            {
                Output = State.Rising;
                return;
            }
            if(cyclePos == _cyclePosFalling)
            {
                Output = State.Falling;
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
