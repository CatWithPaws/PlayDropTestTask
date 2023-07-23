using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Animator animator;

    [SerializeField] private PlayerState state;

    private PlayerState State
    {
        get
        {
            return state; 
        }
        set
        {
            if (state != value)
            {
                state = value;
                ChangeState[(int)value]();
            }
        }
    }

    [SerializeField] private float speed;

    private InteractionArea currentInteractionArea;

    public InteractionArea CurrentInteractionArea { 
        get 
        { 
            return currentInteractionArea;
        } 
        set 
        { 
            if(value == null)
            {
                ChangeState[(int)PlayerState.Idle]();
                currentInteractionArea = value;
            }
            currentInteractionArea = value;
        } 
    }


    private Constants.VoidDelegate[] ChangeState = new Constants.VoidDelegate[(int)PlayerState.Count];

    private Vector2 prevInput = Vector2.zero;

   

    private void Start()
    {
        state = PlayerState.Idle;
        InitSwitchDelegate();
        joystick.OnJoystickRelease += OnJoystickRelease;
    }

    private void OnJoystickRelease()
    {
        if (currentInteractionArea != null)
        {
            State = PlayerState.Chop;
        }
        else
        {
            State = PlayerState.Idle;
        }
    }

    private void InitSwitchDelegate()
    {
        ChangeState[(int)PlayerState.Idle] = ChangeToIdleState;
        ChangeState[(int)PlayerState.Run] = ChangeToRunState;
        ChangeState[(int)PlayerState.Chop] = ChangeToChopState;
    }


    private void FixedUpdate()
    {
        print(currentInteractionArea == null);
        rb.velocity = new Vector3(joystick.Direction.x * speed, rb.velocity.y, joystick.Direction.y * speed);

       
        if (joystick.Direction != Vector2.zero)
        {
            State = PlayerState.Run;
            rb.rotation = Quaternion.LookRotation(new Vector3(joystick.Direction.x, 0, joystick.Direction.y), Vector3.up);
        }
        else if (currentInteractionArea != null)
        {
            State = PlayerState.Chop;
        }
        else
        {
            State = PlayerState.Idle;
        }
        prevInput = joystick.Direction;

    }


    private void ChangeToIdleState()
    {
        state = PlayerState.Idle;
        animator.Play("Idle");
    }

    private void ChangeToRunState()
    {
        state = PlayerState.Run;
        animator.Play("Run");
    }

    private void ChangeToChopState()
    {
        state = PlayerState.Chop;
        animator.Play("Chop");
    }

    public void InvokeDestroy()
    {
        bool isDestroyed = currentInteractionArea.DoAction();
        if (isDestroyed)
        {
            currentInteractionArea = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Log")
        {
            Log log = collision.gameObject.GetComponent<Log>();
            log.sourcePool.AddItem(log);
            PickALog();
        }
    }

    private void PickALog()
    {
        GameData.Instance.LogCount++;
    }

}

public enum PlayerState
{
    Idle,Run, Chop,Count
}