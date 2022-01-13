using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var detector = other.GetComponent<GroundDetector>();
        if (detector == null) return;

        detector.DestroyPlayer();
    }
}
