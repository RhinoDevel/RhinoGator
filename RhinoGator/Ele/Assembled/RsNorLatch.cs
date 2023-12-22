
// RhinoDevel, MT, 2023dec22

using System.Diagnostics;

namespace RhinoGator.Ele.Assembled
{
    /// <remarks>
    /// Page 91.
    /// </remarks>
    internal class RsNorLatch : Base
    {
        private Nor _norR = new Nor();
        private Nor _norS = new Nor();
        internal State Output
        { 
            get
            {
                return _norR.Output;
            }
            private set
            {
                throw new NotSupportedException();
            }
        }

        internal override OutputDep GetDependencies()
        {
            return _norR.GetDependencies()
                | _norS.GetDependencies()
                | OutputDep.LastOutputs;
        }

        internal void Update(State inputR, State inputS)
        {
            var lastOutputR = _norR.Output;

            _norR.Update(new List<State>{ inputR, _norS.Output });
            _norS.Update(new List<State>{ inputS, lastOutputR });
        }
    }
}
