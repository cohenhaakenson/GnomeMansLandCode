using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BD
{
    public class UIHUD : UIBase
    {
        public static UIHUD instance;
        
        public virtual void Awake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError($"UIHUD: Trying to set instance but there is already one! {instance}");
        }
    }
    
}
