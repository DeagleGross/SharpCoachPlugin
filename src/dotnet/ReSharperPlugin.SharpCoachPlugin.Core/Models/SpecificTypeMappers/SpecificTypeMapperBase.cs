using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Builders;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public abstract  class SpecificTypeMapperBase : ISpecificTypeMapper
    {
        protected readonly MappingCodeBuilder CodeBuilder;
        
        public FailedToMapPropertiesContainer InternalFailedPropertiesContainer { get; }

        protected SpecificTypeMapperBase(MappingCodeBuilder codeBuilder)
        {
            CodeBuilder = codeBuilder;
            InternalFailedPropertiesContainer = new();
        }
        
        public abstract bool TryMapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType);
    }
}