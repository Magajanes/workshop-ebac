using UnityEngine;

public class HelixPlatform : MovingPlatform
{
    [Header("Parameters")]
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _angularSpeed;
    [SerializeField]
    private float _verticalSpeed;

    protected override Vector3 VelocityFunction(float parameter)
    {
        _velocity.x = _radius * Mathf.Sin(_angularSpeed * parameter);
        _velocity.y = _verticalSpeed;
        _velocity.z = _radius * Mathf.Cos(_angularSpeed * parameter);
        return _velocity;
    }
}
