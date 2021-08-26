using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace DefaultNamespace
{
    public class CodeBlockBuilder
    {
        private readonly IBlock _codeBlock;
        private readonly CSharpElementFactory _factory;

        public CodeBlockBuilder(IBlock codeBlock)
        {
            _codeBlock = codeBlock;
            _factory = CSharpElementFactory.GetInstance(codeBlock);
        }

        public void AddReturnStatement(string returnTypeName)
        {
            var returnStatement = _factory.CreateStatement($"return new {returnTypeName}");
            _codeBlock.AddStatementAfter(returnStatement, _codeBlock);
        }
    }
}