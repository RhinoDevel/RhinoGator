
// RhinoDevel, MT, 2023dec13

namespace RhinoGator.Ele.Basic
{
    internal class Or : Base
    {
        public void Update(List<State> inputs)
        {
            bool isHigh = inputs.Any(
                s => s == State.High || s == State.Falling);

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
