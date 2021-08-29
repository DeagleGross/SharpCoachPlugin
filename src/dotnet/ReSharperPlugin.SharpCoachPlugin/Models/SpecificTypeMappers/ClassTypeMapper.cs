using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace DefaultNamespace
{
    public class ClassTypeMapper : SpecificTypeMapperBase
    {
        public ClassTypeMapper(MappingCodeBuilder codeBuilder) 
            : base(codeBuilder)
        {
        }

        public override void MapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType)
        {
            switch (toType)
            {
                case TypeKind.Numeric:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Numeric`");
                    break;
                
                case TypeKind.Enum:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Enum`");
                    break;
                
                case TypeKind.String:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `String`");
                    break;
                
                case TypeKind.Class:
                    MapToClass(fromProperty, toProperty);
                    break;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Structure`");
                    break;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Collection`");
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }

        private void MapToClass(IProperty fromProperty, IProperty toProperty)
        {
            var fromClassVariableName = $"{CodeBuilder.FromVariableName}.{fromProperty.ShortName}";

            var fromClassElement = fromProperty.Type.GetTypeElement() as IClass;
            var toClassElement = toProperty.Type.GetTypeElement() as IClass;

            var fromClassTypeInfo = new ClassTypeInfoProvider(fromClassElement, fromClassVariableName);
            var toClassTypeInfo = new ClassTypeInfoProvider(toClassElement);

            if (!fromClassTypeInfo.HasValidModelInfo || !toClassTypeInfo.HasValidModelInfo)
            {
                LogLog.Warn("Failed to map to classes one to another for property `{0}`", fromProperty.ShortName);
                return;
            }
            
            var classesMappingProcessor = new ClassesMappingProcessor(fromClassTypeInfo, toClassTypeInfo);
            var internalClassesCodeMapping = classesMappingProcessor.BuildMappingCode();

            CodeBuilder.AddClassPropertyBinding(fromProperty.ShortName, toClassTypeInfo.FullClassTypeName, internalClassesCodeMapping);
        }
    }
}