using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private SphericalCameraMovement _cameraMovement;
    
    [SerializeField]
    private float _speed;

    private Vector3 _frontDirection;
    private Vector3 _sideDirection;
    private Vector3 _walkDirection;

    private void Start()
    {
        transform.forward = _cameraMovement.Forward;
    }

    private void Update()
    {
        _frontDirection = Input.GetAxis("Vertical") * _cameraMovement.Forward;
        _sideDirection = Input.GetAxis("Horizontal") * _cameraMovement.Right;
        _walkDirection = _frontDirection + _sideDirection;

        if (_walkDirection.magnitude > 0)
        {
            transform.forward = _walkDirection.normalized;

            if (_walkDirection.magnitude > 1)
            {
                _walkDirection = _walkDirection.normalized;
            }
        }

        Vector3 moveDelta = _walkDirection * _speed * Time.deltaTime;
        _characterController.Move(moveDelta);
    }
}
