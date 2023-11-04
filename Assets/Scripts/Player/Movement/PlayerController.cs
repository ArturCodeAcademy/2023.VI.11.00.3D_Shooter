using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCrouching))]
[RequireComponent(typeof(Stamina))]
public class PlayerController : MonoBehaviour
{
	[SerializeField, Min(0)] private float _speedInAir;
	[SerializeField, Min(0)] private float _crouchSpeed;
	[SerializeField, Min(0)] private float _moveSpeed;
	[SerializeField, Min(0)] private float _runSpeed;
	[SerializeField, Min(0)] private float _runStaminaUsagePerSecond;

	[Space(5)]
	[SerializeField, Min(1)] private int _jumpCount = 1;
	[SerializeField, Min(0)] private float _jumpHeight;

	private PlayerMovement _playerMovement;
	private PlayerCrouching _playerCrouching;
	private Stamina _stamina;

	private int _leftJumps;
	private float _jumpForce;

	private void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerCrouching = GetComponent<PlayerCrouching>();
		_stamina = GetComponent<Stamina>();
	}

	private void Start()
	{
		ResetJumps();
		_jumpForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _jumpHeight);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && _leftJumps --> 0)
			_playerMovement.Jump(_jumpForce);

		Vector3 hMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
		hMove = transform.TransformDirection(hMove);

		if (!_playerMovement.IsGrounded)
			hMove *= _speedInAir;
		else if (_playerCrouching.IsCrouched)
			hMove *= _crouchSpeed;
		else if (Input.GetKey(KeyCode.LeftShift))
		{
			float usage = _runStaminaUsagePerSecond * Time.deltaTime;
			float speed = _moveSpeed + (_runSpeed - _moveSpeed) * (_stamina.UseStamina(usage) / usage);
			hMove *= speed;
		}
		else
			hMove *= _moveSpeed;

		if (hMove != Vector3.zero)
			_playerMovement.HorizontalMove(hMove);
	}

	private void ResetJumps(float _ = default)
	{
		_leftJumps = _jumpCount;
	}

	private void OnEnable()
	{
		_playerMovement.OnGetGrounded += ResetJumps;
	}

	private void OnDisable()
	{
		_playerMovement.OnGetGrounded -= ResetJumps;
	}

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (_crouchSpeed > _moveSpeed)
			_crouchSpeed = _moveSpeed;

		if (_moveSpeed > _runSpeed)
			_moveSpeed = _runSpeed;
	}

#endif
}
