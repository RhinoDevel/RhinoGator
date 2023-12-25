
// RhinoDevel, MT, 2023dec25

using System.Diagnostics;
using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 95.
    /// </remarks>
    internal class DLatch : Base
    {
        private Not _not = new Not();
        private RsNandLatch _latch = new RsNandLatch();

        internal State Output
        { 
            get
            {
                return _latch.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }
        internal State SecondOutput
        { 
            get
            {
                return _latch.SecondOutput;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal override OutputDep GetDependencies()
        {
            return _not.Dependencies | _latch.GetDependencies();
        }

        internal void Update(State d)
        {
            _not.Update(new List<State>{ d });

            _latch.Update(_not.Output, d);
        }
    }
}
