using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public class ClassTypeMapper : SpecificTypeMapperBase
    {
        public ClassTypeMapper(MappingCodeBuilder codeBuilder) 
            : base(codeBuilder)
        {
        }

        public override bool TryMapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType)
        {
            switch (toType)
            {
                case TypeKind.Numeric:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Numeric`");
                    return false;
                
                case TypeKind.Enum:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Enum`");
                    return false;
                
                case TypeKind.String:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `String`");
                    return false;
                
                case TypeKind.Class:
                    return TryMapToClass(fromProperty, toProperty);

                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Structure`");
                    return false;
                
                case TypeKind.Collection:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Class` type to `Collection`");
                    return false;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }

        private bool TryMapToClass(IProperty fromProperty, IProperty toProperty)
        {
            var fromClassVariableName = $"{CodeBuilder.FromVariableName}.{fromProperty.ShortName}";

            var fromClassElement = fromProperty.Type.GetTypeElement() as IClass;
            var toClassElement = toProperty.Type.GetTypeElement() as IClass;

            var fromClassTypeInfo = new ClassTypeInfoProvider(fromClassElement, fromClassVariableName);
            var toClassTypeInfo = new ClassTypeInfoProvider(toClassElement);

            if (!fromClassTypeInfo.HasValidModelInfo || !toClassTypeInfo.HasValidModelInfo)
            {
                LogLog.Warn("Failed to map to classes one to another for property `{0}`", fromProperty.ShortName);
                return false;
            }
            
            var classesMappingProcessor = new ClassesMappingProcessor(fromClassTypeInfo, toClassTypeInfo);
            var internalClassesCodeMapping = classesMappingProcessor.BuildMappingCode();

            CodeBuilder.AddPropertyBindingForClass(fromProperty.ShortName, toClassTypeInfo.FullClassTypeName, internalClassesCodeMapping);

            // saving failed to map properties
            InternalFailedPropertiesContainer.Add(classesMappingProcessor.FailedToMapPropertiesContainer);
            
            return true;
        }
    }
}