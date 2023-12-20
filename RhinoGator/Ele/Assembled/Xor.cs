
// RhinoDevel, MT, 2023dec19

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 37.
    /// </remarks>
    internal class Xor
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

        internal void Update(State inputA, State inputB)
        {
            _notA.Update(inputA);
            _andA.Update(new List<State>{ _notA.Output, inputB });

            _notB.Update(inputB);
            _andB.Update(new List<State>{ _notB.Output, inputA });

            _or.Update(new List<State>{ _andA.Output, _andB.Output });
        }
    }
}
