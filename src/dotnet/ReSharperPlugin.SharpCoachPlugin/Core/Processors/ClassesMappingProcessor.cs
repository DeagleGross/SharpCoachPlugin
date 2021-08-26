using DefaultNamespace;
using JetBrains.Collections;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public class ClassesMappingProcessor
    {
        private readonly CodeBlockBuilder _codeBlockBuilder;
        
        private readonly ClassInfoProvider _fromType;
        private readonly ClassInfoProvider _toType;

        public ClassesMappingProcessor(ClassInfoProvider fromType, ClassInfoProvider toType, IBlock methodBody)
        {
            _fromType = fromType;
            _toType = toType;

            _codeBlockBuilder = new CodeBlockBuilder(methodBody);
        }

        public IBlock BuildMappingCode()
        {
            var fromClassProperties = _fromType.GetPropertiesSet();
            var toClassProperties = _toType.GetPropertiesSet();

            // compares properties one by one and does the mapping
            foreach (var (propertyDescriptor, property) in fromClassProperties)
            {
                // same type and name
                if (toClassProperties.ContainsKey(propertyDescriptor))
                {
                    // simple types
                    if (!propertyDescriptor.Type.IsReferenceType())
                    {
                        
                    }
                    else
                    {
                        
                    }
                }
                
                // same name only
            }

            return null;
        }
    }
}