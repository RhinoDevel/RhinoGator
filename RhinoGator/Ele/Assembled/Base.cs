
// RhinoDevel, MT, 2023dec21

namespace RhinoGator.Ele.Assembled
{
    /// <summary>
    /// Base class for assembled elements that are put together by using other
    /// ("logic") elements.
    /// </summary>
    internal abstract class Base
    {
        internal abstract OutputDep GetDependencies();
    }
}
