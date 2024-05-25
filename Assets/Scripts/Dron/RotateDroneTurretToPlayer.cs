using UnityEngine;

public class RotateDroneTurretToPlayer : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _lookToTarget;
	[SerializeField, Min(0)] private float _verticalRotationSpeed = 1f;
	[SerializeField, Min(0)] private float _horizontalRotationSpeed = 1f;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private float _maxAngleX = 90f;
	[SerializeField] private float _minAngleX = -90f;

	private void Update()
	{
		_lookToTarget.LookAt(Player.Instance.transform.position + _offset);
		
		Quaternion vertical = Quaternion.Euler(_lookToTarget.eulerAngles.x, 0, 0);
		Quaternion horizontal = Quaternion.Euler(0, _lookToTarget.eulerAngles.y, 0);

		float dX = Quaternion.Angle(horizontal, _turret.localRotation);
		float lerpX = Mathf.Clamp01(_horizontalRotationSpeed * Time.deltaTime / dX);
		_turret.localRotation = Quaternion.Lerp(_turret.localRotation, vertical, lerpX);

		float dY = Quaternion.Angle(vertical, _turret.localRotation);
		float lerpY = Mathf.Clamp01(_verticalRotationSpeed * Time.deltaTime / dY);
		transform.rotation = Quaternion.Lerp(transform.rotation, horizontal, lerpY);
	}
}
