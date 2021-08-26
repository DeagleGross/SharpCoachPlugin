using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace DefaultNamespace
{
    [ContextAction(Name = "MapModelsAction", Description = "Map internals of models", Group = "C#", Disabled = false, Priority = 2)]
    public class MapModelsAction : ContextActionBase
    {
        private readonly MethodInfoProvider _methodInfoProvider;
        private ClassInfoProvider _toType;
        private ClassInfoProvider _fromType;
        
        private ClassesMappingProcessor _classesMappingProcessor;

        public override string Text => "Map internals of models";

        public MapModelsAction(LanguageIndependentContextActionDataProvider  dataProvider)
        {
            var initialMethodDeclaration = dataProvider.GetSelectedElement<IMethodDeclaration>();
            _methodInfoProvider = new MethodInfoProvider(initialMethodDeclaration);
        }
        
        public override bool IsAvailable(IUserDataHolder cache)
        {
            var methodHasSingleArgument = _methodInfoProvider.HasSingleArgument;
            var methodReturnTypeIsOfReferenceType = _methodInfoProvider.ReturnsReferenceType;

            if (!(methodHasSingleArgument && methodReturnTypeIsOfReferenceType))
            {
                return false;
            }
            
            _toType = _methodInfoProvider.GetReturnTypeDeclaration();
            _fromType = _methodInfoProvider.GetArgumentTypeDeclaration(0);

            return _toType.HasValidModelInfo && _fromType.HasValidModelInfo;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var methodDeclaration = _methodInfoProvider.MethodDeclaration;
            var methodBody = PrepareReturnMethodBodyForMapping();
            
            _classesMappingProcessor ??= new ClassesMappingProcessor(_fromType, _toType, methodBody);
            
            var methodMappingCode = _classesMappingProcessor.BuildMappingCode();
            methodDeclaration.SetBody(methodMappingCode);

            return null;
        }

        private IBlock PrepareReturnMethodBodyForMapping()
        {
            var methodDeclaration = _methodInfoProvider.MethodDeclaration;
            methodDeclaration.RemoveBodyStatements();
            
            var methodBody = methodDeclaration.Body;
            var factory = CSharpElementFactory.GetInstance(methodBody);

            var returnStatement =  factory.CreateStatement($"return new {_toType.ClassTypeName} {{");
            methodBody.AddStatementAfter(returnStatement, methodBody);

            return methodBody;
        }
    }
}