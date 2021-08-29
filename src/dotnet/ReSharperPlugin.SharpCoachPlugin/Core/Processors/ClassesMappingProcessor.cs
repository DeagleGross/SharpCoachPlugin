using DefaultNamespace;
using JetBrains.Collections;
using JetBrains.Diagnostics;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public class ClassesMappingProcessor
    {
        private readonly MappingCodeBuilder _mappingCodeBuilder;
        
        private readonly ClassTypeInfoProvider _fromClassType;
        private readonly ClassTypeInfoProvider _toClassType;

        public ClassesMappingProcessor(ClassTypeInfoProvider fromClassType, ClassTypeInfoProvider toClassType)
        {
            _fromClassType = fromClassType;
            _toClassType = toClassType;

            _mappingCodeBuilder = new MappingCodeBuilder(_fromClassType.VariableName);
        }

        public string BuildMappingCode()
        {
            var fromClassProperties = _fromClassType.GetPropertyNameInfoMap();
            var toClassProperties = _toClassType.GetPropertyNameInfoMap();

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
                        _mappingCodeBuilder.AddSimplePropertyBinding(toClassPropertyName);   
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
                        
                        var specificTypeMapper = SpecificTypeMapperFactory.Create(_mappingCodeBuilder, fromPropertyTypeKind.Value);
                        if (specificTypeMapper is null)
                        {
                            LogLog.Warn("Failed to find appropriate specificTypeMapper for property `{0}`", fromClassProperty.ShortName);
                            continue;
                        }
                        
                        specificTypeMapper.MapToType(fromClassProperty, toClassProperty, toPropertyTypeKind.Value);
                    }
                }
            }

            return _mappingCodeBuilder.Result;
        }
    }
}