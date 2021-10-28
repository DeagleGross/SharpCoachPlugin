using JetBrains.Lifetimes;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Signals.Emitters;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Signals.Listeners
{
    public class MapInternalsSignalListener
    {
        public MapInternalsSignalListener(Lifetime lifetime, MapInternalsSignalEmitter signalEmitter)
        {
            signalEmitter.SomethingHappened.Advise(lifetime,
                arg => MessageBox.ShowInfo($"{arg}"));
        }
    }
}