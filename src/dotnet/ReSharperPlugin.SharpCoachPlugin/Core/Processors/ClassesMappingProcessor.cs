using System.Text;
using DefaultNamespace;
using JetBrains.Collections;
using JetBrains.ReSharper.Psi.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public class ClassesMappingProcessor
    {
        private readonly MappingCodeBuilder _mappingCodeBuilder;
        
        private readonly ReferenceTypeInfoProvider _fromType;
        private readonly ReferenceTypeInfoProvider _toType;

        public ClassesMappingProcessor(ReferenceTypeInfoProvider fromType, ReferenceTypeInfoProvider toType)
        {
            _fromType = fromType;
            _toType = toType;

            _mappingCodeBuilder = new MappingCodeBuilder(_fromType.VariableName);
        }

        public string BuildMappingCode()
        {
            var fromClassProperties = _fromType.GetPropertiesSet();
            var toClassProperties = _toType.GetPropertiesSet();

            // compares properties one by one and does the mapping
            foreach (var (propertyDescriptor, property) in fromClassProperties)
            {
                // same type and name
                if (toClassProperties.ContainsKey(propertyDescriptor))
                {
                    _mappingCodeBuilder.AddSimplePropertyBinding(property.ShortName);
                }
                
                // same name only
            }

            return _mappingCodeBuilder.Result;
        }
    }
}