using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Models.Types;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public static class NumericTypeConverter
    {
        public static string GetNumericTypeStringRepresentation(this NumericType numericType) => numericType.ToString().ToLower();

        public static NumericType? ToNumeric(this IType type)
        {
            if (type.IsSbyte()) return NumericType.Sbyte;
            if (type.IsByte()) return NumericType.Byte;
            if (type.IsShort()) return NumericType.Short;
            if (type.IsUshort()) return NumericType.Ushort;
            if (type.IsInt()) return NumericType.Int;
            if (type.IsUint()) return NumericType.Uint;
            if (type.IsLong()) return NumericType.Long;
            if (type.IsUlong()) return NumericType.ULong;

            return null;
        }
    }
}