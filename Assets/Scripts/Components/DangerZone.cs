using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public int damagePerSecond = 20; // Damage per second
    public float damageInterval = 1f; // How often the player takes damage
    private HashSet<Health> playersInZone = new HashSet<Health>(); // Track players inside

    void OnTriggerEnter(Collider other)
    {
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playersInZone.Add(playerHealth);
            StartCoroutine(DamageOverTime(playerHealth));
        }
    }

    void OnTriggerExit(Collider other)
    {
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playersInZone.Remove(playerHealth);
        }
    }

    IEnumerator DamageOverTime(Health playerHealth)
    {
        while (playersInZone.Contains(playerHealth))
        {
            playerHealth.Damage(damagePerSecond);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}