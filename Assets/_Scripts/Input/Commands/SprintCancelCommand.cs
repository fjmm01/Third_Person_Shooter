using UnityEngine;

public class SprintCancelCommand : IInputCommand
{
    public void Execute(CharacterStateMachine character)
    {
        character.HandleSprintCancel();
    }
}
