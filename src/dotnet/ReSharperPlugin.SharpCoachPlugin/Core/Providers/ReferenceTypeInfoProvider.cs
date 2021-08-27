using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

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

        public IReadOnlyDictionary<PropertyDescriptor, IProperty> GetPropertiesSet(AccessRights accessRights = AccessRights.PUBLIC) 
            => _classInfo.Properties
            .Where(x => x.GetAccessRights() == accessRights)
            .ToDictionary(property => new PropertyDescriptor(property.Type, property.ShortName), property => property);
    }
}