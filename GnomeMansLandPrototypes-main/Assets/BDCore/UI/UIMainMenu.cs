using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BD
{
    public class UIMainMenu : UIBase
    {
        public static UIMainMenu instance;
        
        public virtual void Awake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError($"UIMainMenu: Trying to set instance but there is already one! {instance}");
        }
    }
    
}
