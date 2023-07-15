using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using ReSharperPlugin.SharpCoachPlugin.Actions;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class NonEmptyMethodTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\NonEmptyMethod";

        [Test]
        public void TestNonEmptyMethod()
        {
            // output should be "NOT AVAILABLE\r\n" if action is not available. No other way to check it.
            // classic JetBrains SDK ¯\_(ツ)_/¯
            DoNamedTest2();
        }
    }
}