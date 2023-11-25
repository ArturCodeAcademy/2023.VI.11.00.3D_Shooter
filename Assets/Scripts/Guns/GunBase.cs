using System.Linq;
using UnityEngine;

public class GunBase : MonoBehaviour
{
	[SerializeField, Min(0.01f)] public float _fireRate;
	[SerializeField, Range(0, 180)] private float _fireSpread;
	[SerializeField] private GameObject _hole;
	[SerializeField] private GameObject _shootEffect;
	[SerializeField] private Transform _muzzle;
	[SerializeField, Min(0)] private float _damage;

	private float _pause = 0;

	private void Update()
	{
        if (_pause > 0)
		{
			_pause -= Time.deltaTime;
			return;
		}

        if (Input.GetMouseButton(0))
		{
			_pause = 1 / _fireRate;
			Vector3 forward = GetDirectionWithSpread(_fireSpread, Camera.main.transform.forward);

			RaycastHit hit = Physics.RaycastAll(Camera.main.transform.position, forward)
				.OrderBy(h => h.distance)
				.Where(h => h.collider.transform != Player.Instance.transform)
				.FirstOrDefault();

			if (_shootEffect is not null)
				Instantiate(_shootEffect, _muzzle.position, _muzzle.rotation);

			if (hit.collider is not null)
			{
				if (_hole is not null)
					Instantiate(_hole, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));

				if (hit.collider.TryGetComponent(out IHittable hittable))
					hittable.Hit(_damage);
			}
		}
	}

	public static Vector3 GetDirectionWithSpread(float bulletSpread, Vector3 forward)
	{
		Vector3 angle = UnityEngine.Random.onUnitSphere;
		angle.z = 0;
		angle.Normalize();
		angle *= UnityEngine.Random.Range(0, bulletSpread);
		return Quaternion.Euler(angle) * forward;
	}
}
