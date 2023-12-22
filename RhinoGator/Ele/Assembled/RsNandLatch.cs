
// RhinoDevel, MT, 2023dec22

using System.Diagnostics;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 92.
    /// </remarks>
    internal class RsNandLatch : Base
    {
        private Nand _nandR = new Nand();
        private Nand _nandS = new Nand();
        internal State Output
        { 
            get
            {
                return _nandR.Output;
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
                return _nandS.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal override OutputDep GetDependencies()
        {
            return _nandR.GetDependencies()
                | _nandS.GetDependencies()
                | OutputDep.LastOutputs;
        }

        internal void Update(State inputR, State inputS)
        {
            var lastOutputR = _nandR.Output;

            _nandR.Update(new List<State>{ inputR, _nandS.Output });
            _nandS.Update(new List<State>{ inputS, lastOutputR });
        }
    }
}
