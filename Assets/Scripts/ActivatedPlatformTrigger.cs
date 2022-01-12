using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedPlatformTrigger : PlatformTrigger
{   
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        var activatedPlatform = _platform as ActivatedPlatform;
        if (activatedPlatform == null) return;

        activatedPlatform.Activate();
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
