using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public class ClassTypeInfoProvider
    {
        private readonly IClass _classInfo;

        public string VariableName { get; }

        public bool HasValidModelInfo => _classInfo is not null;

        public string FullClassTypeName
        {
            get
            {
                if (_classInfo is null)
                {
                    return string.Empty;
                }

                return _classInfo.ContainingType is not null 
                    ? $"{_classInfo.ContainingType.ShortName}.{_classInfo.ShortName}" 
                    : _classInfo.ShortName;
            }   
        }

        public ClassTypeInfoProvider(IClass classInfo, string variableName = null)
        {
            _classInfo = classInfo;
            VariableName = !string.IsNullOrEmpty(variableName) ? variableName : string.Empty;
        }

        public IReadOnlyDictionary<string, IProperty> GetPropertyNameInfoMap(AccessRights accessRights = AccessRights.PUBLIC) 
            => _classInfo.Properties
            .Where(x => x.GetAccessRights() == accessRights)
            .ToDictionary(property => property.ShortName, property => property);
    }
}