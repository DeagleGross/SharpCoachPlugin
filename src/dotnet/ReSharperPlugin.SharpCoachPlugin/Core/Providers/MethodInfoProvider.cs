using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public class MethodInfoProvider
    {
        private readonly IMethodDeclaration _methodDeclaration;

        public MethodInfoProvider(IMethodDeclaration methodDeclaration)
        {
            _methodDeclaration = methodDeclaration;
        }

        public bool HasSingleArgument
        {
            get
            {
                if (_methodDeclaration is null) return false;
                if (_methodDeclaration.Params.FirstChild is null) return false;
                return _methodDeclaration.Params.FirstChild.NextSibling is null && 
                       _methodDeclaration.Params.FirstChild.PrevSibling is null;
            }
        }

        public bool ReturnsReferenceType => 
            _methodDeclaration is not null &&
            _methodDeclaration.Type.Classify == TypeClassification.REFERENCE_TYPE;

        public ClassTypeInfoProvider GetReturnTypeDeclaration()
        {
            var returnTypeReference = _methodDeclaration.TypeUsage?.LastChild as IReferenceName;
            var classDeclaration = returnTypeReference?.Reference.Resolve();
            
            return classDeclaration?.IsValid() == true 
                ? new ClassTypeInfoProvider(classDeclaration.DeclaredElement as IClass) 
                : new ClassTypeInfoProvider(null);
        }
        
        public ClassTypeInfoProvider GetArgumentTypeDeclaration(int index)
        {
            var argument = _methodDeclaration.Params.FindNodeAt(new TreeTextRange(new TreeOffset(index)));
            var argumentReferenceName = argument?.Parent as IReferenceName;

            var classDeclaration = argumentReferenceName?.Reference.Resolve();
            var parameter = _methodDeclaration.Params.Children().ElementAt(index) as IParameterDeclaration;
            
            return classDeclaration?.IsValid() == true 
                ? new ClassTypeInfoProvider(classDeclaration.DeclaredElement as IClass, parameter?.DeclaredName) 
                : new ClassTypeInfoProvider(null, parameter?.DeclaredName);
        }
    }
}