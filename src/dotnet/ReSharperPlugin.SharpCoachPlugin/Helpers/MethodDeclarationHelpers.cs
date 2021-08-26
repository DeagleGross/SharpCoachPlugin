using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace DefaultNamespace
{
    public static class MethodDeclarationHelpers
    {
        public static void RemoveBodyStatements(this IMethodDeclaration methodDeclaration)
        {
            var methodBody = methodDeclaration.Body;

            if (methodBody is null)
            {
                // create method body
                var factory = CSharpElementFactory.GetInstance(methodDeclaration);
                methodBody = factory.CreateBlock(string.Empty);
            }
            else
            {
                methodBody.RemoveOrReplaceByEmptyStatement();
            }
            
            methodDeclaration.SetBody(methodBody);
        }
    }
}