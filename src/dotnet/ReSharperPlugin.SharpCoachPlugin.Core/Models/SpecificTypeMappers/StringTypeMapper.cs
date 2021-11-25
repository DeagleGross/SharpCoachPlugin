using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;
using ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public class StringTypeMapper : SpecificTypeMapperBase
    {
        public StringTypeMapper(MappingCodeBuilder codeBuilder) 
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
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `String`");
                    return false;
                
                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `Class`");
                    return false;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `Struct`");
                    return false;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `Collection`");
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
                LogLog.Warn("Failed to map string to numeric type `{0}`", fromProperty.ShortName);
                return false;
            }

            CodeBuilder.AddPropertyBindingWithNumericTryParseCast(fromProperty.ShortName, toNumericType.Value.GetNumericTypeStringRepresentation());
            return true;
        }

        private bool TryMapToEnum(IProperty fromProperty, IProperty toProperty)
        {
            var fullEnumTypeName = toProperty.Type.GetLongPresentableName(CSharpLanguage.Instance!);
            CodeBuilder.AddPropertyBindingWithEnumTryParseCast(fromProperty.ShortName, fullEnumTypeName);
            return true;
        }
    }
}