
// RhinoDevel, MT, 2023dec12

namespace Ele
{
    internal class ClockParams
    {
        /// <summary>
        /// Pulse length in time steps.
        /// </summary>
        /// <remarks>
        /// The pulse length is the sum of <see cref="ClockOutput.High"/> and
        /// <see cref="ClockOutput.Falling"/>, which equals the sum of
        /// <see cref="ClockOutput.Low"/> and <see cref="ClockOutput.Rising"/>,
        /// where <see cref="ClockOutput.Falling"/> and
        /// <see cref="ClockOutput.Rising"/> always take a single time step.
        /// </remarks>
        internal uint PulseSteps;

        /// <summary>
        /// If clock shall start with <see cref="ClockOutput.High"/> or with
        /// <see cref="ClockOutput.Low"/>.
        /// </summary>
        internal bool StartHigh;
    }
}
