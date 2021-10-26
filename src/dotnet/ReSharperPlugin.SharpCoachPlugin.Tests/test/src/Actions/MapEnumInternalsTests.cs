using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using ReSharperPlugin.SharpCoachPlugin.Actions;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class MapEnumInternalsTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\Enum";

        [Test]
        public void TestMapEnumToNumericActionTest()
        {
            DoNamedTest2();
        }
        
        [Test]
        public void TestMapEnumToStringActionTest()
        {
            DoNamedTest2();
        }
    }
}