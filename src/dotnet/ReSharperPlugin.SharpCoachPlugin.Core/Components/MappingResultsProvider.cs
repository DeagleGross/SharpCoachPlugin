using System.Collections.Generic;
using JetBrains.Application;
using ReSharperPlugin.SharpCoachPlugin.Core.Models;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Components
{
    [ShellComponent]
    public class MappingResultsProvider : IMappingResultsStorage
    {
        // ReSharper disable once InconsistentNaming
        private const int MAX_SIZE = 5;

        private readonly LinkedList<MappingOperationResult> _items = new();

        public void Add(MappingOperationResult item)
        {
            if (_items.Count == MAX_SIZE) _items.RemoveFirst();
            _items.AddLast(item);
        }

        public IEnumerator<MappingOperationResult> GetEnumerator()
        {
            var currentNode = _items.Last;
            if (currentNode is null) yield break;

            while (currentNode is not null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Previous;
            }
        }
    }
}