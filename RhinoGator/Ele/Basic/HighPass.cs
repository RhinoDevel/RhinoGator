
// RhinoDevel, MT, 2023dec12

using System.Diagnostics;

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// High-pass filter.
    /// </summary>
    /// <remarks>
    /// Output rises (under some circumstances), if input is high.
    /// Output gets high, if input is still high and output has risen.
    /// Output gets low after being high.
    /// </remarks>
    internal class HighPass : Base
    {
        internal void Update(State input)
        {
            switch(Output)
            {
                case State.Unknown:
                {
                    // On "init", rise, if input is high,
                    // otherwise set output to defined low state:

                    if(input == State.High)
                    {
                        Output = State.Rising;
                        return;
                    }
                    Output = State.Low;
                    return;
                }

                case State.Low:
                {
                    // Rise, if input is high, stay low otherwise:

                    if(input == State.High)
                    {
                        Output = State.Rising;
                        return;
                    }
                    return; // (keep low output)
                }

                case State.Rising:
                {
                    // If input is still high, set output to high,
                    // otherwise let output fall:

                    if(input == State.High)
                    {
                        Output = State.High;
                        return;
                    }
                    Output = State.Falling;
                    return;
                }

                case State.High:
                {
                    // The high state is short, because this is a high-pass
                    // filter:

                    Output = State.Falling;
                    return;
                }

                case State.Falling:
                {
                    // Rise (again), if input got (suddenly..) high,
                    // otherwise reach low state:

                    if(input == State.High)
                    {
                        Debug.Assert(false); // Should not happen (no rise?)..
                        Output = State.Rising;
                        return;
                    }
                    Output = State.Low;
                    return;
                }

                default:
                {
                    throw new Exception(
                        $"Unsupported (last) output state {(int)Output}!");
                }
            }
        }
    }
}
