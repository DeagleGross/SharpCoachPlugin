using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers.FunctionInfoProviders.Abstractions
{
    public abstract class FunctionInfoProviderBase : IFunctionInfoProvider
    {
        public abstract ICSharpDeclaration CSharpDeclaration { get; }
        
        public abstract ITypeUsage FunctionDeclarationTypeUsage { get; }
        
        public abstract ICSharpTreeNode ParameterList { get; }
        
        public abstract bool HasSingleArgument();
        public abstract bool ReturnsReferenceType();
        public abstract IBlock GetMethodBody();

        public ClassTypeInfoProvider GetReturnTypeDeclaration()
        {
            var returnTypeReference = FunctionDeclarationTypeUsage?.LastChild as IReferenceName;
            var classDeclaration = returnTypeReference?.Reference.Resolve();
            
            return classDeclaration?.IsValid() == true 
                ? new ClassTypeInfoProvider(classDeclaration.DeclaredElement as IClass) 
                : new ClassTypeInfoProvider(null);
        }

        public ClassTypeInfoProvider GetArgumentTypeDeclaration(int index)
        {
            /* Structure:
             * - IndexedArgument is a RegularParameterDeclaration
             *   - FirstChild is UserTypeUsage
             *     - FirstChild is ReferenceName, containing IDENTIFIER over user fully qualified user type 
             */
            
            var indexedArgument = ParameterList.Children().ElementAt(index);
            var argumentReferenceName = indexedArgument.FirstChild?.FirstChild as IReferenceName;

            var classDeclaration = argumentReferenceName?.Reference.Resolve();
            var parameter = ParameterList.Children().ElementAt(index) as IParameterDeclaration;
            
            return classDeclaration?.IsValid() == true 
                ? new ClassTypeInfoProvider(classDeclaration.DeclaredElement as IClass, parameter?.DeclaredName) 
                : new ClassTypeInfoProvider(null, parameter?.DeclaredName);
        }
    }
}