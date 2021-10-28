using JetBrains.DataFlow;
using JetBrains.Lifetimes;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Signals.Emitters
{
    public class MapInternalsSignalEmitter
    {
        public readonly ISignal<string> SomethingHappened;

        public MapInternalsSignalEmitter(Lifetime lifetime)
        {
            SomethingHappened = new Signal<string>(lifetime, "MapInternalsSignalEmitter.SomethingHappened");
        }

        public void MakeItHappen(string arg)
        {
            SomethingHappened.Fire(arg);
        }
    }
}