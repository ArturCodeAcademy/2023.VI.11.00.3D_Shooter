using UnityEngine;

public class ShowCursor : MonoBehaviour
{
	private void Awake()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
