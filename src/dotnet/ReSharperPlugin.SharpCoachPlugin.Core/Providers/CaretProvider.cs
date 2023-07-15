using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public readonly struct CaretProvider
    {
        private readonly ITreeNode _documentCaretNode;

        public CaretProvider(LanguageIndependentContextActionDataProvider dataProvider)
        {
            _documentCaretNode = dataProvider?.TokenAfterCaret;
        }
        
        public readonly bool PointsMethodName(IFunctionInfoProvider functionInfoProvider)
            => _documentCaretNode?.Parent == functionInfoProvider.CSharpDeclaration;
    }
}