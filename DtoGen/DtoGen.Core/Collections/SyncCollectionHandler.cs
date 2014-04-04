using System;
using System.Collections.Generic;
using System.Linq;

namespace DtoGen.Core.Collections
{

    /// <summary>
    /// Adds new items, updates existing and removes missing entries in a collection.
    /// </summary>
    public class SyncCollectionHandler<T1, T2> : CollectionHandlerBase<T1, T2>
        where T1 : class
        where T2 : class
    {

        public Func<T1, T2> NewItemFactory { get; set; }

        public Action<T1, T2> UpdateItemAction { get; set; }

        public Action<T2, ICollection<T2>> RemoveItemAction { get; set; }

        public Func<T1, object> KeySelector1 { get; set; }

        public Func<T2, object> KeySelector2 { get; set; }



        public SyncCollectionHandler()
        {
            RemoveItemAction = (item, collection) => collection.Remove(item);
        }


        public override void SynchronizeCollections(IEnumerable<T1> source, ICollection<T2> target)
        {
            var remainingItems = new HashSet<T2>(target);

            foreach (var sourceItem in source)
            {
                var sourceKey = KeySelector1(sourceItem);
                var targetItem = target.FirstOrDefault(t => Equals(sourceKey, KeySelector2(t)));
                if (targetItem == null)
                {
                    // the item is new
                    var newItem = NewItemFactory(sourceItem);
                    target.Add(newItem);
                }
                else
                {
                    // the item was updated
                    UpdateItemAction(sourceItem, targetItem);
                }
            }

            foreach (var item in remainingItems)
            {
                // delete from target because they are not in the source
                RemoveItemAction(item, target);
            }
        }



    }
}