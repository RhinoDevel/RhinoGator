
// RhinoDevel, MT, 2023dec20

namespace RhinoGator.Ele.Basic
{
    /// <summary>
    /// A two-position toggle switch.
    /// 
    /// Forwards the signal on single input, if closed.
    /// If open, the output can be either low or high (depends on input
    /// parameter).
    /// </summary>
    internal class ToggleSwitch : Base
    {
        private readonly bool _isHighIfOpen;

        internal bool IsClosed { get; private set; }

        internal ToggleSwitch(bool highIfOpen, bool isClosed) : base(1)
        {
            _isHighIfOpen = highIfOpen;

            IsClosed = isClosed;
        }

        internal void Open()
        {
            IsClosed = false;
        }

        internal void Close()
        {
            IsClosed = true;
        }

        internal void Toggle()
        {
            IsClosed = !IsClosed;
        }

        private protected override bool IsNextOutputHigh(List<State> inputs)
        {
            if(IsClosed)
            {
                return inputs[0] == State.High || inputs[0] == State.Falling;
            }
            return _isHighIfOpen;
        }
    }
}
