using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.SpecificTypeMappers
{
    public interface ISpecificTypeMapper
    {
        void MapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType);
    }
}