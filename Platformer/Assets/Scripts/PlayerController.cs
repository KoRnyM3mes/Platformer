using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterPattern characterPattern;  // Reference to the CharacterPattern ScriptableObject
    private CharacterController characterController;

    private void Awake()
    {
        // Get the CharacterController component attached to the player
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Call the Move method from the ScriptableObject (CharacterMoveAndRotate3d)
        if (characterPattern != null)
        {
            characterPattern.Move(characterController);
        }
    }
}