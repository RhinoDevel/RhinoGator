
// RhinoDevel, MT, 2023dec25

using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 96.
    /// </remarks>
    internal class EdgeTrigDFlipFlop : Base
    {
        private HighPass _hp = new HighPass(6);
        private Not _not = new Not();
        private Nand _nandR = new Nand();
        private Nand _nandS = new Nand();
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
            return _hp.Dependencies
                | _not.Dependencies
                | _nandR.GetDependencies()
                | _nandS.GetDependencies()
                | _latch.GetDependencies();
        }

        internal void Update(State d, State clk)
        {
            _hp.Update(new List<State>{ clk });

            _not.Update(new List<State>{ d });

            _nandR.Update(new List<State> { d, _hp.Output });

            _nandS.Update(new List<State> { _not.Output, _hp.Output });

            _latch.Update(_nandR.Output, _nandS.Output);
        }
    }
}
