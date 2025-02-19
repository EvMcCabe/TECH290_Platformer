using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    [SerializeField] private Slider healthBar; // Assign in Inspector
    [SerializeField] private Health healthComponent; // Assign in Inspector

    void Start()
{
    if (healthComponent == null)
    {
        Debug.LogError("Health component is not assigned to " + gameObject.name);
        return;
    }

    // Ensure slider matches max health
    healthBar.maxValue = healthComponent.maxHealth;
    healthBar.minValue = 0; // Explicitly set min value
    healthBar.value = healthComponent.GetCurrentHealth(); // Set starting health

    healthComponent.OnHealthChanged += UpdateHealthUI;
}


    void UpdateHealthUI(int currentHealth)
    {
        Debug.Log("Health Updated: " + currentHealth);
        healthBar.value = currentHealth;
    }

    private void OnDestroy()
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged -= UpdateHealthUI;
        }
    }
}
