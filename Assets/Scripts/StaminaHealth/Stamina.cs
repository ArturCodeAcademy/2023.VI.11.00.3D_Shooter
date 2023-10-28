using System;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [field: SerializeField, Min(1)]
    public float MaxStamina { get; private set; }
    public float CurrentStamina { get; private set; }

    [SerializeField, Min(0)] private float _staminaRegenPerSecond = 1f;

    public event Action<float, float> OnStaminaChanged;
    public event Action OnStaminaEnd;

    private bool _isRegenerating = true;

    private void Awake()
    {
		CurrentStamina = MaxStamina;
	}

    public float UseStamina(float stamina)
    {
        if (stamina <= 0)
			return 0;

        _isRegenerating = false;

        float appliedStamina = Mathf.Min(CurrentStamina, stamina);
		CurrentStamina -= appliedStamina;
        OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);

		if (CurrentStamina <= 0)
            OnStaminaEnd?.Invoke();

		return appliedStamina;
    }

    public float RegenStamina(float amount)
    {
        if (amount <= 0)
            return 0;

        float appliedStamina = Mathf.Min(amount, MaxStamina - CurrentStamina);
        CurrentStamina += appliedStamina;
        OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);

        return appliedStamina;
    }

	private void LateUpdate()
    {
        if (_isRegenerating)
		    RegenStamina(_staminaRegenPerSecond * Time.deltaTime);
        _isRegenerating = CurrentStamina < MaxStamina;
	}
}
