
// RhinoDevel, MT, 2024feb28

using RhinoGator.Ele.Basic;
using RhinoGator.Ele.Basic.Led;

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
        private const LedColor _ledCol = LedColor.Red;

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

        // B: ANDs:
        //
        private readonly And _andNotAnotB = new();
        //_andNotBnotD
        private readonly And _andNotAnotCnotD = new();
        private readonly And _andNotAcD = new();
        private readonly And _andAnotCd = new();

        // C: ANDs:
        //
        private readonly And _andNotAnotC = new();
        private readonly And _andNotAd = new();
        private readonly And _andNotCd = new();
        private readonly And _andNotAb = new();
        private readonly And _andAnotB = new();

        // D: ANDs:
        //
        private readonly And _andAnotC = new();
        private readonly And _andNotAnotBnotD = new();
        private readonly And _andNotBcD = new();
        private readonly And _andBnotCd = new();
        private readonly And _andBcNotD = new();

        // E: ANDs:
        //
        //_andNotBnotD
        private readonly And _andCnotD = new();
        private readonly And _andAc = new();
        private readonly And _andAb = new();

        // F: ANDs:
        //
        private readonly And _andNotCnotD = new();
        private readonly And _andBnotD = new();
        //_andAnotB
        //_andAc
        private readonly And _andNotAbNotC = new();

        // G: ANDs:
        //
        private readonly And _andNotBc = new();
        //_andCnotD
        //_andAnotB
        private readonly And _andAd = new();
        //_andNotAbNotC

        private Or _orA = new Or(); // A OR.
        private readonly Or _orB = new(); // B OR.
        private readonly Or _orC = new(); // C OR.
        private readonly Or _orD = new(); // D OR.
        private readonly Or _orE = new(); // E OR.
        private readonly Or _orF = new(); // F OR.
        private readonly Or _orG = new(); // G OR.

        public readonly Led LedA = new(true, _ledCol);
        public readonly Led LedB = new(true, _ledCol);
        public readonly Led LedC = new(true, _ledCol);
        public readonly Led LedD = new(true, _ledCol);
        public readonly Led LedE = new(true, _ledCol);
        public readonly Led LedF = new(true, _ledCol);
        public readonly Led LedG = new(true, _ledCol);

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
            
            // B: ANDs:
            //
            _andNotAnotB.Update([_notA.Output, _notB.Output]);
            //_andNotBnotD
            _andNotAnotCnotD.Update([_notA.Output, _notC.Output, _notD.Output]);
            _andNotAcD.Update([_notA.Output, inputC, inputD]);
            _andAnotCd.Update([inputA, _notC.Output, inputD]);

            // C: ANDs:
            //
            _andNotAnotC.Update([_notA.Output, _notC.Output]);
            _andNotAd.Update([_notA.Output, inputD]);
            _andNotCd.Update([_notC.Output, inputD]);
            _andNotAb.Update([_notA.Output, inputB]);
            _andAnotB.Update([inputA, _notB.Output]);

            // D: ANDs:
            //
            _andAnotC.Update([inputA, _notC.Output]);
            _andNotAnotBnotD.Update([
                _notA.Output, _notB.Output, _notD.Output]);
            _andNotBcD.Update([_notB.Output, inputC, inputD]);
            _andBnotCd.Update([inputB, _notC.Output, inputD]);
            _andBcNotD.Update([inputB, inputC, _notD.Output]);

            // E: ANDs:
            //
            //_andNotBnotD
            _andCnotD.Update([inputC, _notD.Output]);
            _andAc.Update([inputA, inputC]);
            _andAb.Update([inputA, inputB]);

            // F: ANDs:
            //
            _andNotCnotD.Update([_notC.Output, _notD.Output]);
            _andBnotD.Update([inputB, _notD.Output]);
            //_andAnotB
            //_andAc
            _andNotAbNotC.Update([_notA.Output, inputB, _notC.Output]);

            // G: ANDs:
            //
            _andNotBc.Update([_notB.Output, inputC]);
            //_andCnotD
            //_andAnotB
            _andAd.Update([inputA, inputD]);
            //_andNotAbNotC

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

            // B: OR:
            //
            _orB.Update(
                [
                    _andNotAnotB.Output,
                    _andNotBnotD.Output,
                    _andNotAnotCnotD.Output,
                    _andNotAcD.Output,
                    _andAnotCd.Output
                ]);

            // C: OR:
            //
            _orC.Update(
                [
                    _andNotAnotC.Output,
                    _andNotAd.Output,
                    _andNotCd.Output,
                    _andNotAb.Output,
                    _andAnotB.Output
                ]);

            // D: OR:
            //
            _orD.Update(
                [
                    _andAnotC.Output,
                    _andNotAnotBnotD.Output,
                    _andNotBcD.Output,
                    _andBnotCd.Output,
                    _andBcNotD.Output
                ]);

            // E: OR:
            //
            _orE.Update(
                [
                    _andNotBnotD.Output,
                    _andCnotD.Output,
                    _andAc.Output,
                    _andAb.Output
                ]);

            // F: OR:
            //
            _orF.Update(
                [
                    _andNotCnotD.Output,
                    _andBnotD.Output,
                    _andAnotB.Output,
                    _andAc.Output,
                    _andNotAbNotC.Output
                ]);

            _orG.Update(
                [
                    _andNotBc.Output,
                    _andCnotD.Output,
                    _andAnotB.Output,
                    _andAd.Output,
                    _andNotAbNotC.Output
                ]);

            LedA.Update([_orA.Output]);
            LedB.Update([_orB.Output]);
            LedC.Update([_orC.Output]);
            LedD.Update([_orD.Output]);
            LedE.Update([_orE.Output]);
            LedF.Update([_orF.Output]);
            LedG.Update([_orG.Output]);
        }
    }
}
