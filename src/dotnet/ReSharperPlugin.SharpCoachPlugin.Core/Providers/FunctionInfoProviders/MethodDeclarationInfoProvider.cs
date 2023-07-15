using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperPlugin.SharpCoachPlugin.Core.Helpers;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders
{
    public class MethodDeclarationInfoProvider : FunctionInfoProviderBase
    {
        private readonly IMethodDeclaration _methodDeclaration;

        public override ICSharpDeclaration CSharpDeclaration => _methodDeclaration;
        public override ITypeUsage FunctionDeclarationTypeUsage => _methodDeclaration.TypeUsage;
        public override ICSharpTreeNode ParameterList => _methodDeclaration.Params;
        
        public MethodDeclarationInfoProvider(IContextActionDataProvider contextActionDataProvider)
        {
            _methodDeclaration = contextActionDataProvider.GetSelectedElement<IMethodDeclaration>();
        }

        public static bool MatchesContextAction(IContextActionDataProvider contextActionDataProvider) 
            => contextActionDataProvider.GetSelectedElement<IMethodDeclaration>() is not null;

        public override bool HasSingleArgument()
        {
            if (_methodDeclaration is null) return false;
            
            var methodParams = _methodDeclaration.Params;
            if (methodParams?.FirstChild is null) return false;
            return methodParams?.FirstChild?.NextSibling is null && methodParams?.FirstChild?.PrevSibling is null;
        }

        public override bool ReturnsReferenceType() => 
            _methodDeclaration is not null && 
            _methodDeclaration.Type.Classify == TypeClassification.REFERENCE_TYPE;
        
        public override IBlock GetMethodBody() => _methodDeclaration.Body ?? _methodDeclaration.GetEmptyMethodBody();

        public override bool IsEmpty() => _methodDeclaration.Body.Statements.IsEmpty;
    }
}