using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

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

        public bool ReturnsReferenceType => _methodDeclaration.Type.Classify == TypeClassification.REFERENCE_TYPE;

        public ModelInfoProvider GetReturnTypeDeclaration()
        {
            // TODO extract information about return model
            return new ModelInfoProvider();
        }
        
        public ModelInfoProvider GetArgumentTypeDeclaration(int index)
        {
            // TODO extract information about parameter model
            return new ModelInfoProvider();
        }
    }
}