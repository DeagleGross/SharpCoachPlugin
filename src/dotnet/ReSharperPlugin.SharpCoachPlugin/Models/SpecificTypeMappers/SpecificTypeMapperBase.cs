using DefaultNamespace;
using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Models;
using ReSharperPlugin.SharpCoachPlugin.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
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