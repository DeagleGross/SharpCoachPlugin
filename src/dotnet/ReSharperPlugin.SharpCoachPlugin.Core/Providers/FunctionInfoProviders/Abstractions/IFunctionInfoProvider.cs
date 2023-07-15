using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions
{
    public interface IFunctionInfoProvider
    {
        IBlock GetMethodBody();
        
        ICSharpDeclaration CSharpDeclaration { get; }
        
        bool HasSingleArgument();

        bool ReturnsReferenceType();

        ClassTypeInfoProvider GetReturnTypeDeclaration();

        ClassTypeInfoProvider GetArgumentTypeDeclaration(int index);
    }
}