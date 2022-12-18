using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Helpers
{
    public static class MethodDeclarationHelpers
    {
        public static IBlock GetEmptyMethodBody(this IMethodDeclaration functionDeclaration)
        {
            var factory = CSharpElementFactory.GetInstance(functionDeclaration);
            return factory.CreateEmptyBlock();
        }
        
        public static IBlock GetEmptyMethodBody(this ILocalFunctionDeclaration functionDeclaration)
        {
            var factory = CSharpElementFactory.GetInstance(functionDeclaration);
            return factory.CreateEmptyBlock();
        }
    }
}