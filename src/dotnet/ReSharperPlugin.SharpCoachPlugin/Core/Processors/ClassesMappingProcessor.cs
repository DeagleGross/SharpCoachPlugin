using DefaultNamespace;
using JetBrains.Collections;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public class ClassesMappingProcessor
    {
        private readonly MappingCodeBuilder _mappingCodeBuilder;
        
        private readonly ReferenceTypeInfoProvider _fromType;
        private readonly ReferenceTypeInfoProvider _toType;

        public ClassesMappingProcessor(ReferenceTypeInfoProvider fromType, ReferenceTypeInfoProvider toType)
        {
            _fromType = fromType;
            _toType = toType;

            _mappingCodeBuilder = new MappingCodeBuilder(_fromType.VariableName);
        }

        public string BuildMappingCode()
        {
            var fromClassProperties = _fromType.GetPropertyNameInfoMap();
            var toClassProperties = _toType.GetPropertyNameInfoMap();

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

                        
                    }
                }
            }

            return _mappingCodeBuilder.Result;
        }
    }
}