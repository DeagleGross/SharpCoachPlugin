using System;
using DefaultNamespace;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public class NumericTypeMapper : SpecificTypeMapperBase
    {
        public NumericTypeMapper(MappingCodeBuilder codeBuilder) 
            : base(codeBuilder)
        {
        }

        public override void MapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType)
        {
            switch (toType)
            {
                case TypeKind.Numeric:
                    MapToNumeric(fromProperty, toProperty);
                    break;
                
                case TypeKind.Enum:
                    
                    break;
                
                case TypeKind.String:
                    break;
                
                case TypeKind.Class:
                    // can not think about a solution for this case
                    break;
                
                case TypeKind.Structure:
                    // can not think about a solution for this case
                    break;
                
                case TypeKind.Collection:
                    // can not think about a solution for this case
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }

        private void MapToNumeric(IProperty fromProperty, IProperty toProperty)
        {
            var fromNumericType = fromProperty.Type.ToNumeric();
            var toNumericType = toProperty.Type.ToNumeric();

            if (fromNumericType is null || toNumericType is null)
            {
                LogLog.Warn("Failed to map numeric types one to another ({0})", fromProperty.ShortName);
                return;
            }

            CodeBuilder.AddNumericPropertyBinding(fromProperty.ShortName, fromNumericType.Value, toNumericType.Value);
        }
        
        private void MapToEnum(IProperty fromProperty, IProperty toProperty)
        {
            CodeBuilder.AddNumericToEnumPropertyBinding("asdf", fromProperty.ShortName);
        }

        private void MapToString(IProperty fromProperty, IProperty toProperty)
        {
            
        }
    }
}