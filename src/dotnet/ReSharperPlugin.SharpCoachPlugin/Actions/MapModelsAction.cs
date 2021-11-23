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
using ReSharperPlugin.SharpCoachPlugin.Core.Helpers;
using ReSharperPlugin.SharpCoachPlugin.Core.Models;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;
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

        private readonly IMethodDeclaration _methodDeclaration;
        private readonly MethodInfoProvider _methodInfoProvider;

        private ClassTypeInfoProvider _toClassType;
        private ClassTypeInfoProvider _fromClassType;

        private ClassesMappingProcessor _classesMappingProcessor;

        public MapModelsAction(LanguageIndependentContextActionDataProvider dataProvider)
        {
            _methodDeclaration = dataProvider.GetSelectedElement<IMethodDeclaration>();
            _methodInfoProvider = new MethodInfoProvider(_methodDeclaration);
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            var methodHasSingleArgument = _methodInfoProvider.HasSingleArgument;
            var methodReturnTypeIsOfReferenceType = _methodInfoProvider.ReturnsReferenceType;

            if (!(methodHasSingleArgument && methodReturnTypeIsOfReferenceType))
            {
                return false;
            }

            _toClassType = _methodInfoProvider.GetReturnTypeDeclaration();
            _fromClassType = _methodInfoProvider.GetArgumentTypeDeclaration(0);

            return _toClassType.HasValidModelInfo && _fromClassType.HasValidModelInfo;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            _classesMappingProcessor ??= new ClassesMappingProcessor(_fromClassType, _toClassType);

            var methodMappingCode = _classesMappingProcessor.BuildMappingCode();
            var mappingMethodBody = EmbedMappingCodeToMethodBody(methodMappingCode);

            _methodDeclaration.SetCodeBody(mappingMethodBody);

            var a = Shell.Instance.GetComponent<MappingResultsProvider>();
            a.Add(new MappingOperationResult
            {
                InputClassName = "hello",
                OutputClassName = "world",
                OperationDate = DateTime.Now,
                InputClassErrorPropertyNames = new [] { "h", "e" },
                OutputClassErrorPropertyNames = new [] { "q", "qwe", "qwe" }
            });
            
            ShowMapModelsToolWindow();

            return null;
        }

        private ICSharpStatement EmbedMappingCodeToMethodBody(string internalMappingCode)
        {
            var methodBody = _methodDeclaration.Body ?? _methodDeclaration.GetEmptyMethodBody();
            var factory = CSharpElementFactory.GetInstance(methodBody);

            return factory.CreateStatement(string.Format(MethodReturnFormat, _toClassType.FullClassTypeName,
                internalMappingCode));
        }

        private void ShowMapModelsToolWindow()
        {
            var mappingResultsProvider = Shell.Instance.GetComponent<IMappingResultsStorage>();
            MapModelsLogsToolWindow.Show(mappingResultsProvider);
        }
    }
}