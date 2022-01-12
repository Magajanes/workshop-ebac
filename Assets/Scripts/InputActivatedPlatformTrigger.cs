using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActivatedPlatformTrigger : PlatformTrigger
{
    private bool _checkInput;
    
    private void Update()
    {
        if (!_checkInput) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            var activatedPlatform = _platform as ActivatedPlatform;
            if (activatedPlatform == null) return;

            activatedPlatform.Activate();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        _checkInput = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        _checkInput = false;
    }

}
