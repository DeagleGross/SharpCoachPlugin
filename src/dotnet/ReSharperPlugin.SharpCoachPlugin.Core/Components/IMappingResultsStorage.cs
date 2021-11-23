using System.Collections.Generic;
using ReSharperPlugin.SharpCoachPlugin.Core.Models;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Components
{
    public interface IMappingResultsStorage
    {
        void Add(MappingOperationResult item);

        IEnumerator<MappingOperationResult> GetEnumerator();
    }
}