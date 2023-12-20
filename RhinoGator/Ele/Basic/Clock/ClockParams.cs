
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
        /// <see cref="State.Falling"/>, which equals the sum of
        /// <see cref="State.Low"/> and <see cref="State.Rising"/>,
        /// where <see cref="State.Falling"/> and
        /// <see cref="State.Rising"/> always take a single "time" step.
        /// </remarks>
        internal uint PulseSteps;

        /// <summary>
        /// If clock shall start with <see cref="State.High"/> or with
        /// <see cref="State.Low"/>.
        /// </summary>
        internal bool StartHigh;
    }
}
