using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> : MonoBehaviour
{
    protected Queue<T> objectList = new Queue<T>();

    public int PoolSize => objectList.Count;

    public void AddItem(T item)
    {
        if (!objectList.Contains(item))
        {
            OnAddItem(item);
            objectList.Enqueue(item);
        }
       
    }

    protected virtual void OnPickItem(T item)
    {

    }

    protected virtual void OnAddItem(T item)
    {

    }

    public T PickAvailableItem()
    {
        if (objectList.Count > 0) 
        {
            var item = objectList.Dequeue();
            OnPickItem(item);
            return item;
        }
        else
        {
            throw new System.Exception("Pool is Empty");
        }
    }
}

