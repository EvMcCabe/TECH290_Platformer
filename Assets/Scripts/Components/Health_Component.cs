using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public event Action<int> OnHealthChanged; // Event triggered on health change
    public event Action OnDeath; // Event triggered on death

    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject respawnMenu; // Assign in Inspector

    public Transform respawnPoint; 

    private GameObject playerCameraArm; // The player's Camera Arm (parent of camera)
    private GameObject deathCamera; // The separate death camera

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); 

        // Find the player camera arm (so we don't touch the camera directly)
        playerCameraArm = GameObject.FindGameObjectWithTag("CameraArm"); 
        if (playerCameraArm == null)
        {
            Debug.LogError("Camera Arm not found! Make sure it's tagged as 'CameraArm'.");
        }

        // Find the Death Camera
        deathCamera = GameObject.Find("DeathCamera");
        if (deathCamera == null)
        {
            Debug.LogError("Death Camera not found! Make sure it's named 'DeathCamera'.");
        }
        else
        {
            deathCamera.SetActive(false); // Ensure it's disabled at start
        }
    }

    public void Damage(int amount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnHealthChanged?.Invoke(currentHealth);
            UpdateHealthUI();

            if (currentHealth == 0)
            {
                Die();
            }
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            OnHealthChanged?.Invoke(currentHealth);
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        OnDeath?.Invoke();

        if (gameObject.CompareTag("Player"))
        {
            if (respawnMenu != null)
            {
                respawnMenu.SetActive(true); // Show respawn UI
            }
            else
            {
                Debug.LogWarning("Respawn menu not assigned in Health component!");
            }

            // Disable the player instead of destroying it
            gameObject.SetActive(false);

            // Disable the player's camera arm
            if (playerCameraArm != null)
            {
                playerCameraArm.SetActive(false);
            }

            // Enable the Death Camera
            if (deathCamera != null)
            {
                deathCamera.SetActive(true);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Respawn point is not set!");
        }

        currentHealth = maxHealth;
        UpdateHealthUI();
        gameObject.SetActive(true); // Reactivate the player

        // Disable the Death Camera
        if (deathCamera != null)
        {
            deathCamera.SetActive(false);
        }

        // Re-enable the player's camera arm
        if (playerCameraArm != null)
        {
            playerCameraArm.SetActive(true);
        }

        if (respawnMenu != null)
        {
            respawnMenu.SetActive(false);
        }
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        Debug.Log("New respawn point set to: " + newRespawnPoint.position);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
