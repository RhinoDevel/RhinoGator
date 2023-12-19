
// RhinoDevel, MT, 2023dec19

using System.Diagnostics;

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// An inverting buffer.
    /// </summary>
    internal class Not
    {
        internal State Output { get; private set; } = State.Unknown;

        public void Update(State input)
        {
            bool isHigh = input == State.Low || input == State.Rising;

            switch(Output)
            {
                case State.Unknown:
                {
                    if(isHigh)
                    {
                        Output = State.Rising;
                        return;
                    }
                    Output = State.Low;
                    return;
                }

                case State.Low:
                {
                    if(isHigh)
                    {
                        Output = State.Rising;
                        return;
                    }
                    return; // (keep low output)
                }

                case State.Rising:
                {
                    if(isHigh)
                    {
                        Output = State.High;
                        return;
                    }
                    Output = State.Falling;
                    return;
                }

                case State.High:
                {
                    if(isHigh)
                    {
                        return; // (keep high output)
                    }
                    Output = State.Falling;
                    return;
                }

                case State.Falling:
                {
                    if(isHigh)
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