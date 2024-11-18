using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterPattern characterPattern;  // Reference to the CharacterPattern ScriptableObject
    private CharacterController characterController;

    private void Awake()
    {
        // Get references to components
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Move the character using the CharacterPattern system
        if (characterPattern != null)
        {
            characterPattern.Move(characterController);
        }
    }
}