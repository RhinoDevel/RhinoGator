
// RhinoDevel, MT, 2023dec12

namespace RhinoGator
{
    /// <summary>
    /// The possible states of an element's input or output.
    /// </summary>
    internal enum State
    {
        Unknown = -1,
        Low,
        High,

        /// <remarks>
        /// To be interpreted as high in some cases (see position in 
        /// <see cref="Clock"/> cycle output).
        /// </remarks>
        Falling,

        /// <remarks>
        /// To be interpreted as low in some cases (see position in 
        /// <see cref="Clock"/> cycle output).
        /// </remarks>
        Rising,

        NotConnected
    }
}
