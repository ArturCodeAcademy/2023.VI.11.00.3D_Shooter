using UnityEngine;

public class HideCursor : MonoBehaviour
{
	private void Awake()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
