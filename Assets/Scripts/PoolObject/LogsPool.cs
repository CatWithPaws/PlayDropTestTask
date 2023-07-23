using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsPool : PoolObject<Log>
{
    [SerializeField] private List<Log> initArray;
    [SerializeField] private Transform unusedPoolPosition;

    private void Start()
    {

        foreach (Log obj in initArray)
        {
            AddItem(obj);
        }

        print(objectList.Count);

    }

    protected override void OnPickItem(Log item)
    {
        item.Rigidbody.isKinematic = false;
    }

    protected override void OnAddItem(Log item)
    {
        item.Rigidbody.velocity = Vector3.zero;
        item.Transform.position = unusedPoolPosition.position;
        item.Rigidbody.isKinematic = true;
    }
}
