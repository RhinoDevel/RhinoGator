
// RhinoDevel, MT, 2023dec22

using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 81.
    /// </remarks>
    internal class HalfAdder : Base
    {
        private And _and = new And();
        private Xor _xor = new Xor();
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
                return _and.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal override OutputDep GetDependencies()
        {
            return _xor.GetDependencies() | _and.Dependencies;
        }

        internal void Update(State inputA, State inputB)
        {
            _and.Update(new List<State>{ inputA, inputB });
            _xor.Update(inputA, inputB);
        }
    }
}
