using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 3f;
    public float aimWalkSpeed = 4f;
    public float rotationSpeed = 10f;
    public float stunDuration = 2f;
    public float hitRecoveryTime = 0.5f;
}
