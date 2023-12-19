
// RhinoDevel, MT, 2023dec19

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 34.
    /// </remarks>
    internal class Nand
    {
        private Basic.And _and = new Basic.And();
        private Basic.Not _not = new Basic.Not();

        internal State Output
        { 
            get
            {
                return _not.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        public void Update(List<State> inputs)
        {
            _and.Update(inputs);
            _not.Update(_and.Output);
        }
    }
}
