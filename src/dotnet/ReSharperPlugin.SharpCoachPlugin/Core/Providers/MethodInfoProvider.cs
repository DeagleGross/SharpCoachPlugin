using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public class MethodInfoProvider
    {
        public IMethodDeclaration MethodDeclaration { get; }

        public MethodInfoProvider(IMethodDeclaration methodDeclaration)
        {
            MethodDeclaration = methodDeclaration;
        }

        public bool HasSingleArgument
        {
            get
            {
                if (MethodDeclaration is null) return false;
                if (MethodDeclaration.Params.FirstChild is null) return false;
                return MethodDeclaration.Params.FirstChild.NextSibling is null && 
                       MethodDeclaration.Params.FirstChild.PrevSibling is null;
            }
        }

        public bool ReturnsReferenceType => MethodDeclaration.Type.Classify == TypeClassification.REFERENCE_TYPE;

        public ClassInfoProvider GetReturnTypeDeclaration()
        {
            var returnTypeReference = MethodDeclaration.TypeUsage?.LastChild as IReferenceName;
            var classDeclaration = returnTypeReference?.Reference.Resolve();
            
            return classDeclaration?.IsValid() == true 
                ? new ClassInfoProvider(classDeclaration.DeclaredElement as IClass) 
                : new ClassInfoProvider(null);
        }
        
        public ClassInfoProvider GetArgumentTypeDeclaration(int index)
        {
            var argumentTreeNode = MethodDeclaration.Params.FindNodeAt(new TreeTextRange(new TreeOffset(index)));
            var argumentReferenceName = argumentTreeNode?.Parent as IReferenceName;
            
            var classDeclaration = argumentReferenceName?.Reference.Resolve();
            
            return classDeclaration?.IsValid() == true 
                ? new ClassInfoProvider(classDeclaration.DeclaredElement as IClass) 
                : new ClassInfoProvider(null);
        }
    }
}