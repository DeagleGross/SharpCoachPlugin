using System.Collections;
using System.Collections.Generic;
using JetBrains.Application;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Components
{
    [ShellComponent]
    public class MappingResultsStorage : IEnumerable<MappingOperationResult>
    {
        // ReSharper disable once InconsistentNaming
        private const int MAX_SIZE = 5;

        private readonly LinkedList<MappingOperationResult> _items = new();

        public void Add(MappingOperationResult item)
        {
            // last is new
            // first is the oldest one
            
            if (_items.Count == MAX_SIZE) _items.RemoveFirst();
            _items.AddLast(item);
        }
        
        public IEnumerator<MappingOperationResult> GetEnumerator()
        {
            if (_items.IsNullOrEmpty()) yield break;
            
            var lastNode = _items.Last;
            if (lastNode is null) yield break;

            while (lastNode is not null)
            {
                yield return lastNode.Value;
                lastNode = lastNode.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}