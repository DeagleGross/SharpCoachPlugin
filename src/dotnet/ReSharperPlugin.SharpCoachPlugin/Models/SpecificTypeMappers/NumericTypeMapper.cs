using System;
using DefaultNamespace;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers;
using ReSharperPlugin.SharpCoachPlugin.Models.Types;

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
                    MapToEnum(fromProperty, toProperty);
                    break;
                
                case TypeKind.String:
                    MapToString(fromProperty, toProperty);
                    break;
                
                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Numeric` type to `Class`");
                    break;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Numeric` type to `Struct`");
                    break;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Numeric` type to `Collection`");
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

            if (fromNumericType > toNumericType)
            {
                CodeBuilder.AddPropertyBindingWithCast(fromProperty.ShortName, toNumericType.Value.GetNumericTypeStringRepresentation());
            }
            else
            {
                CodeBuilder.AddPropertyBindingStandard(fromProperty.ShortName);   
            }
        }
        
        private void MapToEnum(IProperty fromProperty, IProperty toProperty)
        {
            var fullEnumTypeName = toProperty.Type.GetLongPresentableName(CSharpLanguage.Instance!);
            CodeBuilder.AddPropertyBindingWithCast(toProperty.ShortName, fullEnumTypeName);
        }

        private void MapToString(IProperty fromProperty, IProperty toProperty)
        {
            CodeBuilder.AddPropertyBindingWithToStringCall(toProperty.ShortName);
        }
    }
}