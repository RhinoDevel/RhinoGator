
// RhinoDevel, MT, 2023dec28

using System.Diagnostics;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 110.
    /// </remarks>
    internal class RippleCounter : Base
    {
        private readonly List<EdgeTrigJKFlipFlop> _jks;

        internal List<State> Outputs
        {
            get
            {
                return _jks.Select(jk => jk.Output).ToList();
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal RippleCounter(int bits)
        {
            _jks = new List<EdgeTrigJKFlipFlop>();
            for(int i = 0; i < bits; ++i)
            {
                _jks.Add(new EdgeTrigJKFlipFlop(true)); // Neg. edge triggered.
            }
        }

        internal override OutputDep GetDependencies()
        {
            return new EdgeTrigJKFlipFlop(true).GetDependencies();
        }

        internal void Update(State clock, State negClear)
        {
            var curClock = clock;

            for(int i = 0; i < _jks.Count; ++i)
            {
                _jks[i].Update(
                    State.High,
                    State.High,
                    curClock,
                    State.High,
                    negClear);

                curClock = _jks[i].Output;
            }
        }
    }
}
