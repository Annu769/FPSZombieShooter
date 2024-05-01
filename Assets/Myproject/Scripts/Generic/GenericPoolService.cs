using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FPSZombie.Generic
{
    public class GenericPoolService<T> :NonMonoGenericSingleton<GenericPoolService<T>> where T : class
    {
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        public virtual T GetItem()
        {
            if (pooledItems.Count > 0)
            { 
            PooledItem<T> poolItem = pooledItems.Find(newItem => newItem.isused == false);
            if(poolItem != null)
            {
                poolItem.isused = true;
                return poolItem.item;
            }
            }
            PooledItem<T> newPooledItem = new PooledItem<T>();
            newPooledItem.item = CreateItem();
            pooledItems.Add(newPooledItem);
            return newPooledItem.item;
           
        }
        public virtual void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(newItem => newItem.item == item);
            if (pooledItem != null)
                pooledItem.isused = false;
        }
        protected virtual T CreateItem()
        {
            return (T)null;
        }
    }
    public class PooledItem<T>
    {
        public T item;
        public bool isused;
    }
}
