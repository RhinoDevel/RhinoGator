
// RhinoDevel, MT, 2023dec20

using System.Diagnostics;

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// Base class for basic logic elements that are not put together by using
    /// other elements.
    /// </summary>
    internal abstract class Base
    {
        internal State Output { get; private set; } = State.Unknown;

        /// <remarks>
        /// null means no restriction.
        /// </remarks>
        private readonly int? _maxInputs;

        private protected Base(int? maxInputs)
        {
            Debug.Assert(maxInputs == null || 0 <= maxInputs.Value);

            _maxInputs = maxInputs;
        }

        private protected abstract bool IsNextOutputHigh(List<State> inputs);

        /// <summary>
        /// Get the output (state) that should follow based on the last output
        /// and the logically next output, only.
        /// </summary>
        /// <param name="lastOutput">
        /// The last (most recent, latest) output of this element.
        /// </param>
        /// <param name="nextIsHigh">
        /// If the next output should logically be high (otherwise it should
        /// logically be low).
        /// </param>
        /// <remarks>
        /// Rising and falling takes one (last-to-next output) change.
        /// </remarks>
        private static State GetOutput(State lastOutput, bool nextIsHigh)
        {
            switch(lastOutput)
            {
                case State.Unknown:
                {
                    if(nextIsHigh)
                    {
                        return State.Rising;
                    }
                    return State.Low;
                }

                case State.Low:
                {
                    if(nextIsHigh)
                    {
                        return State.Rising;
                    }
                    return State.Low; // (keep low output)
                }

                case State.Rising:
                {
                    if(nextIsHigh)
                    {
                        return State.High;
                    }
                    return State.Falling;
                }

                case State.High:
                {
                    if(nextIsHigh)
                    {
                        return State.High; // (keep high output)
                    }
                    return State.Falling;
                }

                case State.Falling:
                {
                    if(nextIsHigh)
                    {
                        return State.Rising;
                    }
                    return State.Low;
                }

                case State.NotConnected: // Falls through. Maybe impl. later?
                default:
                {
                    throw new Exception(
                        $"Unsupported (last) output state {(int)lastOutput}!");
                }
            }
        }

        internal void Update(List<State> inputs)
        {
            Debug.Assert(inputs != null);
            
            // Must have been checked before usage:
            //
            Debug.Assert(_maxInputs == null || inputs.Count <= _maxInputs);

            var nextIsHigh = IsNextOutputHigh(inputs);

            Output = GetOutput(Output, nextIsHigh);
        }
    }
}
