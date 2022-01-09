using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;
    
    [SerializeField]
    private float _speed;

    private Vector3 _frontDirection;
    private Vector3 _sideDirection;
    private Vector3 _walkDirection;

    private void Update()
    {
        _frontDirection = Input.GetAxis("Vertical") * transform.forward;
        _sideDirection = Input.GetAxis("Horizontal") * transform.right;

        _walkDirection = _frontDirection + _sideDirection;
        Vector3 moveDelta = _walkDirection * _speed * Time.deltaTime;
        _characterController.Move(moveDelta);
    }
}
