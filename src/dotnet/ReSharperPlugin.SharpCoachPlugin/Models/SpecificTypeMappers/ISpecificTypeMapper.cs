using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;

namespace ReSharperPlugin.SharpCoachPlugin.Models
{
    public interface ISpecificTypeMapper
    {
        void MapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType);
    }
}