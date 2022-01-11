using System.Collections.Generic;
using UnityEngine;

public class SenoidalPlatform : MovingPlatform
{
    public enum Direction
    {
        RIGHT,
        UP,
        FORWARD
    }

    [SerializeField]
    private float _amplitude;
    [SerializeField]
    private float _frequency;
    [SerializeField]
    private Direction _direction;

    private static Dictionary<Direction, Vector3> _movementDirections = new Dictionary<Direction, Vector3>
    {
        { Direction.RIGHT, Vector3.right },
        { Direction.UP, Vector3.up },
        { Direction.FORWARD, Vector3.forward }
    };

    protected override Vector3 VelocityFunction(float parameter)
    {
        return _amplitude * Mathf.Sin(_frequency * parameter) * _movementDirections[_direction];
    }

    protected override void UpdateParameter()
    {
        base.UpdateParameter();

        if (_timeParameter < 2 * Mathf.PI) return;

        _timeParameter = 0;
    }
}
