using System;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public static class SpecificTypeMapperFactory
    {
        public static ISpecificTypeMapper CreateSpecificMapper(MappingCodeBuilder codeBuilder, TypeKind fromType) => fromType switch
        {
            TypeKind.Numeric => new NumericTypeMapper(codeBuilder),
            TypeKind.Enum => new EnumTypeMapper(codeBuilder),
            TypeKind.String => new StringTypeMapper(codeBuilder),
            TypeKind.Class => new ClassTypeMapper(codeBuilder),
            TypeKind.Structure => null,
            TypeKind.Collection => new CollectionTypeMapper(codeBuilder),
            _ => throw new ArgumentOutOfRangeException(nameof(fromType), fromType, null)
        };
    }
}