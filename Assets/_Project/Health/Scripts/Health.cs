using System;
using UnityEngine;
using UnityEngine.UI;


public class Health: MonoBehaviour
{
    [SerializeField]
    private Image _healthFilled;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float CurrentHealth;

    public void Container(IHealth unit)
    {
        MaxHealth = CurrentHealth = unit.Health();
        UpdateHealthVisual(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        
            CurrentHealth-= damage;
        UpdateHealthVisual(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0) UnitIsDead();
    }

    private void UnitIsDead()
    {
        
    }

    public void UpdateHealthVisual(float currentHealth, float maxHealth)
    {
        if (currentHealth <= 0)
        {
            _healthFilled.fillAmount = 0;
            return;
        }
        _healthFilled.fillAmount = currentHealth / maxHealth;
    }
}
