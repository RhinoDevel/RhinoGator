
// RhinoDevel, MT, 2023dec22

namespace RhinoGator.Ele
{
    /// <summary>
    /// Holds helper methods to be used by element implementations.
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Return, if given state is to be interpreted as high or low.
        /// </summary>
        internal static bool IsHigh(State s)
        {
            switch(s)
            {
                case State.Unknown: // By definition! Falls through.
                case State.Low: // Falls through.
                case State.LowFalling:
                {
                    return false;
                }

                
                case State.High: // Falls through.
                case State.HighRising:
                {
                    return true;
                }

                case State.NotConnected: // Falls through. // TODO: Implement (high)!
                default:
                {
                    throw new NotSupportedException(
                        $"The state {(int)s} is not supported!");
                }
            }
        }
    }
}
