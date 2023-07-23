using UnityEngine;

public class ChopArea : InteractionArea
{

    [SerializeField] private LogsPool Logs;
    private float spawnRange = 0.2f;

    private void Awake()
    {
        objectCooldownAfterDestroying = 15f;
    }

    protected override void DropLoot()
    {
        Log log = Logs.PickAvailableItem();

        log.transform.position = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange),0,Random.Range(-spawnRange, spawnRange)).normalized;
        log.transform.rotation = Quaternion.Euler(0, Random.Range(-180f, 180f), log.transform.rotation.eulerAngles.z);
        Vector3 directionFromChopArea = log.transform.position - transform.position;
        log.Rigidbody.velocity = directionFromChopArea.normalized * spawnRange;
    }

}
