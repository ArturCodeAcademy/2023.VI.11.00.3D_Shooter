using Unity.VisualScripting;
using UnityEngine;

public class DronSpawner : MonoBehaviour
{
    [SerializeField] private Health[] _dronPrefabs;
    [SerializeField, Min(0)] private Vector3 _spawnArea;
    [SerializeField, Min(0)] private float _spawnInterval;
	[SerializeField, Min(0)] private int _maxDronCount;

    private float _spawnTimer;
	private int _dronCount;

    private void Update()
    {
		_spawnTimer -= Time.deltaTime;
		if (_spawnTimer <= 0 && _dronCount < _maxDronCount)
        {
			_spawnTimer = _spawnInterval;
			SpawnDron(); 
		}
	}

    private void SpawnDron()
    {
		Health dronPrefab = _dronPrefabs[Random.Range(0, _dronPrefabs.Length)];
		Vector3 position = new( Random.Range(-_spawnArea.x, _spawnArea.x),
								Random.Range(-_spawnArea.y, _spawnArea.y),
								Random.Range(-_spawnArea.z, _spawnArea.z));
		position += transform.position;
		Health dronHealth = Instantiate(dronPrefab, position, Quaternion.identity);
		dronHealth.AddComponent<DronRegistration>().Register(this);
		_dronCount++;
	}

	public void Unregister()
	{
		_dronCount--;
	}

#if UNITY_EDITOR

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, _spawnArea * 2);
	}

#endif
}
