using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesScript : MonoBehaviour
{
    public int goldToCollect;
    public Text goldToCollectDisplay;

    void Start()
    {
        Collectible.OnCollect += CollectCoin;

        addGoldCoin();
        DisplayCollectibles();
    }

    void addGoldCoin()
    {
        GameObject[] goldCoins = GameObject.FindGameObjectsWithTag("Collectible");
        goldToCollect = goldCoins.Length;
        Debug.Log(goldToCollect);
    }

    void DisplayCollectibles()
    {
        if (goldToCollectDisplay == null)
        {
            Debug.LogError("goldToCollectDisplay is not assigned in the Inspector!");
            return;
        }
        goldToCollectDisplay.text = "Gold Coins to Collect: " + goldToCollect;
    }

    void CollectCoin()
    {
        Debug.Log("Coin collected! Reducing count...");
        if (goldToCollect != 0)
        {
            goldToCollect--; // Reduce count
        }
        DisplayCollectibles(); // Update UI
    }

    private void OnDestroy()
    {
        // Unsubscribe from event to avoid errors
        Collectible.OnCollect -= CollectCoin;
    }
}
