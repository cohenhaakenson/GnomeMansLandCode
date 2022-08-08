using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSetActive : MonoBehaviour
{
    public void OpenLock()
    {
        gameObject.SetActive(false);
    }
    public void CloseLock()
    {
        gameObject.SetActive(true);
    }

}
