using System;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults
{
    public class MappingOperationResult
    {
        public DateTime OperationDate { get; set; }
        
        public string InputClassName { get; set; }
        
        public string OutputClassName { get; set; }

        public FailedToMapPropertiesContainer FailedToMapPropertiesContainer { get; set; } = new();
    }
}