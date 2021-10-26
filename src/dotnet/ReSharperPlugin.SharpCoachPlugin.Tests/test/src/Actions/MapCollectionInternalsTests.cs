using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using ReSharperPlugin.SharpCoachPlugin.Actions;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class MapCollectionInternalsTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\Collection";

        [Test]
        public void TestMapCollectionOfClassesActionTest()
        {
            DoNamedTest2();
        }
    }
}