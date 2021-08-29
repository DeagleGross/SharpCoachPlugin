using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;

namespace DefaultNamespace
{
    public class StringTypeMapper : SpecificTypeMapperBase
    {
        public StringTypeMapper(MappingCodeBuilder codeBuilder) 
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
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `String`");
                    break;
                
                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `Class`");
                    break;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `Struct`");
                    break;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `String` type to `Collection`");
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
                LogLog.Warn("Failed to map string to numeric type `{0}`", fromProperty.ShortName);
                return;
            }

            CodeBuilder.AddWithNumericTryParseCast(fromProperty.ShortName, toNumericType.Value.GetNumericTypeStringRepresentation());
        }

        private void MapToEnum(IProperty fromProperty, IProperty toProperty)
        {
            var fullEnumTypeName = toProperty.Type.GetLongPresentableName(CSharpLanguage.Instance!);
            CodeBuilder.AddWithEnumTryParseCast(fromProperty.ShortName, fullEnumTypeName);
        }
    }
}