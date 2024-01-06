using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void LoadLevel(int level)
	{
		SceneManager.LoadScene(level);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
