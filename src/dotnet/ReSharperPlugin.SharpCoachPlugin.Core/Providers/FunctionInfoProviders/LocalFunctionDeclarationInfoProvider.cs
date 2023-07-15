using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperPlugin.SharpCoachPlugin.Core.Helpers;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders
{
    public class LocalFunctionDeclarationInfoProvider : FunctionInfoProviderBase
    {
        private readonly ILocalFunctionDeclaration _functionDeclaration;

        public override ICSharpDeclaration CSharpDeclaration => _functionDeclaration;
        public override ITypeUsage FunctionDeclarationTypeUsage => _functionDeclaration.TypeUsage;
        public override ICSharpTreeNode ParameterList => _functionDeclaration.ParameterList;
        
        public LocalFunctionDeclarationInfoProvider(IContextActionDataProvider contextActionDataProvider)
        {
            _functionDeclaration = contextActionDataProvider.GetSelectedElement<ILocalFunctionDeclaration>();
        }

        public static bool MatchesContextAction(IContextActionDataProvider contextActionDataProvider) 
            => contextActionDataProvider.GetSelectedElement<ILocalFunctionDeclaration>() is not null;

        public override bool HasSingleArgument()
        {
            if (_functionDeclaration is null) return false;
            if (ParameterList?.FirstChild is null) return false;
            return ParameterList?.FirstChild?.NextSibling is null && ParameterList?.FirstChild?.PrevSibling is null;
        }

        public override bool ReturnsReferenceType() =>
            _functionDeclaration is not null && 
            _functionDeclaration.Type.Classify == TypeClassification.REFERENCE_TYPE;

        public override IBlock GetMethodBody() => _functionDeclaration.Body ?? _functionDeclaration.GetEmptyMethodBody();
        public override bool IsEmpty() => _functionDeclaration.Body.Statements.IsEmpty;
    }
}