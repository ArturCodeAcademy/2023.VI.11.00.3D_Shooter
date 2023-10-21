using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    public event Action OnGetGrounded;
    public event Action OnGetOfTheGround;
    public event Action OnMove;
    public event Action OnStop;

    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _groundedTimer = 0.2f;
    [SerializeField, Range(0, 1)] private float _moveVectorMultiplier;

    private CharacterController _characterController;
    private float _groundedTimerCounter = 0f;
    private float _verticalVelocity = 0f;
    private Vector3 _moveVector;

	private void Awake()
	{
	    _characterController = GetComponent<CharacterController>();
	}

	private void LateUpdate()
	{
		Vector3 moveVector = (_moveVector + _verticalVelocity * Vector3.up) * Time.deltaTime;
        if (_moveVector.magnitude > 0)
            OnStop?.Invoke();
        else
            OnMove?.Invoke();
        _verticalVelocity -= Physics.gravity.magnitude * _gravityScale * Time.deltaTime;

        var collisionFlags = _characterController.Move(moveVector);

        if (collisionFlags.HasFlag(CollisionFlags.Below))
        {
            IsGrounded = true;
            _groundedTimerCounter = _groundedTimer;
            OnGetGrounded?.Invoke();
            _verticalVelocity = 0;
        }
        else
        {
            if (_groundedTimerCounter > 0)
                _groundedTimerCounter -= Time.deltaTime;
			else
            {
                if (IsGrounded)
                    OnGetOfTheGround?.Invoke();
				IsGrounded = false;
            }
        }

        _moveVector = Vector3.Lerp(_moveVector, Vector3.zero, _moveVectorMultiplier);
	}

    public void HorizontalMove(Vector3 moveVector)
    {
		_moveVector = moveVector;
        _moveVector.y = 0;
	}

	public void Jump(float jumpForce)
    {
		_verticalVelocity = jumpForce;
	}
}
