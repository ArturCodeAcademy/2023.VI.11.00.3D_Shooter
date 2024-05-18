using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenerateLevelsButtons : MonoBehaviour
{
	[SerializeField] private LevelButton[] _levelButtons;
	[SerializeField] private Transform _buttonParent;
	[SerializeField] private Button _buttonPrefab;

	[Serializable]
    public class LevelButton
	{
		public string Name;
		public string SceneName;
	}

	private void Start()
	{
		foreach (var levelButton in _levelButtons)
		{
			var button = Instantiate(_buttonPrefab, _buttonParent);
			button.name = levelButton.Name;
			button.gameObject.SetActive(true);
			button.GetComponentInChildren<TMP_Text>().text = levelButton.Name;
			button.onClick.AddListener(() => LoadLevel(levelButton.SceneName));
		}
	}

	private void LoadLevel(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
