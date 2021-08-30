using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers;
using ReSharperPlugin.SharpCoachPlugin.Models.Types;

namespace DefaultNamespace
{
    public class EnumTypeMapper : SpecificTypeMapperBase
    {
        public EnumTypeMapper(MappingCodeBuilder codeBuilder)
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
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Enum`");
                    break;
                
                case TypeKind.String:
                    MapToString(fromProperty);
                    break;
                
                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Class`");
                    break;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Struct`");
                    break;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Enum` type to `Collection`");
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }
        
        private void MapToNumeric(IProperty fromProperty, IProperty toProperty)
        {
            var toNumericType = toProperty.Type.ToNumeric();
            if (toNumericType is null)
            {
                LogLog.Warn("Failed to map to numeric ({0})", fromProperty.ShortName);
                return;
            }

            CodeBuilder.AddPropertyBindingWithCast(toProperty.ShortName, toNumericType.Value.GetNumericTypeStringRepresentation());
        }

        private void MapToString(IProperty fromProperty)
        {
            CodeBuilder.AddPropertyBindingWithToStringCall(fromProperty.ShortName);
        }
    }
}