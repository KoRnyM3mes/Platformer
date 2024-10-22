using UnityEngine;
using System.Collections;
public class DestroyOnContact : MonoBehaviour
{
    public float delayBeforeDestruction = 2f; // Time to wait before destroying the object

    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is the player
        if (other.CompareTag("Player"))
        {
            // Start the coroutine to destroy the object after a delay
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delayBeforeDestruction);
        
        // Destroy this game object
        Destroy(gameObject);
    }
}