
// RhinoDevel, MT, 2023dec22

using System.Diagnostics;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 82.
    /// </remarks>
    internal class BinaryAdder : Base
    {
        private readonly HalfAdder _ha;
        private readonly List<FullAdder> _fas;

        internal State CarryOutput
        { 
            get
            {
                if(_fas.Count == 0)
                {
                    return _ha.CarryOutput;
                }
                return _fas.Last().CarryOutput;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal List<State> SumOutputs
        {
            get
            {
                var retVal = new List<State> { _ha.SumOutput };

                retVal.AddRange(_fas.Select(fa => fa.SumOutput));

                return retVal;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal BinaryAdder(int bits)
        {
            _ha = new HalfAdder();

            _fas = new List<FullAdder>();
            for(int i = 0; i < bits - 1; ++i)
            {
                _fas.Add(new FullAdder());
            }
        }

        internal override OutputDep GetDependencies()
        {
            return _ha.GetDependencies()
                | new FullAdder().GetDependencies();
        }

        internal void Update(List<Tuple<State, State>> inputABs)
        {
            Debug.Assert(inputABs.Count == (1 + _fas.Count));

            int i = 0;

            _ha.Update(inputABs[i].Item1, inputABs[i].Item2);
            
            ++i;

            while(i < inputABs.Count)
            {
                _fas[i - 1].Update(
                    inputABs[i].Item1,
                    inputABs[i].Item2,
                    i == 1 ? _ha.CarryOutput : _fas[i - 2].CarryOutput);

                ++i;
            }
        }
    }
}
