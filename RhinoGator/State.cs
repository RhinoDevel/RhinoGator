
// RhinoDevel, MT, 2023dec12

namespace RhinoGator
{
    /// <summary>
    /// The possible states of an element's input or output.
    /// </summary>
    internal enum State
    {
        Low = 0,
        High,

        /// <remarks>
        /// To be interpreted as low in some cases (see position in 
        /// <see cref="Clock"/> cycle output).
        /// </remarks>
        LowFalling,

        /// <remarks>
        /// To be interpreted as high in some cases (see position in 
        /// <see cref="Clock"/> cycle output).
        /// </remarks>
        HighRising,

        Unknown,
        NotConnected
    }
}
