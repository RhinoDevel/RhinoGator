
// RhinoDevel, MT, 2023dec19

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// This is logically equivalent to a bubbled OR gate (De Morgan's second
    /// theorem).
    /// 
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

        internal void Update(List<State> inputs)
        {
            _and.Update(inputs);
            _not.Update(new List<State>{ _and.Output });
        }
    }
}
