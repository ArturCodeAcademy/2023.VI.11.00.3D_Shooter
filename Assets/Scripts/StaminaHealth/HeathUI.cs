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

	private void Start()
	{
		UpdateUI(_health.CurrentHealth, _health.MaxHealth);
	}

	private void OnEnable()
	{
		_health .OnHealthChanged += UpdateUI;
	}

    private void OnDisable()
    {
		_health.OnHealthChanged -= UpdateUI;
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
