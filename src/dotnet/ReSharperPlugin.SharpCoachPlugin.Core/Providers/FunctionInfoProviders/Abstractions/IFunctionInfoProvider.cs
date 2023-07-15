using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions
{
    public interface IFunctionInfoProvider
    {
        /// <returns>true, if method implementation does not contain any code except whitespaces symbols</returns>
        bool IsEmpty();
        
        IBlock GetMethodBody();
        
        ICSharpDeclaration CSharpDeclaration { get; }
        
        bool HasSingleArgument();

        bool ReturnsReferenceType();

        ClassTypeInfoProvider GetReturnTypeDeclaration();

        ClassTypeInfoProvider GetArgumentTypeDeclaration(int index);
    }
}