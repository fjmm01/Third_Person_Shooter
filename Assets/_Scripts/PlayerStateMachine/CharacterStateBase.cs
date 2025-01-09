using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterStateBase : ICharacterState
{
    protected CharacterStateMachine stateMachine;
    protected CharacterController controller;
    protected CharacterData data;
    protected Animator animator;
    protected Transform mainCamera;

    protected Vector2 moveInput;
    protected Vector3 moveDirection;
    protected float currentVelocity;
    protected float targetVelocity;

    public CharacterStateBase(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.controller = stateMachine.Controller;
        this.data = stateMachine.Data;
        this.animator = stateMachine.Animator;
        this.mainCamera = Camera.main.transform;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void HandleInput() { }


    public virtual void OnMovementInput(Vector2 input)
    {
        moveInput = input;

        if (moveInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg
                + Camera.main.transform.eulerAngles.y;
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    public virtual void OnSprintStart() { }
    public virtual void OnSprintCancel() { }
}
