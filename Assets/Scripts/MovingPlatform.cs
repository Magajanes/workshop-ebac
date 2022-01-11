using System;
using UnityEngine;

public abstract class MovingPlatform : MonoBehaviour
{
    protected Vector3 _velocity;
    protected float _timeParameter;
    public event Action<Vector3> OnMove;

    protected abstract Vector3 VelocityFunction(float parameter);

    private void Update()
    {
        Move();
        UpdateParameter();
    }

    protected virtual void UpdateParameter()
    {
        _timeParameter += Time.deltaTime;
    }

    protected virtual void Move()
    {
        _velocity = VelocityFunction(_timeParameter);
        transform.position += _velocity * Time.deltaTime;
        OnMove?.Invoke(_velocity);
    }
}
