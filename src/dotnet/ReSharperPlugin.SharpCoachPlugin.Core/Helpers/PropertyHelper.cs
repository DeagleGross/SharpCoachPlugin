using System.Collections.Generic;
using System.Text;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Helpers
{
    public static class PropertyHelper
    {
        public static string GetFullName(this IProperty property)
        {
            if (property is null) return string.Empty;
            
            // building full containing types tree
            var containingType = property.GetContainingType();
            var typeNamesTree = new List<string>();
            while (containingType is not null)
            {
                typeNamesTree.Add(containingType.ShortName);
                containingType = containingType.GetContainingType();
            }
            
            // going from the most general class name to the most specific
            var fullNameBuilder = new StringBuilder();
            for (var i = typeNamesTree.Count - 1; i >= 0; i--)
            {
                fullNameBuilder.Append($"{typeNamesTree[i]}.");
            }
                
            fullNameBuilder.Append(property.ShortName);
            return fullNameBuilder.ToString();
        } 
    }
}