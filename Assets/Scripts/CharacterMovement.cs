using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private SphericalCameraMovement _cameraMovement;
    [SerializeField]
    private Animator _animator;

    [Header("Parameters")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravityModifier;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _drag;

    private Vector3 _frontDirection;
    private Vector3 _sideDirection;
    private Vector3 _walkDirection;
    private Vector3 _verticalDirection;
    private Vector3 _moveDirection;
    private Vector3 _externalVelocity;

    private bool _isJumping;
    private bool _isAttached;

    private void Start()
    {
        transform.forward = _cameraMovement.Forward;
    }

    private void Update()
    {
        CalculateWalkDirection();
        CalculateGravity();
        CheckLanding();
        CheckJump();
        AdjustMovement();
        CheckAttached();
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        _moveDirection = _walkDirection + _verticalDirection;
        Vector3 moveDelta = (_moveDirection * _speed + _externalVelocity) * Time.deltaTime;
        _characterController.Move(moveDelta);
        _animator.SetFloat("Speed", _walkDirection.magnitude);
    }

    private void CheckAttached()
    {
        if (_isAttached) return;

        if (!_characterController.isGrounded) return;

        if (_externalVelocity.magnitude > 0.1f)
        {
            _externalVelocity -= _drag * Time.deltaTime * _externalVelocity.normalized;
            return;
        }

        _externalVelocity = Vector3.zero;
    }

    private void AdjustMovement()
    {
        if (_walkDirection.magnitude == 0) return;

        transform.forward = _walkDirection.normalized;

        if (_walkDirection.magnitude <= 1) return;

        _walkDirection = _walkDirection.normalized;
    }

    private void CheckJump()
    {
        if (_isJumping) return;

        if (!Input.GetButtonDown("Jump")) return;

        _verticalDirection.y = _jumpForce;
        _isJumping = true;
        _animator.SetTrigger("Jump");
    }

    private void CheckLanding()
    {
        if (!_isJumping) return;

        if (!_characterController.isGrounded) return;

        _isJumping = false;
        _animator.SetTrigger("Land");
    }

    private void CalculateGravity()
    {
        if (_characterController.isGrounded)
        {
            _verticalDirection.y = 0;
            return;
        }

        _verticalDirection.y += Physics.gravity.y * _gravityModifier * Time.deltaTime;
    }

    private void CalculateWalkDirection()
    {
        _frontDirection = Input.GetAxis("Vertical") * _cameraMovement.Forward;
        _sideDirection = Input.GetAxis("Horizontal") * _cameraMovement.Right;
        _walkDirection = _frontDirection + _sideDirection;
    }

    public void SetExternalMovement(Vector3 externalVelocity)
    {
        _externalVelocity = externalVelocity;
    }

    public void SetAttached(bool isAttached)
    {
        _isAttached = isAttached;
    }
}
