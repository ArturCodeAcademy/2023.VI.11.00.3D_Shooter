using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _mouseSensitivity = 100f;
    [SerializeField] private bool _invertX = false;
    [SerializeField] private bool _invertY = false;

    [Space(3)]
    [SerializeField] private GameObject _virtualCamera;

    private float _xRotation = 0f;

    private const float MAX_X_ROTATION = 90f;
    private const float MIN_X_ROTATION = -90f;

	private void Update()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 lookVetor = mouseDelta * _mouseSensitivity;

        if (_invertY)
			lookVetor.y *= -1f;
        if (_invertX)
            lookVetor.x *= -1f;

        _xRotation = Mathf.Clamp(_xRotation - lookVetor.y, MIN_X_ROTATION, MAX_X_ROTATION);
        _virtualCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * lookVetor.x);
    }
}
