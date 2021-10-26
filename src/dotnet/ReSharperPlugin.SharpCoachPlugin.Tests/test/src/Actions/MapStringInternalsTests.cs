using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using ReSharperPlugin.SharpCoachPlugin.Actions;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class MapStringInternalsTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\String";

        [Test]
        public void TestMapStringToNumericActionTest()
        {
            DoNamedTest2();
        }
        
        [Test]
        public void TestMapStringToEnumActionTest()
        {
            DoNamedTest2();
        }
    }
}