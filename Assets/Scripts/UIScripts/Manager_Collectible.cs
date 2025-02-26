using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add for scene management
using TMPro;


public class CollectiblesManager : MonoBehaviour
{
    public int goldCollected = 0;
    public Text goldCollectedDisplay;

    public int totalGoldCollectibles;
    public TextMeshProUGUI rocketPiecesDisplay;


    public Health playerHealth;

    private int totalKeyCubes = 0;
    private int keyCubesCollected = 0;

    private int totalRocketPieces = 4;
    private int rocketPiecesCollected = 0;

    public GameObject gate; // Assign this in the Unity Inspector

    void Start()
    {
        Collectible.OnCollect += CollectItem;

        CountCollectibles();
        DisplayCollectibles();
    }

    void CountCollectibles()
    {
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        foreach (GameObject collectible in collectibles)
        {
            Collectible collectibleScript = collectible.GetComponent<Collectible>();
            if (collectibleScript != null)
            {
                switch (collectibleScript.collectibleType)
                {
                    case Collectible.CollectibleType.GoldCoin:
                        totalGoldCollectibles++;
                        break;
                    case Collectible.CollectibleType.KeyCube:
                        totalKeyCubes++;
                        break;
                    case Collectible.CollectibleType.ImportantItem:
                        if (collectible.CompareTag("Item")) // Ensure it's a Rocket Piece
                        {
                            totalRocketPieces++;
                        }
                        break;
                }
            }
        }

        Debug.Log("Total Gold Coins: " + totalGoldCollectibles);
        Debug.Log("Total KeyCubes: " + totalKeyCubes);
        Debug.Log("Total Rocket Pieces: " + totalRocketPieces);
    }

    void DisplayCollectibles()
{
    if (goldCollectedDisplay != null)
    {
        goldCollectedDisplay.text = "Gold: " + goldCollected + " / " + totalGoldCollectibles;
    }

    if (rocketPiecesDisplay != null)
    {
        rocketPiecesDisplay.text = "Rocket Pieces: " + rocketPiecesCollected + "/" + totalRocketPieces;
    }
}



    void CollectItem(Collectible.CollectibleType collectibleType)
    {
        Debug.Log(collectibleType.ToString() + " collected!");

        switch (collectibleType)
        {
            case Collectible.CollectibleType.GoldCoin:
                goldCollected++;
                break;

            case Collectible.CollectibleType.KeyCube:
                keyCubesCollected++;
                CheckGateUnlock();
                break;

            case Collectible.CollectibleType.ImportantItem:
                rocketPiecesCollected++;
                CheckRocketPieceCollection();
                break;

            case Collectible.CollectibleType.HealthPotion:
                if (playerHealth != null)
                {
                    int healingAmount = 50;
                    int healAmount = Mathf.Min(healingAmount, playerHealth.maxHealth - playerHealth.GetCurrentHealth());
                    playerHealth.Heal(healAmount);
                }
                break;
        }

        DisplayCollectibles();
    }

    void CheckGateUnlock()
    {
        if (keyCubesCollected >= totalKeyCubes)
        {
            UnlockGate();
        }
    }

    void CheckRocketPieceCollection()
    {
        if (rocketPiecesCollected >= totalRocketPieces)
        {
            LoadWinScene();
        }
    }

    void UnlockGate()
    {
        if (gate != null)
        {
            Collider gateCollider = gate.GetComponent<Collider>();
            if (gateCollider != null)
            {
                Destroy(gateCollider);
                Destroy(gate);
                Debug.Log("Gate unlocked! Collider removed.");
            }
        }
        else
        {
            Debug.LogError("Gate GameObject is not assigned in the Inspector!");
        }
    }

    void LoadWinScene()
    {
        // Load the win scene (make sure you have a scene named "WinScene" in your project)
        Debug.Log("All Rocket Pieces collected! Loading Win Scene...");
        SceneManager.LoadScene("WinScene"); // Replace "WinScene" with the name of your actual win scene
    }

    private void OnDestroy()
    {
        Collectible.OnCollect -= CollectItem;
    }
}
