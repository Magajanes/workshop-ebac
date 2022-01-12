using UnityEngine;

public class SenoidalPlatform : MovingPlatform
{
    [SerializeField]
    private float _amplitude;
    [SerializeField]
    private float _frequency;
    [SerializeField]
    private Direction _direction;

    protected override Vector3 VelocityFunction(float parameter)
    {
        return _amplitude * Mathf.Sin(_frequency * parameter) * _versors[_direction];
    }

    protected override void UpdateParameter()
    {
        base.UpdateParameter();

        if (_timeParameter < 2 * Mathf.PI) return;

        _timeParameter = 0;
    }
}
