using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { GoldCoin, HealthPotion, ImportantItem }
    public CollectibleType collectibleType; // Type of the collectible
    public static event System.Action<CollectibleType> OnCollect; // Event that sends the collectible type

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if it's the player
        {
            Debug.Log("Player collected a " + collectibleType.ToString());

            OnCollect?.Invoke(collectibleType); // Notify the manager with the type
            Destroy(gameObject); // Destroy the collectible
        }
    }
}
