using UnityEngine;

public class LookToMainCamera : MonoBehaviour
{
    [SerializeField] private bool _useOffset = true;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
		if (_useOffset)
			transform.localPosition = _offset;
		
		transform.LookAt(Camera.main.transform.position);
		transform.Rotate(0, 180, 0);
	}

#if UNITY_EDITOR

	private void Reset()
	{
		_useOffset = true;
		_offset = transform.localPosition;
	}

	private void OnValidate()
	{
		if (_useOffset)
			transform.localPosition = _offset;
	}

#endif
}
