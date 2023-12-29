
// RhinoDevel, MT, 2023dec29

using System.Diagnostics;

namespace RhinoGator.Ele.Basic.Led
{
    /// <summary>
    /// A light emitting diode.
    /// </summary>
    /// <remarks>
    /// Kind of a "dummy" element, having no real output.
    /// </remarks>
    internal class Led : Base
    {
        private static readonly int? _maxInputs = 1;
        private const OutputDep _dependencies = OutputDep.CurInputs;
        
        private readonly bool _onIfHigh;
        private readonly LedColor _color;

        /// <remarks>
        /// Will be set by <see cref="IsNextOutputHigh"/>.
        /// </remarks>
        internal bool IsOn { get; private set; }

        internal LedColor GetColor()
        {
            return _color;
        }

        internal Led(bool onIfHigh, LedColor color)
            : base(_maxInputs, _dependencies, true)
        {
            _onIfHigh = onIfHigh;

            _color = color;
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            Debug.Assert(Output == State.NotConnected);

            if(_onIfHigh)
            {
                IsOn = Helper.IsHigh(inputs[0]);
            }
            else
            {
                IsOn = !Helper.IsHigh(inputs[0]);
            }

            return false;
        }
    }
}
