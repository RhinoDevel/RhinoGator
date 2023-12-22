
// RhinoDevel, MT, 2023dec22

using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 81.
    /// </remarks>
    internal class TripleXor : Base
    {
        private Xor _xorAB = new Xor();
        private Xor _xorABC = new Xor();
        internal State Output
        { 
            get
            {
                return _xorABC.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal override OutputDep GetDependencies()
        {
            return _xorAB.GetDependencies()
                | _xorABC.GetDependencies();
        }

        internal void Update(State inputA, State inputB, State inputC)
        {
            _xorAB.Update(inputA, inputB);
            _xorABC.Update(_xorAB.Output, inputC);
        }
    }
}
