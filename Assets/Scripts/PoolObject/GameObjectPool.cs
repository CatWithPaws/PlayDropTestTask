using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : PoolObject<GameObject>
{

    [SerializeField] private GameObject[] InitArray;

    private void Awake()
    {
        if(InitArray != null && InitArray.Length > 0)
        {
            foreach(GameObject obj in InitArray)
            {
                AddItem(obj);
            }
        }
    }

}
