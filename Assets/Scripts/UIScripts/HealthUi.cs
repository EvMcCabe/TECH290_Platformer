using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUi : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI healthText; // Reference to the Text component (or TextMeshProUGUI)
    private Health healthComponent;

    void Start()
    {
        healthComponent = FindObjectOfType<Health>(); // Find the player's Health script
        if (healthComponent == null || healthText == null || healthBar == null)
        {
            Debug.LogError("Missing components in the Health UI!");
            return;
        }

        // Set the health bar max value and current value
        healthBar.maxValue = healthComponent.maxHealth;
        healthBar.value = healthComponent.GetCurrentHealth();

        // Update the health text
        healthText.text = "Health: " + healthComponent.GetCurrentHealth();

        // Subscribe to health changes
        healthComponent.OnHealthChanged += UpdateHealthUI;
    }

    void UpdateHealthUI(int currentHealth)
    {
        // Update the health bar and health text when health changes
        healthBar.value = currentHealth;
        healthText.text = "Health: " + currentHealth;  // Display the current health
    }

    private void OnDestroy()
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged -= UpdateHealthUI;
        }
    }
}