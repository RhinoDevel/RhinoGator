
// RhinoDevel, MT, 2023dec25

using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 99.
    /// 
    /// Not direct-coupled.
    /// </remarks>
    internal class EdgeTrigJKFlipFlop : Base
    {
        private HighPass _hp = new HighPass(1);
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
                | _nandR.GetDependencies()
                | _nandS.GetDependencies()
                | _latch.GetDependencies();
        }

        internal void Update(State j, State k, State clk)
        {
            _hp.Update(new List<State>{ clk });

            _nandR.Update(
                new List<State> { j, _hp.Output, _latch.SecondOutput });

            _nandS.Update(new List<State> { k, _hp.Output, _latch.Output });

            _latch.Update(_nandR.Output, _nandS.Output);
        }
    }
}
