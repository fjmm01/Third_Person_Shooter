using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public CharacterController Controller {  get; private set; }
    public CharacterData Data {  get; private set; }
    public Animator Animator { get; private set; }

    private Dictionary<Type, ICharacterState> states;
    private ICharacterState currentState;

    private Vector2 currentMovementInput;
    private bool isSprintPressed;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();

        InitializeStates();
    }

    private void InitializeStates()
    {
        states = new Dictionary<Type, ICharacterState>()
        {
            { typeof(IdleState), new IdleState(this) },
            { typeof(WalkingState), new WalkingState(this) },
            { typeof(SprintingState), new SprintingState(this) },
            { typeof(CrouchingState), new CrouchingState(this) },
            { typeof(CrouchWalkingState), new CrouchWalkingState(this) },
            { typeof(AimingState), new AimingState(this) },
            { typeof(AimWalkingState), new AimWalkingState(this) },
            { typeof(InteractingState), new InteractingState(this) },
            { typeof(StunnedState), new StunnedState(this) },
            { typeof(HitState), new HitState(this) }
        };


        ChangeState<IdleState>();
    }

    public void ChangeState<T>() where T:ICharacterState
    {
        if(currentState != null) currentState.Exit();

        currentState = states[typeof(T)];
        currentState.Enter();

    }

    private void Update()
    {
        currentState?.HandleInput();
        currentState?.Update();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void HandleMovementInput(Vector2 movementValue)
    {
        currentMovementInput = movementValue;
        // Notificar al estado actual del cambio en el input
        (currentState as CharacterStateBase)?.OnMovementInput(movementValue);
    }

    public void HandleSprintStart()
    {
        isSprintPressed = true;
        // Notificar al estado actual
        (currentState as CharacterStateBase)?.OnSprintStart();
    }

    public void HandleSprintCancel()
    {
        isSprintPressed = false;
        // Notificar al estado actual
        (currentState as CharacterStateBase)?.OnSprintCancel();
    }
}
