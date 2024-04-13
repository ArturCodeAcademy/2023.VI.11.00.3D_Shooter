using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnEndHealth : MonoBehaviour
{
	[SerializeField] private Health _health;

	[Space(5)]
	[SerializeField] private GameObject _explosionPrefab;
	[SerializeField] private List<GameObject> _detach;
	[SerializeField, Min(0)] private float _destroyDelay = 5f;
	[SerializeField, Min(0)] private float _explosionForce = 0.5f;
	[SerializeField, Min(0)] private float _explosionAngularImpulse = 0.5f;
	[SerializeField, Min(0)] private float _explosionRadius = 0.5f;

	private void OnEnable()
	{
		_health.OnHealthEnd += OnHealthEnd;
	}

	private void OnDisable()
	{
		_health.OnHealthEnd -= OnHealthEnd;
	}

	private void OnHealthEnd()
	{
		foreach (var item in _detach)
		{
			item.transform.parent = null;
			if (item.TryGetComponent(out MeshCollider collider))
				collider.convex = true;
			var rb = item.AddComponent<Rigidbody>();
			rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
			rb.AddTorque(Random.insideUnitSphere * _explosionAngularImpulse, ForceMode.Impulse);
			Destroy(item, _destroyDelay);
		}
		
		if (_explosionPrefab is not null)
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}

#if UNITY_EDITOR

	private void Reset()
	{
		_health ??= GetComponent<Health>();

		_detach = GetComponentsInChildren<Collider>().Select(c => c.gameObject).ToList();
	}

#endif
}
