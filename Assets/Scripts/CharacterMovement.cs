using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private SphericalCameraMovement _cameraMovement;
    [SerializeField]
    private Animator _animator;
    
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravityModifier;
    [SerializeField]
    private float _jumpForce;

    private Vector3 _frontDirection;
    private Vector3 _sideDirection;
    private Vector3 _walkDirection;
    private Vector3 _verticalDirection;
    private Vector3 _moveDirection;

    private bool _isJumping;

    private void Start()
    {
        transform.forward = _cameraMovement.Forward;
    }

    private void Update()
    {
        _frontDirection = Input.GetAxis("Vertical") * _cameraMovement.Forward;
        _sideDirection = Input.GetAxis("Horizontal") * _cameraMovement.Right;
        _walkDirection = _frontDirection + _sideDirection;

        if (_characterController.isGrounded)
        {
            _verticalDirection.y = 0;
        }
        else
        {
            _verticalDirection.y += Physics.gravity.y * _gravityModifier * Time.deltaTime;
        }

        if (_isJumping && _characterController.isGrounded)
        {
            _isJumping = false;
        }

        if (!_isJumping)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _verticalDirection.y = _jumpForce;
                _isJumping = true;
            }
        }

        if (_walkDirection.magnitude > 0)
        {
            transform.forward = _walkDirection.normalized;

            if (_walkDirection.magnitude > 1)
            {
                _walkDirection = _walkDirection.normalized;
            }
        }

        _moveDirection = _walkDirection + _verticalDirection;
        Vector3 moveDelta = _moveDirection * _speed * Time.deltaTime;
        _characterController.Move(moveDelta);
    }
}
