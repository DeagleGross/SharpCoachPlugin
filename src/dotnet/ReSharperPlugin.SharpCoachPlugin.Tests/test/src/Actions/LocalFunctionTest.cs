using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using ReSharperPlugin.SharpCoachPlugin.Actions;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class MapModelsActionTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\LocalFunction";

        [Test]
        public void TestLocalFunctionLaunchTest()
        {
            DoNamedTest2();
        }
    }
}