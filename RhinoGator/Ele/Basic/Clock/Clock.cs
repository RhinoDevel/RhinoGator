
// RhinoDevel, MT, 2023dec12

namespace RhinoGator.Ele.Basic.Clock
{
    /// <summary>
    /// Clock generator.
    /// </summary>
    /// <remarks>
    /// Always generates a symmetric square wave (a duty cycle of 50%).
    /// 
    /// Always starts with <see cref="State.Low"/> or <see cref="State.High"/>.
    /// 
    /// Always rises and falls in one "time" step.
    /// </remarks>
    internal class Clock : Base
    {
        private static readonly int? _maxInputs = 0;
        private const OutputDep _dependencies = OutputDep.Time;

        private readonly ClockParams _p;

        private readonly uint _cycleSteps;

        private uint _cyclePos;

        internal Clock(ClockParams p) : base(_maxInputs, _dependencies)
        {
            _p = p; // Kind of bad: Reference.

            _cycleSteps = 2 * _p.PulseSteps;

            _cyclePos = 0;
        }

        /// <remarks>
        /// Also increments <see cref="_cyclePos"/> for the next call.
        /// </remarks>
        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            bool retVal;
        
            if(_cyclePos < _p.PulseSteps)
            {
                retVal = _p.StartHigh;
            }
            else
            {
                retVal = !_p.StartHigh;
            }

            Forward(1);

            return retVal;
        }

        /// <remarks>
        /// Does NOT update, just moves the cycle position!
        /// </remarks>
        internal void Forward(uint steps)
        {
            _cyclePos = _cyclePos + steps;
            _cyclePos = _cyclePos % _cycleSteps;
        }
    }
}
