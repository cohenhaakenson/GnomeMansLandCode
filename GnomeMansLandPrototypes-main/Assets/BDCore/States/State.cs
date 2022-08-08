using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BD
{
    public class State : MonoBehaviour
    {
        [HideInInspector]
        public string STATE_IS_ON = "STATE IS ON";

        public StateTag ThisStateTag;

        [HideInInspector] public bool StateIsOn;

        [Tooltip("List of objects to turn on during this state and turn off when it leaves.")]
        public List<GameObject> OnObjects = new List<GameObject>();

        [Tooltip("List of objects to turn off during this state and turn off when it leaves.")]
        public List<GameObject> OffObjects = new List<GameObject>();

        protected StateManager manager;

        public virtual void SetStateManager(StateManager mgr)
        {
            manager = mgr;
        }

        public bool CanStartState(StateTag targetState)
        {
            return targetState == ThisStateTag;
        }

        public bool CanEndState(StateTag targetState)
        {
            return targetState != ThisStateTag;
        }

        protected virtual void ShowOnObjects()
        {
            for (int i = 0; i < OnObjects.Count; i++)
                OnObjects[i].SetActive(true);
        }

        protected virtual void HideOnObjects()
        {
            for (int i = 0; i < OnObjects.Count; i++)
                OnObjects[i].SetActive(false);
        }

        protected virtual void ShowOffObjects()
        {
            for (int i = 0; i < OffObjects.Count; i++)
                OffObjects[i].SetActive(true);
        }

        protected virtual void HideOffObjects()
        {
            for (int i = 0; i < OffObjects.Count; i++)
                OffObjects[i].SetActive(false);
        }

        public void StartState()
        {
            enabled = true;
            StateIsOn = true;
            OnStartState();
        }

        public void EndState()
        {
            enabled = false;
            StateIsOn = false;
            OnEndState();
        }

        // OnStartState happens as soon as the state changes.
        protected virtual void OnStartState()
        {
            //Debug.Log("State: OnStartState: " + gameObject.name + " State: " + ThisStateTag);
            ShowOnObjects();
            HideOffObjects();
        }

        // OnEndState happens as soon as the state end.
        protected virtual void OnEndState()
        {
            //Debug.Log("State: OnEndState: " + gameObject.name + " State: " + ThisStateTag);
            HideOnObjects();
            ShowOffObjects();
        }

    }
    
}
