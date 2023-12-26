
// RhinoDevel, MT, 2023dec25

using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 99.
    /// 
    /// Not direct-coupled.
    /// 
    /// Can divide a frequency by two (J = 1 and K = 1), always with an output
    /// duty cycle of 50%.
    /// </remarks>
    internal class EdgeTrigJKFlipFlop : Base
    {
        private readonly Not? _not;
        private readonly HighPass _hp;
        private readonly Nand _nandR;
        private readonly Nand _nandS;
        private readonly RsNandLatch _latch;

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

        internal EdgeTrigJKFlipFlop(bool negEdgeTriggered)
        {
            _not = negEdgeTriggered ? new Not() : null;
            _hp = new HighPass(1);
            _nandR = new Nand();
            _nandS = new Nand();
            _latch = new RsNandLatch();
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
            if(_not == null)
            {
                _hp.Update(new List<State>{ clk });
            }
            else
            {
                _not.Update(new List<State>{ clk });
                _hp.Update(new List<State>{ _not.Output });
            }

            _nandR.Update(
                new List<State> { j, _hp.Output, _latch.SecondOutput });

            _nandS.Update(new List<State> { k, _hp.Output, _latch.Output });

            _latch.Update(_nandR.Output, _nandS.Output);
        }
    }
}
