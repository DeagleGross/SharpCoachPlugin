using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.TypeHelpers
{
    public static class TypeKindHelper
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

            // TODO work on resolving type
            if (type is IArrayType)
            {
                return TypeKind.Collection;
            }

            return null;
        }
    }
}