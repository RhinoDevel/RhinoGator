
// RhinoDevel, MT, 2023dec21

namespace RhinoGator
{
    /// <summary>
    /// Indicators about what the output of an element depends on.
    /// </summary>
    [Flags]
    internal enum OutputDep : byte
    {
        /// <summary>
        /// Output (state) depends on the current input (states).
        /// </summary>
        CurInputs = 1 << 0,

        /// <summary>
        /// Output (state) depends on the user (e.g. pressing a button).
        /// </summary>
        User = 1 << 1,

        /// <summary>
        /// Output (state) depends on time (e.g. the clock element).
        /// </summary>
        Time = 1 << 2,

        /// <summary>
        /// Output (state) depens on its own last output (e.g. a high pass
        /// filter).
        /// </summary>
        LastOutput = 1 << 3
    }
}
