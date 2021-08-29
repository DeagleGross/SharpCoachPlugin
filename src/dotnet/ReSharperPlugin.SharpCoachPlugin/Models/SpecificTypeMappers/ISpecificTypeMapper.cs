using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Models
{
    public interface ISpecificTypeMapper
    {
        void MapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType);
    }
}