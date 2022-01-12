using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingPlatform : MonoBehaviour
{
    protected Vector3 _velocity;
    protected float _timeParameter;
    public event Action<Vector3> OnMove;

    protected abstract Vector3 VelocityFunction(float parameter);

    protected static Dictionary<Direction, Vector3> _versors = new Dictionary<Direction, Vector3>
    {
        { Direction.RIGHT, Vector3.right },
        { Direction.UP, Vector3.up },
        { Direction.FORWARD, Vector3.forward }
    };

    protected virtual void Update()
    {
        Move();
        UpdateParameter();

        OnMove?.Invoke(_velocity);
    }

    protected virtual void UpdateParameter()
    {
        _timeParameter += Time.deltaTime;
    }

    protected virtual void Move()
    {
        _velocity = VelocityFunction(_timeParameter);
        transform.position += _velocity * Time.deltaTime;
    }
}

public enum Direction
{
    RIGHT,
    UP,
    FORWARD
}
