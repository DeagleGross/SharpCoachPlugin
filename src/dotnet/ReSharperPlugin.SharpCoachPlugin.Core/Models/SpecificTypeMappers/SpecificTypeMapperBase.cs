using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public abstract  class SpecificTypeMapperBase : ISpecificTypeMapper
    {
        protected readonly MappingCodeBuilder CodeBuilder;

        protected SpecificTypeMapperBase(MappingCodeBuilder codeBuilder)
        {
            CodeBuilder = codeBuilder;
        }
        
        public abstract void MapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType);
    }
}