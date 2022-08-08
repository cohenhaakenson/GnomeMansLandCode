using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BD
{
    public class GlobalParentAttach : MonoBehaviour
    {
        public GameTag ParentTag;

        void Start()
        {
            StartCoroutine(WaitForInstance());
        }

        IEnumerator WaitForInstance()
        {
            while (GameManagerBase.instanceBase == null)
            {
                Debug.Log($"GlobalParentAttach::WaitForInstance: {gameObject.name} GameManager: {GameManagerBase.instanceBase}");
                yield return 0;
            }
            yield return 0;
            Attach();
        }

        void Attach()
        {
            BD.GameManagerBase.instanceBase.AttachToGlobalParent(transform, ParentTag);
        }

    }
}