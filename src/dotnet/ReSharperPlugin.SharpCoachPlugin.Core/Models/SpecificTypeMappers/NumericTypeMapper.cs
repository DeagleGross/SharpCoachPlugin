using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;
using ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public class NumericTypeMapper : SpecificTypeMapperBase
    {
        public NumericTypeMapper(MappingCodeBuilder codeBuilder) 
            : base(codeBuilder)
        {
        }

        public override bool TryMapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType)
        {
            switch (toType)
            {
                case TypeKind.Numeric:
                    return TryMapToNumeric(fromProperty, toProperty);

                case TypeKind.Enum:
                    return TryMapToEnum(fromProperty, toProperty);

                case TypeKind.String:
                    return TryMapToString(fromProperty, toProperty);

                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Numeric` type to `Class`");
                    return false;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Numeric` type to `Struct`");
                    return false;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Numeric` type to `Collection`");
                    return false;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }

        private bool TryMapToNumeric(IProperty fromProperty, IProperty toProperty)
        {
            var fromNumericType = fromProperty.Type.ToNumeric();
            var toNumericType = toProperty.Type.ToNumeric();

            if (fromNumericType is null || toNumericType is null)
            {
                LogLog.Warn("Failed to map numeric types one to another ({0})", fromProperty.ShortName);
                return false;
            }

            if (fromNumericType > toNumericType)
            {
                CodeBuilder.AddPropertyBindingWithCast(fromProperty.ShortName, toNumericType.Value.GetNumericTypeStringRepresentation());
                return true;
            }
            else
            {
                CodeBuilder.AddPropertyBindingStandard(fromProperty.ShortName);
                return true;
            }
        }
        
        private bool TryMapToEnum(IProperty fromProperty, IProperty toProperty)
        {
            var fullEnumTypeName = toProperty.Type.GetLongPresentableName(CSharpLanguage.Instance!);
            CodeBuilder.AddPropertyBindingWithCast(toProperty.ShortName, fullEnumTypeName);
            return true;
        }

        private bool TryMapToString(IProperty fromProperty, IProperty toProperty)
        {
            CodeBuilder.AddPropertyBindingWithToStringCall(toProperty.ShortName);
            return true;
        }
    }
}