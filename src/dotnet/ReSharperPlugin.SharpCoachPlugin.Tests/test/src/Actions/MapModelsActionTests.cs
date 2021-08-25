using DefaultNamespace;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class MapModelsActionTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions";

        [Test]
        public void TestMapLightModelActionTest()
        {
            DoNamedTest2();
        }
    }
}