using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BD
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent ListenToEvent;
        public ListenerEventClass Response;
        public bool DeregisterOnDisable = true;
        public bool LogDebugInfo;

        [System.Serializable]
        public class ListenerEventClass : UnityEvent<GameObject>
        {
        }

        bool registered = false;

        public void OnEventRaised(GameObject owner)
        {
            if (LogDebugInfo)
                Debug.Log($"GameEventListen: OnEventRaised: Listener Object: {gameObject} Event: {ListenToEvent.name} owner: {owner.name} -------DEBUG-------");
            Response.Invoke(owner);
        }

        protected virtual void OnEnable()
        {
            if (LogDebugInfo)
                Debug.Log(
                    $"GameEventListen: OnEnable: Listener Object: {gameObject} Event: {ListenToEvent} -------DEBUG-------");

            if (!gameObject.activeInHierarchy)
                Debug.Log($"Trying to Register when the object is not active in the hierarchy. {gameObject.name}");

            if (!registered && gameObject.activeInHierarchy)
            {
                StartCoroutine(WaitAndRegister());
            }
        }

        IEnumerator WaitAndRegister()
        {
            while (GameEventManager.instance == null)
                yield return 0;

            RegisterWithGEM();
        }

        public virtual void RegisterWithGEM()
        {
            if (registered)
                return;

            if (!ListenToEvent)
            {
                Debug.LogError(
                    $"GameEventListener: RegisterWithGEM: No Event to listen to - {gameObject} | Not registering. Will not respond to an event.");
                return;
            }

            if (LogDebugInfo)
                Debug.Log($"GameEventListen: RegisterWithGEM: Listener Object: {gameObject} -------DEBUG-------");

            GameEventManager.instance.RegisterListenerToEvent(ListenToEvent, this);
            registered = true;
        }

        protected virtual void OnDisable()
        {
            if (registered && DeregisterOnDisable)
            {
                if (LogDebugInfo)
                    Debug.Log($"GameEventListen: OnDisable: Listener Object: {gameObject} Event: {ListenToEvent} -------DEBUG-------");

                UnregisterWithGM();
            }
        }

        private void OnDestroy()
        {
            if (registered)
                UnregisterWithGM();
        }

        IEnumerator WaitAndUnregister()
        {
            //while (GameController.instance == null)
            //{
                yield return 0;
      //      }

            UnregisterWithGM();
        }

        protected virtual void UnregisterWithGM()
        {
            GameEventManager.instance.UnregisterListenerToEvent(ListenToEvent, this);
            registered = false;
        }

    }
    
}
