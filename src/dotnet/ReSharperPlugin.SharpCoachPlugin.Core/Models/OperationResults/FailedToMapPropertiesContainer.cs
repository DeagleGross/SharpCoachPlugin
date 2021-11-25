using System.Collections.Generic;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults
{
    public class FailedToMapPropertiesContainer
    {
        public IList<string> FromClassPropertyNames { get; } = new List<string>();
        
        public IList<string> ToClassPropertyNames { get; } = new List<string>();

        public void Add(FailedToMapPropertiesContainer container)
        {
            if (container is null) return;
            
            foreach (var fromClassFailedProperty in container.FromClassPropertyNames)
            {
                FromClassPropertyNames.Add(fromClassFailedProperty);
            }
            
            foreach (var toClassFailedProperty in container.ToClassPropertyNames)
            {
                ToClassPropertyNames.Add(toClassFailedProperty);
            }
        }
    }
}