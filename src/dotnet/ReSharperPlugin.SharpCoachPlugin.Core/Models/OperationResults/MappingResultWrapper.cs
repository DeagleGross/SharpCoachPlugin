using System;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults
{
    public class MappingResultWrapper
    {
        public MappingOperationResult MappingOperationResult { get; } = new();

        public FailedToMapPropertiesContainer FailedToMapPropertiesContainer => MappingOperationResult.FailedToMapPropertiesContainer;
        
        public bool IsSuccessful
        {
            get
            {
                var hasErrorsInInput = !MappingOperationResult.FailedToMapPropertiesContainer.FromClassPropertyNames.IsNullOrEmpty();
                var hasErrorsInOutput = !MappingOperationResult.FailedToMapPropertiesContainer.ToClassPropertyNames.IsNullOrEmpty();

                return !(hasErrorsInInput || hasErrorsInOutput);
            }
        }

        public MappingResultWrapper(ClassTypeInfoProvider fromClassType, ClassTypeInfoProvider toClassType)
        {
            MappingOperationResult.InputClassName = fromClassType.FullClassTypeName;
            MappingOperationResult.OutputClassName = toClassType.FullClassTypeName;
            MappingOperationResult.OperationDate = DateTime.Now;
        }

        public void AddFailedToMapInputProperty(string inputClassPropertyName)
        {
            MappingOperationResult.FailedToMapPropertiesContainer.FromClassPropertyNames.Add(inputClassPropertyName);
        }
        
        public void AddFailedToMapOutputProperty(string outputClassPropertyName)
        {
            MappingOperationResult.FailedToMapPropertiesContainer.ToClassPropertyNames.Add(outputClassPropertyName);
        }
    }
}