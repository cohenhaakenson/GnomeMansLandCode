using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BD
{
    public class PlayerManagerBase : MonoBehaviour
    {
        public static PlayerManagerBase InstanceBase;

        public virtual void Awake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (!InstanceBase)
                InstanceBase = this;
            //else
              //  Debug.LogError($"PlayerManagerBase: Trying to set instance but there is already one! {InstanceBase}");
        }
    }
}
