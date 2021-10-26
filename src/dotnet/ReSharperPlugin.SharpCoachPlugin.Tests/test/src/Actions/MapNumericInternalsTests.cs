using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using ReSharperPlugin.SharpCoachPlugin.Actions;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class MapNumericInternalsTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\Numeric";

        [Test]
        public void TestMapSameTypeAndNameProperties()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestMapNumericToEnumActionTest()
        {
            DoNamedTest2();
        }
        
        [Test]
        public void TestMapNumericToStringActionTest()
        {
            DoNamedTest2();
        }
    }
}