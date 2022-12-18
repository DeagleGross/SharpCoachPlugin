using JetBrains.ReSharper.Feature.Services.ContextActions;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders
{
    public static class FunctionInfoProviderFactory
    {
        public static IFunctionInfoProvider GetMatchingFunctionInfoProvider(
            IContextActionDataProvider contextActionDataProvider)
        {
            if (LocalFunctionDeclarationInfoProvider.MatchesContextAction(contextActionDataProvider))
                return new LocalFunctionDeclarationInfoProvider(contextActionDataProvider);
            
            if (MethodDeclarationInfoProvider.MatchesContextAction(contextActionDataProvider))
                return new MethodDeclarationInfoProvider(contextActionDataProvider);

            return null;
        }   
    }
}