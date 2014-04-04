using System;
using System.Collections.Generic;
using System.Linq;

namespace DtoGen.Core.Collections
{
    public abstract class CollectionHandlerBase<T1, T2> : ICollectionHandler<T1, T2>
        where T1 : class
        where T2 : class
    {
        public Func<T1, T2> TransformFunction { get; set; }

        public CollectionHandlerBase()
        {
            TransformFunction = PropertyConverter.Convert<T1, T2>;
        }

        public abstract void SynchronizeCollections(IEnumerable<T1> source, ICollection<T2> target);

    }

}