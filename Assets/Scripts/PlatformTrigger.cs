using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField]
    protected MovingPlatform _platform;

    protected virtual void OnTriggerEnter(Collider other)
    {
        var detector = other.GetComponent<GroundDetector>();
        if (detector == null) return;

        detector.Attach(_platform);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        var detector = other.GetComponent<GroundDetector>();
        if (detector == null) return;

        detector.Release(_platform);
    }
}
