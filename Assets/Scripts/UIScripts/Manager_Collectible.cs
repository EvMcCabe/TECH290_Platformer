using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesManager : MonoBehaviour
{
    public int goldCollected = 0; // Start at 0 and increase when collecting
    public Text goldCollectedDisplay;

    public int totalGoldCollectibles; // Total number of gold coins in the scene
    public Text itemsToCollectDisplay;

    public Health playerHealth; // Reference to the player's Health script

    void Start()
    {
        Collectible.OnCollect += CollectItem;

        CountTotalGoldCollectibles(); // Count gold collectibles in the scene
        DisplayCollectibles(); // Initialize UI
    }

    void CountTotalGoldCollectibles()
    {
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        totalGoldCollectibles = collectibles.Length; // Get the number of gold coins at the start
        Debug.Log("Total collectibles: " + totalGoldCollectibles);
    }

    void DisplayCollectibles()
    {
        if (goldCollectedDisplay == null)
        {
            Debug.LogError("goldCollectedDisplay is not assigned in the Inspector!");
            return;
        }
        goldCollectedDisplay.text = "Gold Collected: " + goldCollected + " / " + totalGoldCollectibles;
    }

    void CollectItem(Collectible.CollectibleType collectibleType)
    {
        Debug.Log(collectibleType.ToString() + " collected!");

        if (collectibleType == Collectible.CollectibleType.GoldCoin)
        {
            goldCollected++; // Increase gold collected count
        }
        else if (collectibleType == Collectible.CollectibleType.HealthPotion)
{
    if (playerHealth != null)
    {
        int healingAmount = 50; // Amount to heal

        // Calculate the amount to heal, ensuring it doesn't exceed the max health
        int healAmount = Mathf.Min(healingAmount, playerHealth.maxHealth - playerHealth.GetCurrentHealth());

        // Heal the player by the calculated amount
        playerHealth.Heal(healAmount);
    }
}

        DisplayCollectibles(); // Update UI
    }

    private void OnDestroy()
    {
        // Unsubscribe from event to avoid errors
        Collectible.OnCollect -= CollectItem;
    }
}
