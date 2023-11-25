using UnityEngine;

public class HitRetranslator : MonoBehaviour, IHittable
{
    [SerializeField] private Health _health;
	[SerializeField, Min(0)] private float _damageMultiplier = 1;

	public float Hit(float damage)
    {
		return _health.Hit(damage * _damageMultiplier);
	}
}
