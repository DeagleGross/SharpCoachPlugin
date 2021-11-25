using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;
using ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public class CollectionTypeMapper : SpecificTypeMapperBase
    {
        public CollectionTypeMapper(MappingCodeBuilder codeBuilder) 
            : base(codeBuilder)
        {
        }

        public override bool TryMapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType)
        {
            switch (toType)
            {
                case TypeKind.Numeric:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Numeric`");
                    return false;
                
                case TypeKind.Enum:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Enum`");
                    return false;
                
                case TypeKind.String:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `String`");
                    return false;
                
                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Class`");
                    return false;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Structure`");
                    return false;
                
                case TypeKind.Collection:
                    return TryMapToCollection(fromProperty, toProperty);

                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }

        private bool TryMapToCollection(IProperty fromProperty, IProperty toProperty)
        {
            var fromCollectionType = fromProperty.Type.ToCollectionType();
            var toCollectionType = fromProperty.Type.ToCollectionType();
            if (!fromCollectionType.SupportsLinq() || toCollectionType is null)
            {
                LogLog.Warn("Failed to find collection type of property {0}", fromProperty.ShortName);
                return false;
            }

            var fromUnderlyingType = fromProperty.GetUnderlyingType(fromCollectionType);
            var toUnderlyingType = toProperty.GetUnderlyingType(toCollectionType);
            if (fromUnderlyingType is null || toUnderlyingType is null)
            {
                LogLog.Warn("Failed to find underlying type for collection of property {0}", fromProperty.ShortName);
                return false;
            }
            
            // if both are classes
            {
                var fromClassVariableName = $"tmp{fromProperty.ShortName}";
        
                var fromClassElement = fromUnderlyingType.GetTypeElement() as IClass;
                var toClassElement = toUnderlyingType.GetTypeElement() as IClass;

                var fromClassTypeInfo = new ClassTypeInfoProvider(fromClassElement, fromClassVariableName);
                var toClassTypeInfo = new ClassTypeInfoProvider(toClassElement);

                if (!fromClassTypeInfo.HasValidModelInfo || !toClassTypeInfo.HasValidModelInfo)
                {
                    LogLog.Warn("Failed to map underlying classes one to another for property `{0}`", fromProperty.ShortName);
                    return false;
                }
            
                var classesMappingProcessor = new ClassesMappingProcessor(fromClassTypeInfo, toClassTypeInfo);
                var internalLinqCodeMapping = classesMappingProcessor.BuildMappingCode();
                
                if (!string.IsNullOrEmpty(internalLinqCodeMapping))
                {
                    var toCollectionCastMethod = toCollectionType.Value.GetToCollectionLinqCast();
                    CodeBuilder.AddPropertyBindingForLinqSelect(
                        fromProperty.ShortName,
                        $"{fromClassVariableName} => new {toClassTypeInfo.FullClassTypeName}(){{{internalLinqCodeMapping}}}",
                        toCollectionCastMethod);
                    return true;
                }
                
                InternalFailedPropertiesContainer.Add(classesMappingProcessor.FailedToMapPropertiesContainer);
            }

            LogLog.Warn("By now it is not supported... (mapping of non-classes collections)");
            return false;
        }
    }
}