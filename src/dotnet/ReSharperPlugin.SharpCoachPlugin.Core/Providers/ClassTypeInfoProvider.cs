using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public class ClassTypeInfoProvider
    {
        /// <summary>
        /// Originally used type expression in user code
        /// </summary>
        private readonly string _typeUsage;
        private readonly IClass _classInfo;

        public string TypeUsage => !string.IsNullOrEmpty(_typeUsage) ? _typeUsage : FullClassTypeName;
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

        public ClassTypeInfoProvider(
            IClass classInfo,
            string variableName = null,
            string typeUsage = null)
        {
            _classInfo = classInfo;
            _typeUsage = typeUsage;
            VariableName = !string.IsNullOrEmpty(variableName) ? variableName : string.Empty;
        }

        public IReadOnlyDictionary<string, IProperty> GetPropertyNameInfoMap(AccessRights accessRights = AccessRights.PUBLIC) 
            => _classInfo.Properties
            .Where(x => x.GetAccessRights() == accessRights)
            .ToDictionary(property => property.ShortName, property => property);
    }
}