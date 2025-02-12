using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event System.Action OnCollect; // Event for CollectiblesManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if it's the player
        {
            Debug.Log("Player collected a coin!");

            OnCollect?.Invoke(); // Notify CollectiblesManager
            Destroy(gameObject); // Destroy the collectible
        }
    }
}
