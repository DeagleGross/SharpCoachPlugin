using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace DefaultNamespace
{
    public static class MethodDeclarationHelpers
    {
        public static IBlock GetEmptyMethodBody(this IMethodDeclaration methodDeclaration)
        {
            var factory = CSharpElementFactory.GetInstance(methodDeclaration);
            return factory.CreateEmptyBlock();
        }
    }
}