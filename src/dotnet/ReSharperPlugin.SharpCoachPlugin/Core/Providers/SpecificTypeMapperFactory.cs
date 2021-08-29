using System;
using DefaultNamespace;
using ReSharperPlugin.SharpCoachPlugin.Models;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public static class SpecificTypeMapperFactory
    {
        public static ISpecificTypeMapper Create(MappingCodeBuilder codeBuilder, TypeKind fromType) => fromType switch
        {
            TypeKind.Numeric => new NumericTypeMapper(codeBuilder),
            TypeKind.Enum => new EnumTypeMapper(codeBuilder),
            TypeKind.String => new StringTypeMapper(codeBuilder),
            TypeKind.Class => new ClassTypeMapper(codeBuilder),
            TypeKind.Structure => null,
            TypeKind.Collection => null,
            _ => throw new ArgumentOutOfRangeException(nameof(fromType), fromType, null)
        };
    }
}