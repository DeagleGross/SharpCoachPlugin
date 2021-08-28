using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public static class TypeKindConverter
    {
        public static TypeKind? GetTypeKind(this IType type)
        {
            if (type.IsSbyte() || type.IsByte() || 
                type.IsShort() || type.IsUshort() || 
                type.IsInt() || type.IsUint() ||
                type.IsLong() || type.IsUlong())
            {
                return TypeKind.Numeric;
            }

            if (type.IsEnumType())
            {
                return TypeKind.Enum;
            }

            if (type.IsString())
            {
                return TypeKind.String;
            }

            if (type.IsClassType())
            {
                return TypeKind.Class;
            }

            if (type.IsStructType())
            {
                return TypeKind.Structure;
            }

            if (type.IsCollectionLike())
            {
                return TypeKind.Collection;
            }

            return null;
        }
    }
}