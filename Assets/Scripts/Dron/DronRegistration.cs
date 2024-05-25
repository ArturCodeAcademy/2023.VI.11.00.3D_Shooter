using UnityEngine;

public class DronRegistration : MonoBehaviour
{
    private DronSpawner _dronSpawner;

    public void Register(DronSpawner dronSpawner)
    {
		_dronSpawner = dronSpawner;
	}

    private void OnDestroy()
    {
		_dronSpawner?.Unregister();
	}
}
