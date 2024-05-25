using UnityEngine;

public class DronMovement : MonoBehaviour
{
    [SerializeField, Min(0)] private float _horizontalSpeed = 5f;
    [SerializeField, Min(0)] private float _verticalSpeed = 5f;
    [SerializeField, Min(0)] private float _minHorizontalDistance = 15f;
    [SerializeField, Min(0)] private float _minVerticalDistance = 7.5f;

    private Transform _target;

    private void Start()
    {
		_target = Player.Instance.transform;
	}

	private void Update()
    {
		if (_target == null)
        {
			return;
		}

		Vector3 targetPosition = _target.position;
		Vector3 currentPosition = transform.position;

		Vector3 currentPosXZ = new( currentPosition.x,
									targetPosition.y,
									currentPosition.z);
		float horizontalDistance = Vector3.Distance(currentPosXZ, targetPosition);

		Vector3 direction = (targetPosition - currentPosXZ).normalized;
		if (horizontalDistance < _minHorizontalDistance)
			direction = -direction;

		transform.position += direction * _horizontalSpeed * Time.deltaTime;

		direction = (targetPosition.y + _minVerticalDistance > currentPosition.y)
			? Vector3.up
			: Vector3.down;
		transform.position += direction * _verticalSpeed * Time.deltaTime;
    }

#if UNITY_EDITOR

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position + Vector3.left * _minHorizontalDistance,
						transform.position + Vector3.right * _minHorizontalDistance);
		Gizmos.DrawLine(transform.position + Vector3.forward * _minHorizontalDistance,
						transform.position + Vector3.back * _minHorizontalDistance);
	}

#endif
}
