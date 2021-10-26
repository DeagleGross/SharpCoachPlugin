using System;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers
{
    public static class CollectionTypeHelper
    {
        [CanBeNull]
        public static IType GetUnderlyingType(this IProperty property, CollectionType? propertyCollectionType)
        {
            if (propertyCollectionType is null) return null;
            
            return propertyCollectionType switch
            {
                CollectionType.Array => (property.ReturnType as IArrayType)?.ElementType,
                CollectionType.List => null,
                CollectionType.Enumerable => null,
                _ => throw new ArgumentOutOfRangeException(nameof(propertyCollectionType), propertyCollectionType, null)
            };
        }
        
        public static CollectionType? ToCollectionType(this IType type)
        {
            // TODO resolve collection type correctly
            if (type is IArrayType) return CollectionType.Array;

            return null;
        }

        public static bool SupportsLinq(this CollectionType? collectionType)
        {
            if (collectionType is null) return false;

            return collectionType.Value switch
            {
                CollectionType.Array => true,
                CollectionType.List => true,
                CollectionType.Enumerable => true,
                _ => false
            };
        }

        public static string GetToCollectionLinqCast(this CollectionType collectionType)
        {
            return collectionType switch
            {
                CollectionType.Array => ".ToArray()",
                CollectionType.List => ".ToList()",
                CollectionType.Enumerable => string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(collectionType), collectionType, null)
            };
        }
    }
}