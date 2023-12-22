
// RhinoDevel, MT, 2023dec22

using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 81.
    /// </remarks>
    internal class FullAdder : Base
    {
        private And _andAB = new And();
        private And _andAC = new And();
        private And _andBC = new And();
        private Or _or = new Or();
        private TripleXor _xor = new TripleXor();
        internal State SumOutput
        { 
            get
            {
                return _xor.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }
        internal State CarryOutput
        { 
            get
            {
                return _or.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal override OutputDep GetDependencies()
        {
            return _xor.GetDependencies()
                | _andAB.Dependencies
                | _andAC.Dependencies
                | _andBC.Dependencies
                | _or.Dependencies;
        }

        internal void Update(State inputA, State inputB, State inputC)
        {
            _andAB.Update(new List<State>{ inputA, inputB });
            _andAC.Update(new List<State>{ inputA, inputC });
            _andBC.Update(new List<State>{ inputB, inputC });
            _or.Update(
                new List<State>
                { 
                    _andAB.Output,
                    _andAC.Output,
                    _andBC.Output
                });
            _xor.Update(inputA, inputB, inputC);
        }
    }
}
