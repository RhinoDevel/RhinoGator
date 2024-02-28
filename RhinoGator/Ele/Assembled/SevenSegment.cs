
// RhinoDevel, MT, 2024feb28

using RhinoGator.Ele.Basic;

namespace RhinoGator.Ele.Assembled
{
    /// <summary>
    /// Seven-sement display.
    /// </summary>
    /// <remarks>
    /// Kind of a "dummy" element, having no real output.
    /// </remarks>
    internal class SevenSegment : Base
    {
        private Not _notA = new Not();
        private Not _notB = new Not();
        private Not _notC = new Not();
        private Not _notD = new Not();

        // A: ANDs:
        //
        private And _andNotBnotD = new And();
        private And _andNotAc = new And();
        private And _andBc = new And();
        private And _andAnotD = new And();
        private And _andNotAbD = new And();
        private And _andAnotBnotC = new And();

        // A: OR:
        //
        private Or _orA = new Or();

        internal override OutputDep GetDependencies()
        {
            throw new NotImplementedException();
        }

        internal void Update(
            State inputA, State inputB, State inputC, State inputD)
        {
            _notA.Update(new List<State> { inputA });
            _notB.Update(new List<State> { inputB });
            _notC.Update(new List<State> { inputC });
            _notD.Update(new List<State> { inputD });

            // A: ANDs:
            //
            _andNotBnotD.Update(new List<State> { _notB.Output, _notD.Output });
            _andNotAc.Update(new List<State> { _notA.Output, inputC });
            _andBc.Update(new List<State> { inputB, inputC });
            _andAnotD.Update(new List<State> { inputA, _notD.Output });
            _andNotAbD.Update(new List<State> { _notA.Output, inputB, inputD });
            _andAnotBnotC.Update(
                new List<State> { inputA, _notB.Output, _notC.Output });
            
            // A: OR:
            //
            _orA.Update(
                new List<State>
                {
                    _andNotBnotD.Output,
                    _andNotAc.Output,
                    _andBc.Output,
                    _andAnotD.Output,
                    _andNotAbD.Output,
                    _andAnotBnotC.Output
                });

            // TODO: Implement!
        }
    }
}
