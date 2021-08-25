using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.Util;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.CSharp.Util;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace DefaultNamespace
{
    [ContextAction(Name = "MapModelsAction", Description = "Map internals of models", Group = "C#", Disabled = false, Priority = 2)]
    public class MapModelsAction : ContextActionBase
    {
        private readonly IMethodDeclaration _initialMethodDeclaration;
        
        private readonly MethodInfoProvider _methodInfoProvider;
        private MappingProcessor _mappingProcessor;

        public override string Text => "Map internals of models";

        public MapModelsAction(LanguageIndependentContextActionDataProvider  dataProvider)
        {
            _initialMethodDeclaration = dataProvider.GetSelectedElement<IMethodDeclaration>();
            _methodInfoProvider = new MethodInfoProvider(_initialMethodDeclaration);
        }
        
        public override bool IsAvailable(IUserDataHolder cache)
        {
            var methodHasSingleArgument = _methodInfoProvider.HasSingleArgument;
            var methodReturnTypeIsOfReferenceType = _methodInfoProvider.ReturnsReferenceType;

            return methodHasSingleArgument && methodReturnTypeIsOfReferenceType;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            if (_mappingProcessor is null)
            {
                var model1 = _methodInfoProvider.GetReturnTypeDeclaration();
                var model2 = _methodInfoProvider.GetArgumentTypeDeclaration(0);
                
                _mappingProcessor = new MappingProcessor(model1, model2);
            }

            // TODO set real body of method
            _initialMethodDeclaration.SetBody(null);

            return null;
        }
    }
}