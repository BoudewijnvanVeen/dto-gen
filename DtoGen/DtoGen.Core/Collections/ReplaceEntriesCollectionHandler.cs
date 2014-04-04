using System;
using System.Collections.Generic;
using System.Linq;

namespace DtoGen.Core.Collections
{
    /// <summary>
    /// Cleans the target collection and adds the items from the source.
    /// </summary>
    public class ReplaceEntriesCollectionHandler<T1, T2> : CollectionHandlerBase<T1, T2>
        where T1 : class
        where T2 : class
    {
        public override void SynchronizeCollections(IEnumerable<T1> source, ICollection<T2> target)
        {
            target.Clear();

            foreach (var item in source)
            {
                target.Add(TransformFunction(item));
            }
        }
    }
}