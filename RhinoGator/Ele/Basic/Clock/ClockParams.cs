
// RhinoDevel, MT, 2023dec12

namespace RhinoGator.Ele.Basic.Clock
{
    internal class ClockParams
    {
        /// <summary>
        /// Pulse length in time steps.
        /// </summary>
        /// <remarks>
        /// The pulse length is the sum of <see cref="State.High"/> and
        /// <see cref="State.LowFalling"/>, which equals the sum of
        /// <see cref="State.Low"/> and <see cref="State.HighRising"/>,
        /// where <see cref="State.LowFalling"/> and
        /// <see cref="State.HighRising"/> always take a single "time" step.
        /// </remarks>
        internal uint PulseSteps;

        /// <summary>
        /// If clock shall start with <see cref="State.High"/> or with
        /// <see cref="State.Low"/>.
        /// </summary>
        internal bool StartHigh;
    }
}
