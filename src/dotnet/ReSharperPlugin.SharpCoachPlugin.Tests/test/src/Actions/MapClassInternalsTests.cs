using DefaultNamespace;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class MapClassInternalsTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\Class";

        [Test]
        public void TestMapClassToClassActionTest()
        {
            DoNamedTest2();
        }
    }
}