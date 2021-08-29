using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public static class CollectionTypeConverter
    {
        public static CollectionType? ToCollectionType(this IType type)
        {
            if (type.IsArray()) return CollectionType.Array;
            if (type.IsIList()) return CollectionType.List;

            if (type.IsIEnumerable()) return CollectionType.Enumerable;
            
            return null;
        }
    }
}