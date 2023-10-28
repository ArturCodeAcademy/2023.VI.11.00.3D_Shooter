using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
	[SerializeField] private Stamina _stamina;
	[SerializeField] private Image _foreground;
	[SerializeField] private bool _useGradient;
	[SerializeField] private Gradient _gradient;
	[SerializeField] private TMP_Text _text;
	[SerializeField] private string _format = "{0:0} / {1:0}";

	private void Start()
	{
		UpdateUI(_stamina.CurrentStamina, _stamina.MaxStamina);
	}

	private void OnEnable()
	{
		_stamina.OnStaminaChanged += UpdateUI;
	}

	private void OnDisable()
	{
		_stamina.OnStaminaChanged -= UpdateUI;
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
