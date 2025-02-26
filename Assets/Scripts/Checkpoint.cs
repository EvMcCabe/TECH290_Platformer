using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // When the player enters the checkpoint trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.SetRespawnPoint(transform);
                Debug.Log("Checkpoint reached!");
            }
        }
    }
}
