using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BD
{
    public class DebugEnable : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log($"<color=green>DebugEnable:OnEnable: {gameObject.name}</color>");
        }

        private void OnDisable()
        {
            Debug.Log($"<color=red>DebugEnable:OnDisable: {gameObject.name}</color>");
        }
    }
}

