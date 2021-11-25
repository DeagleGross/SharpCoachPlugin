using System.Collections.Generic;
using JetBrains.Collections;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Helpers;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;
using ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public class ClassesMappingProcessor
    {
        private readonly MappingCodeBuilder _mappingCodeBuilder;
        
        private readonly ClassTypeInfoProvider _fromClassType;
        private readonly ClassTypeInfoProvider _toClassType;
        
        public MappingResultWrapper MappingResultWrapper { get; }

        public FailedToMapPropertiesContainer FailedToMapPropertiesContainer => MappingResultWrapper.FailedToMapPropertiesContainer;

        public ClassesMappingProcessor(ClassTypeInfoProvider fromClassType, ClassTypeInfoProvider toClassType)
            : this(fromClassType, toClassType, fromClassType.VariableName)
        {
        }

        private ClassesMappingProcessor(ClassTypeInfoProvider fromClassType, ClassTypeInfoProvider toClassType, string variableName)
        {
            _fromClassType = fromClassType;
            _toClassType = toClassType;
            
            _mappingCodeBuilder = new MappingCodeBuilder(variableName);
            MappingResultWrapper = new MappingResultWrapper(fromClassType, toClassType);
        }

        public string BuildMappingCode()
        {
            var fromClassProperties = _fromClassType.GetPropertyNameInfoMap();
            var toClassProperties = _toClassType.GetPropertyNameInfoMap();
            var processedPropertyNames = new HashSet<string>();

            // compares properties one by one and does the mapping
            foreach (var (toClassPropertyName, toClassProperty) in toClassProperties)
            {
                // finding property in `fromClass` with same name, but not type
                if (fromClassProperties.ContainsKey(toClassPropertyName))
                {
                    var fromClassProperty = fromClassProperties[toClassPropertyName];

                    // the most simple case - properties have the same type
                    if (Equals(fromClassProperty.Type, toClassProperty.Type))
                    {
                        _mappingCodeBuilder.AddPropertyBindingStandard(toClassPropertyName);
                        processedPropertyNames.Add(toClassPropertyName);
                    }
                    else
                    {
                        var fromPropertyTypeKind = fromClassProperty.Type.GetTypeKind();
                        var toPropertyTypeKind = toClassProperty.Type.GetTypeKind();

                        if (fromPropertyTypeKind is null || toPropertyTypeKind is null)
                        {
                            LogLog.Warn("Failed to determine type kind of property `{0}`", toClassPropertyName);
                            continue;
                        }

                        /* Here we are talking about collections of different types or about different types themselves.
                         *
                         * ---
                         * Numeric types:
                         * Any `long` to `int`, `int` to `byte` casts are really redundant.
                         * in other way casts are needed.
                         *
                         * ---
                         * String:
                         * For `string` another cast is made -> `ToString()`.
                         * In other way there are complex casts: i.e. `long.Parse(numberStringValue)`
                         * 
                         * ---
                         * Enums:
                         * We can map `enum` to `string` by using expression `enum.ToString()`
                         * in other direction we can use expression `Enum.Parse<EnumType>(enumStringValue)`.
                         *
                         * ---
                         * Different Types:
                         * If we have 2 classes of differentTypes the task comes down to the same problem,
                         * so the solution is to create another `ClassesMappingProcessor` and build mapping code.
                         *
                         * ---
                         * Same collection, but different types:
                         * I.e. class Stock1 and Stock2 with same internal fields and properties syntactically
                         * {
                         *      public long Id { get; set; }
                         *      public string Name { get; set; }
                         * }
                         *
                         * We need to write a LINQ to map them and then cast it to collection, specified in either class
                         * (or leave it is IEnumerable<T>)
                         * Stocks = class2.Stocks.Select(x => new Class1.Stock1 { ... }).ToCollectionType();
                         *
                         * ---
                         * Different collections, different types:
                         * Again it is the same approach as the last one variant,
                         * but you dont have any information how to map i.e. `Stock[]` to `Dictionary<string, Stock>`
                         */
                        
                        var specificTypeMapper = SpecificTypeMapperFactory.CreateSpecificMapper(_mappingCodeBuilder, fromPropertyTypeKind.Value);
                        if (specificTypeMapper is null)
                        {
                            LogLog.Warn("Failed to find appropriate specificTypeMapper for property `{0}`", fromClassProperty.ShortName);
                            continue;
                        }
                        
                        var isSuccess = specificTypeMapper.TryMapToType(fromClassProperty, toClassProperty, toPropertyTypeKind.Value);
                        if (isSuccess) processedPropertyNames.Add(toClassPropertyName);
                        
                        MappingResultWrapper.FailedToMapPropertiesContainer.Add(specificTypeMapper.InternalFailedPropertiesContainer);
                    }
                }
            }

            FillFailedToMapPropertiesInfo(fromClassProperties, toClassProperties, processedPropertyNames);

            return _mappingCodeBuilder.Result;
        }

        private void FillFailedToMapPropertiesInfo(
            IReadOnlyDictionary<string, IProperty> fromClassProperties, 
            IReadOnlyDictionary<string, IProperty> toClassProperties,
            ICollection<string> processedPropertyNames)
        {
            foreach (var (propertyName, property) in fromClassProperties)
            {
                if (!processedPropertyNames.Contains(propertyName)) MappingResultWrapper.AddFailedToMapInputProperty(property.GetFullName());    
            }
            
            foreach (var (propertyName, property) in toClassProperties)
            {
                if (!processedPropertyNames.Contains(propertyName)) MappingResultWrapper.AddFailedToMapOutputProperty(property.GetFullName());    
            } 
        }
    }
}