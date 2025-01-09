using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterinputHandler : MonoBehaviour
{
    public PlayerInput playerInput;
    private CharacterStateMachine character;
    public InputActionMap gamePlayActions;

    //Cola de comandos para procesar
    private Queue<IInputCommand> inputCommands = new Queue<IInputCommand>();

    private void Awake()
    {
        character = GetComponent<CharacterStateMachine>();
        playerInput = GetComponent<PlayerInput>();

        //Obtener el action map del gameplay
        gamePlayActions = playerInput.actions.FindActionMap("Gameplay");

        //Configurar los callbacks para las acciones
        SetUpInputCallbacks();
    }

    private void SetUpInputCallbacks()
    {
        //Movimiento
        var moveAction = gamePlayActions.FindAction("Move");
        moveAction.performed += ctx => EnqueueCommand(new MovementInputCommand(ctx.ReadValue<Vector2>()));
        moveAction.canceled += ctx => EnqueueCommand(new MovementInputCommand(Vector2.zero));

        // Sprint
        var sprintAction = gamePlayActions.FindAction("Sprint");
        sprintAction.performed += ctx => EnqueueCommand(new SprintStartCommand());
        sprintAction.canceled += ctx => EnqueueCommand(new SprintCancelCommand());
    }

    private void EnqueueCommand(IInputCommand command)
    {
        inputCommands.Enqueue(command);
    }

    private void Update()
    {
        //Procesar todos los comandos acumulados en este frame
        while (inputCommands.Count > 0)
        {
            var command = inputCommands.Dequeue();
            command.Execute(character);
        }
    }

    private void OnEnable()
    {
        gamePlayActions.Enable();
    }

    private void OnDisable()
    {
        gamePlayActions.Disable();
    }
}
