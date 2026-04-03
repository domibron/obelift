using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 100;

    float currentHealth;

    bool isDead = false;

    public delegate void OnHealthChangedDelegate(float oldHealth, float newHealth);

    public event Action OnDeath;
    public event OnHealthChangedDelegate OnHealthChanged;

    void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeFromHealth(float amount)
    {
        AddToHealth(-amount);
    }

    public void AddToHealth(float amount)
    {
        InvokeOnHealthChanged(currentHealth, currentHealth + amount);

        currentHealth += amount;

        if (currentHealth <= 0)
        {
            InvokeOnDeath();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public float GetHealthNormalized()
    {
        return currentHealth / maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    protected virtual void Die()
    {
        if (isDead) return;

        InvokeOnDeath();
        isDead = true;
    }

    protected void InvokeOnDeath()
    {
        OnDeath?.Invoke();
    }

    protected void InvokeOnHealthChanged(float oldHealth, float newHealth)
    {
        OnHealthChanged?.Invoke(oldHealth, newHealth);
    }
}
