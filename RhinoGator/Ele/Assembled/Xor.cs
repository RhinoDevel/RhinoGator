
// RhinoDevel, MT, 2023dec19

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 37.
    /// </remarks>
    internal class Xor : Base
    {
        private Basic.Not _notA = new Basic.Not();
        private Basic.Not _notB = new Basic.Not();
        private Basic.And _andA = new Basic.And();
        private Basic.And _andB = new Basic.And();
        private Basic.Or _or = new Basic.Or();

        internal State Output
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
            return _notA.Dependencies
                | _notB.Dependencies
                | _andA.Dependencies
                | _andB.Dependencies
                | _or.Dependencies;
        }

        internal void Update(State inputA, State inputB)
        {
            _notA.Update(new List<State>{ inputA });
            _andA.Update(new List<State>{ _notA.Output, inputB });

            _notB.Update(new List<State>{ inputB });
            _andB.Update(new List<State>{ _notB.Output, inputA });

            _or.Update(new List<State>{ _andA.Output, _andB.Output });
        }
    }
}
