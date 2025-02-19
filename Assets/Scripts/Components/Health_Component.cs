using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public event Action<int> OnHealthChanged; // Event triggered on health change
    public event Action OnDeath; // Event triggered on death

    [SerializeField] private Slider healthSlider; // Optional UI reference (for player only)

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Ensure UI starts at full health if applicable
    }

    public void Damage(int amount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent negative health

            OnHealthChanged?.Invoke(currentHealth); // Notify subscribers
            UpdateHealthUI(); // Update UI if applicable

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
            currentHealth = Mathf.Min(currentHealth, maxHealth); // Prevent overhealing

            OnHealthChanged?.Invoke(currentHealth);
            UpdateHealthUI(); // Update UI if applicable
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            
            healthSlider.value = (float)currentHealth / maxHealth; // Normalize between 0-1
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}