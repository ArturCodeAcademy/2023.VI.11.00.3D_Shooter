using System;
using UnityEngine;

public class Health : MonoBehaviour, IHittable
{
    [field:SerializeField, Min(1)]
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public event Action<float, float> OnHealthChanged;
    public event Action OnHealthEnd;

    private void Awake()
    {
		CurrentHealth = MaxHealth;
	}

    public float Hit(float damage)
    {
        if (damage <= 0)
            return 0;

        float appliedDamage = Mathf.Min(CurrentHealth, damage);
        CurrentHealth -= appliedDamage;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
			OnHealthEnd?.Invoke();

        return appliedDamage;
    }

    public float Heal(float amount)
    {
		if (amount <= 0)
			return 0;

		float appliedHeal = Mathf.Min(amount, MaxHealth - CurrentHealth);
        CurrentHealth += appliedHeal;
		OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        return appliedHeal;
	}
}
