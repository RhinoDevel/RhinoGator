
// RhinoDevel, MT, 2023dec22

using RhinoGator.Ele.Basic;

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
    /// 
    /// With optional preset and clear inputs (not in truth table above..).
    /// </remarks>
    internal class RsNandLatch : Base
    {
        private And _andR = new And();
        private Not _notR = new Not();
        private Or _orR = new Or();

        private And _andS = new And();
        private Not _notS = new Not();
        private Or _orS = new Or();

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
            return _andS.Dependencies
                | _notS.Dependencies
                | _orS.Dependencies

                | _andR.Dependencies
                | _notR.Dependencies
                | _orR.Dependencies

                |_nandR.GetDependencies()
                | _nandS.GetDependencies()
                | OutputDep.LastOutputs;
        }

        internal void Update(
            State inputR,
            State inputS,
            State negPreset = State.High,
            State negClear = State.High)
        {
            // **************************************************************
            // *** Prevent race condition by never setting actual R and S ***
            // *** both to zero, if preset or clear input is low:         ***
            // **************************************************************

            _andS.Update(new List<State> { inputS, negClear });
            _notS.Update(new List<State> { negPreset });
            _orS.Update(new List<State> { _andS.Output, _notS.Output });

            _andR.Update(new List<State> { inputR, negPreset });
            _notR.Update(new List<State> { negClear });
            _orR.Update(new List<State> { _andR.Output, _notR.Output });

            // *********************
            // *** Actual latch: ***
            // *********************

            _nandS.Update(new List<State>{ _orS.Output, _nandR.Output });
            _nandR.Update(new List<State>{ _orR.Output, _nandS.Output });
        }
    }
}
