using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BD
{
    public class StateManager : MonoBehaviour
    {
        protected StateTag currentStateTag;

        public StateTag StartingStateTag;

        protected State currentState;
        protected StateTag lastStateTag;
        protected State lastState;
        protected bool firstStateChange;

        private bool initialized;

        [HideInInspector] public Dictionary<StateTag, State> States = new Dictionary<StateTag, State>();


        protected virtual void Awake()
        {
            InitStateManager();
            GoToStartState(gameObject);
        }

        public void GoToStartState(GameObject owner)
        {
            ChangeState(StartingStateTag);
        }

        public void InitStateManager()
        {
            if (initialized)
                return;

            SetStateManager(this);
            initialized = true;
        }

        protected virtual void SetStateManager(StateManager mgr)
        {
            List<State> stateComps = new List<State>(GetComponents<State>());
            for (int i = 0; i < stateComps.Count; i++)
            {
                //Debug.Log($"StateManager:SetStateManager: StateComp: {stateComps[i]} Tag: {stateComps[i].ThisStateTag} Object: {gameObject.name}");
                if (States.ContainsKey(stateComps[i].ThisStateTag))
                    continue;

                if (stateComps[i].ThisStateTag == null)
                {
                    Debug.LogWarning($"StateManager:SetStateManager: No ThisStateTag: {gameObject.name}");
                    continue;
                }

                States.Add(stateComps[i].ThisStateTag, stateComps[i]);
                stateComps[i].SetStateManager(this);
            }
        }

        public virtual StateTag GetStateTag()
        {
            return currentStateTag;
        }

        public virtual State GetState()
        {
            return currentState;
        }

        public virtual void ChangeState(StateTag newState)
        {
            if (CanChangeToState(newState))
                MoveToState(newState);
        }

        private void MoveToState(StateTag newState)
        {
            if (currentState)
                currentState.EndState();

            lastStateTag = currentStateTag;
            lastState = currentState;
            currentStateTag = newState;
            currentState = GetStateFromTag(newState);
            currentState.StartState();
            firstStateChange = true;
        }

        public virtual bool CanChangeToState(StateTag newState)
        {
            if (!HasState(newState)) return false;

            if (!currentStateTag) return true;

            return States[newState].CanStartState(newState) && States[currentStateTag].CanEndState(newState);
        }

        public virtual State GetStateFromTag(StateTag checkTag)
        {
            if (!HasState(checkTag)) return null;

            return States[checkTag];
        }

        public virtual bool HasState(StateTag checkTag)
        {
            if (!States.ContainsKey(checkTag))
            {
                Debug.LogError($"StateManager:CanChangeState: Object: {gameObject.name} No state ({checkTag.name})");
                return false;
            }

            return true;
        }
    }
}
    