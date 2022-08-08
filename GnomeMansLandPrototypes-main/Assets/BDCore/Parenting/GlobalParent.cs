using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BD
{
    public class GlobalParent : MonoBehaviour
    {
        public GameTag parentTag;
        
        void Awake()
        {
            StartCoroutine(SetupGlobalParent());
        }

        IEnumerator SetupGlobalParent()
        {
            while (GameManagerBase.instanceBase == null)
            {
                Debug.Log($"GlobalParent::SetupGlobalParent: Waiting For Instance: {gameObject.name}");
                yield return 0;
            }
            BD.GameManagerBase.instanceBase.AddGlobalParent(this);
        }
    }
}
