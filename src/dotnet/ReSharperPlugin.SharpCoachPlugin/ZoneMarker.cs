using JetBrains.Application.BuildScript.Application.Zones;

namespace ReSharperPlugin.SharpCoachPlugin
{
    [ZoneMarker]
    public class ZoneMarker : IRequire<ISharpCoachPluginZone>
    {
    }
}