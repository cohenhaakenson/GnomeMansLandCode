using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BD
{
    [RequireComponent(typeof(StateManager))]
    public class GameManagerBase : MonoBehaviour
    {
        public static GameManagerBase instanceBase;
        public StateManager GameStateManager;
        public GameEvent EventInitialized;
        public List<GameObject> SpawnOnStart = new List<GameObject>();
        
        public GameEventManager EventManager;
        public PlayerManagerBase PlayerManager;
        
        private bool bInitialized = false;

        public virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SetInstance();
            StartInitialization();
        }

        public virtual void SetInstance()
        {
            if (!instanceBase)
                instanceBase = this;
            else
                Debug.LogError($"GameManager: Trying to set instance but there is already one! {instanceBase}");
        }

        public virtual void StartInitialization()
        {
            StartCoroutine("Initialization");
        }

        IEnumerator Initialization()
        {
            yield return 0;
            EndInitialization();
        }

        public virtual void EndInitialization()
        {
            foreach (GameObject spawn in SpawnOnStart)
                Instantiate(spawn, transform);

            if (!EventManager)
                EventManager = GetComponentInChildren<GameEventManager>(true);
            if (!PlayerManager)
                PlayerManager = GetComponentInChildren<PlayerManagerBase>(true);
        }

        public virtual bool IsInitialized() { return bInitialized; }

        public virtual void SetInitialized(bool set)
        {
            bInitialized = set;
            if (EventManager)
                EventManager.RaiseEvent(EventInitialized, gameObject);
            else
                Debug.LogError($"GameManager: Trying to raise the initialization event but it doesn't exist! {gameObject}");
        }
        
        
        #region GlobalParenting
        private List<GlobalParent> globalParents = new List<GlobalParent>();

        public void AddGlobalParent(GlobalParent gp)
        {
            if (!globalParents.Contains(gp))
                globalParents.Add(gp);
        }

        public GlobalParent GetGlobalParent(GameTag tag)
        {
            for (int i = 0; i < globalParents.Count; i++)
                if (globalParents[i].parentTag == tag)
                    return globalParents[i];

            return null;
        }

        public void AttachToGlobalParent(Transform t, GameTag gt)
        {
            t.SetParent(GetGlobalParent(gt).transform);
        }

        #endregion
    }
}
