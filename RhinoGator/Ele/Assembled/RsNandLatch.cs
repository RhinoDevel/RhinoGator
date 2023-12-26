
// RhinoDevel, MT, 2023dec22

using System.Diagnostics;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 92.
    /// 
    /// R S | Q    | Comment
    /// -----------------------
    /// 0 0 | *    | Race.
    /// 0 1 | 1    | Set.
    /// 1 0 | 0    | Reset.
    /// 1 1 | N.C. | No change.
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
            _nandS.Update(new List<State>{ inputS, _nandR.Output });
            _nandR.Update(new List<State>{ inputR, _nandS.Output });
        }
    }
}
