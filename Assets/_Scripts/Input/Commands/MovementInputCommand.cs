using UnityEngine;

public class MovementInputCommand : IInputCommand
{
    private Vector2 movementValue;

    public MovementInputCommand(Vector2 value)
    {
        movementValue = value;
    }
    public void Execute(CharacterStateMachine character)
    {
        character.HandleMovementInput(movementValue);
    }
}
