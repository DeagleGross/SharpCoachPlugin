using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public interface ISpecificTypeMapper
    {
        FailedToMapPropertiesContainer InternalFailedPropertiesContainer { get; }
        
        bool TryMapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType);
    }
}