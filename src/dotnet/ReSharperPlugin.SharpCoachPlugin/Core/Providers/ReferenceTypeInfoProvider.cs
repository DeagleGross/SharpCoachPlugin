using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public class ReferenceTypeInfoProvider
    {
        private readonly IClass _classInfo;
        
        public string VariableName { get; }

        public bool HasValidModelInfo => _classInfo is not null;

        public string ClassTypeName => _classInfo.ShortName;

        public ReferenceTypeInfoProvider(IClass classInfo, string variableName = null)
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