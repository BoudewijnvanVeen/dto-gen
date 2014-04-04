using System;
using System.Collections.Generic;
using System.Linq;

namespace DtoGen.Core.Collections
{
    public interface ICollectionHandler<T1, T2>
        where T1 : class
        where T2 : class
    {

        Func<T1, T2> TransformFunction { get; set; } 

        void SynchronizeCollections(IEnumerable<T1> source, ICollection<T2> target);

    }
}