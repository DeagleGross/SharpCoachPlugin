using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;
using ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public class EnumTypeMapper : SpecificTypeMapperBase
    {
        public EnumTypeMapper(MappingCodeBuilder codeBuilder)
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
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Enum`");
                    return false;
                
                case TypeKind.String:
                    return TryMapToString(fromProperty);

                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Class`");
                    return false;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Struct`");
                    return false;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Collection`");
                    return false;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }
        
        private bool TryMapToNumeric(IProperty fromProperty, IProperty toProperty)
        {
            var toNumericType = toProperty.Type.ToNumeric();
            if (toNumericType is null)
            {
                LogLog.Warn("Failed to map to numeric ({0})", fromProperty.ShortName);
                return false;
            }

            CodeBuilder.AddPropertyBindingWithCast(toProperty.ShortName, toNumericType.Value.GetNumericTypeStringRepresentation());
            return true;
        }

        private bool TryMapToString(IProperty fromProperty)
        {
            CodeBuilder.AddPropertyBindingWithToStringCall(fromProperty.ShortName);
            return true;
        }
    }
}