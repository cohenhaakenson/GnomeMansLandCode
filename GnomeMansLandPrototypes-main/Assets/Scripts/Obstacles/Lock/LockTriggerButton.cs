using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTriggerButton : MonoBehaviour
{
    [SerializeField] private LockAnimated theLock;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            theLock.OpenLock();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            theLock.CloseLock();
        }
    }
}
