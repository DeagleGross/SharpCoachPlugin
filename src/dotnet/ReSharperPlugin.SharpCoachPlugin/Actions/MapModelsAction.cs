using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Components;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions;
using ReSharperPlugin.SharpCoachPlugin.Ui.ToolWindows;

namespace ReSharperPlugin.SharpCoachPlugin.Actions
{
    [ContextAction(Name = "MapModelsAction", Description = "Map internals of models", Group = "C#", Disabled = false, Priority = 2)]
    public class MapModelsAction : ContextActionBase
    {
        private const string MethodReturnFormat = @"
        {{ 
            return new {0}()
            {{ 
                {1} 
            }};
        }}";

        public override string Text => "Map internals of models";

        private readonly CaretProvider _caretProvider; 
        private readonly IFunctionInfoProvider _functionDeclaration;

        private ClassTypeInfoProvider _toClassType;
        private ClassTypeInfoProvider _fromClassType;

        private ClassesMappingProcessor _classesMappingProcessor;

        public MapModelsAction(LanguageIndependentContextActionDataProvider dataProvider)
        {
            _functionDeclaration = FunctionInfoProviderFactory.GetMatchingFunctionInfoProvider(dataProvider);
            _caretProvider = new CaretProvider(dataProvider);
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            if (_functionDeclaration is null) return false;
            if (!_caretProvider.PointsMethodName(_functionDeclaration)) return false;

            var methodHasSingleArgument = _functionDeclaration.HasSingleArgument();
            var methodReturnTypeIsOfReferenceType = _functionDeclaration.ReturnsReferenceType();

            if (!(methodHasSingleArgument && methodReturnTypeIsOfReferenceType))
            {
                return false;
            }

            _toClassType = _functionDeclaration.GetReturnTypeDeclaration();
            _fromClassType = _functionDeclaration.GetArgumentTypeDeclaration(0);

            return _toClassType.HasValidModelInfo && _fromClassType.HasValidModelInfo;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            _classesMappingProcessor ??= new ClassesMappingProcessor(_fromClassType, _toClassType);

            var methodMappingCode = _classesMappingProcessor.BuildMappingCode();
            var mappingMethodBody = EmbedMappingCodeToMethodBody(methodMappingCode);
            _functionDeclaration.CSharpDeclaration.SetCodeBody(mappingMethodBody);

            ShowMapModelsToolWindow();
            return null;
        }

        private ICSharpStatement EmbedMappingCodeToMethodBody(string internalMappingCode)
        {
            var methodBody = _functionDeclaration.GetMethodBody();
            var factory = CSharpElementFactory.GetInstance(methodBody);

            return factory.CreateStatement(string.Format(MethodReturnFormat, _toClassType.TypeUsage, internalMappingCode));
        }

        private void ShowMapModelsToolWindow()
        {
            var mappingResultsProvider = Shell.Instance.GetComponent<MappingResultsStorage>();
            mappingResultsProvider.Add(_classesMappingProcessor.MappingResultWrapper.MappingOperationResult);

            // explicitly showing window only when operation is not successful
            MapModelsLogsToolWindowUI.Show(mappingResultsProvider, _classesMappingProcessor.MappingResultWrapper.IsSuccessful);
        }
    }
}