using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Providers
{
    public class ClassInfoProvider
    {
        private readonly IClass _classInfo;

        public bool HasValidModelInfo => _classInfo is not null;

        public string ClassTypeName => _classInfo.ShortName;
        
        public ClassInfoProvider(IClass classInfo)
        {
            _classInfo = classInfo;
        }

        public IReadOnlyDictionary<PropertyDescriptor, IProperty> GetPropertiesSet(AccessRights accessRights = AccessRights.PUBLIC) 
            => _classInfo.Properties
            .Where(x => x.GetAccessRights() == accessRights)
            .ToDictionary(property => new PropertyDescriptor(property.Type, property.ShortName), property => property);
    }
}