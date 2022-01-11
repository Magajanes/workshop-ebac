using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement _player;

    public static event Action OnPlayerDestroyed; 

    private void SetPlayerExternalVelocity(Vector3 velocity)
    {
        _player.SetExternalMovement(velocity);
    }
    
    public void Attach(MovingPlatform platform)
    {
        _player.SetAttached(true);
        platform.OnMove += SetPlayerExternalVelocity;
    }

    public void Release(MovingPlatform platform)
    {
        _player.SetAttached(false);
        platform.OnMove -= SetPlayerExternalVelocity;
    }

    public void DestroyPlayer()
    {
        OnPlayerDestroyed?.Invoke();
    }
}
