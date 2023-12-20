
// RhinoDevel, MT, 2023dec19

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// This is logically equivalent to a bubbled AND gate (De Morgan's first
    /// theorem).
    /// 
    /// Page 32.
    /// </remarks>
    internal class Nor
    {
        private Basic.Or _or = new Basic.Or();
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
            _or.Update(inputs);
            _not.Update(new List<State>{ _or.Output });
        }
    }
}
