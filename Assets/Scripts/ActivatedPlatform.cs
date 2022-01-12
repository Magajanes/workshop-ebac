using UnityEngine;

public class ActivatedPlatform : MovingPlatform
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _moveTime;
    [SerializeField]
    private Direction _direction;

    private int sign = 1;
    private bool isActive = false;

    protected override Vector3 VelocityFunction(float parameter)
    {
        return sign * _speed * _versors[_direction];
    }

    protected override void Update()
    {
        if (!isActive) return;

        base.Update();
    }

    public void Activate()
    {
        if (isActive) return;

        isActive = true;
    }

    protected override void UpdateParameter()
    {
        if (_timeParameter < _moveTime)
        {
            _timeParameter += Time.deltaTime;
            return;
        }

        isActive = false;
        _velocity = Vector3.zero;

        _timeParameter = 0;
        sign = -sign;
    }
}
