using System.Collections;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    [SerializeField] protected int actionsToDestroy = 3;
    [SerializeField] protected int doneActions = 0;
    [SerializeField] protected GameObject objectToAct;
    [SerializeField] protected Collider actionTrigger;
    [SerializeField] protected float objectCooldownAfterDestroying;

    protected bool canAct => objectToAct.activeSelf == true;

    protected void OnTriggerEnter(Collider other)
    {
        if (canAct && other.gameObject.TryGetComponent(out Player player))
        {
            player.CurrentInteractionArea = this;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (canAct && other.gameObject.TryGetComponent(out Player player))
        {
            if(player.CurrentInteractionArea == this) 
            { 
                player.CurrentInteractionArea = null;
            }
        }
    }

    public bool DoAction()
    {
        if (canAct)
        {
            
            doneActions++;
            DropLoot();
            if (doneActions >= actionsToDestroy)
            {
                StartCoroutine(CoolDownObject());
                return true;
            }
            return false;
        }
        return false;
    }

    protected IEnumerator CoolDownObject()
    {
        actionTrigger.enabled = false;
        objectToAct.SetActive(false);
        
        yield return new WaitForSeconds(objectCooldownAfterDestroying);
        doneActions = 0;
        objectToAct.SetActive(true);
        actionTrigger.enabled=true;
    }

    protected virtual void DropLoot()
    {

    }

}
