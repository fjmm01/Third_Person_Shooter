using UnityEngine;

public class SprintStartCommand : IInputCommand
{
    public void Execute(CharacterStateMachine character)
    {
        character.HandleSprintStart();
    }
}
