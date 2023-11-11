using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeathUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _foreground;
    [SerializeField] private bool _useGradient;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _format = "{0:0} / {1:0}";
	[SerializeField, Min(0)] private float _updateTime = 2f;

	private float _currentHealth;
	private float _currentHealthOnUI;
	private float _max;
	private float _updateTimer;

	private void Start()
	{
		_currentHealthOnUI = _currentHealth = _health.CurrentHealth;
		UpdateUI(_health.CurrentHealth, _health.MaxHealth);
	}

	private void Update()
	{
		if (_currentHealthOnUI != _currentHealth)
		{
			float health = Mathf.Lerp(_currentHealthOnUI, _currentHealth, _updateTimer / _updateTime);
			_updateTimer += Time.deltaTime;
			UpdateUI(health, _max);
			if (_updateTimer >= _updateTime)
				_currentHealthOnUI = _currentHealth;
		}
	}

	private void OnEnable()
	{
		_health.OnHealthChanged += SetCurrentHealth;
	}

    private void OnDisable()
    {
		_health.OnHealthChanged -= SetCurrentHealth;
	}

	private void SetCurrentHealth(float current, float max)
	{
		_currentHealth = current;
		_max = max;
		_updateTimer = 0;
	}

	private void UpdateUI(float current, float max)
    {
        float fillAmount = current / max;
        _foreground.fillAmount = fillAmount;
        
        if (_useGradient)
			_foreground.color = _gradient.Evaluate(fillAmount);

        if (_text is not null)
            _text.text = string.Format(_format, current, max, fillAmount * 100);
    }
}
