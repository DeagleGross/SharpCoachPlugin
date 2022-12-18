using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using ReSharperPlugin.SharpCoachPlugin.Actions;

namespace ReSharperPlugin.SharpCoachPlugin.Tests.test.src.Actions
{
    public class FullModelNameTests : CSharpContextActionExecuteTestBase<MapModelsAction>
    {
        protected override string ExtraPath => @"Actions\FullModelNameTests";

        [Test]
        public void TestFullInputModelNameTest() { DoNamedTest2(); }
        
        [Test]
        public void TestFullOutputModelNameTest() { DoNamedTest2(); }
    }
}