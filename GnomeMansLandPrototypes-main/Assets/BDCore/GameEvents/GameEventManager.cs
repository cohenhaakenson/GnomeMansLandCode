using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BD
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager instance;

        public bool DebugThisManager;

    #region GameEvents

        public List<GameEvent> AllGameEvents = new List<GameEvent>();

        Dictionary<string, List<GameEventListener>> GameEventLinks = new Dictionary<string, List<GameEventListener>>();

        bool eventsInit = false;

        private void Awake()
        {
            InitEvents();
        }

        public void InitEvents()
        {
            if (eventsInit)
                return;

            instance = this;

            int num = 0;
            foreach (GameEvent ge in AllGameEvents)
            {
                if (DebugThisManager) Debug.Log($"GameEventManager: RegisterAllEvents: Event: {ge.name}");
                num++;
                if (!GameEventLinks.ContainsKey(ge.name))
                {
                    GameEventLinks.Add(ge.name, new List<GameEventListener>());
                }
            }

            if (DebugThisManager) Debug.Log($"GameEventManager: TotalEventsRegistered: {num}");
            eventsInit = true;
        }

        public static void RaiseGlobalEvent(GameEvent ge, GameObject owner)
        {
            GameEventManager.instance.RaiseEvent(ge, owner);
        }

        public void RaiseEvent(GameEvent ge, GameObject owner)
        {
            if (!eventsInit)
                InitEvents();

            if (ge == null)
            {
                Debug.LogError($"GameEventManager: RaiseEvent: Given Game Event is null. Coming from {owner}");
                return;
            }

            if (!GameEventLinks.ContainsKey(ge.name))
            {
                Debug.LogError($"GameEventManager: RaiseEvent: No Event: {ge.name} in the GameEventLinks.");
                return;
            }

            if (DebugThisManager)
                Debug.Log(
                    $"GameEventManager: RaiseEvent: {ge.name} Owner: {owner} Listeners: {GameEventLinks[ge.name].Count}");

            for (int i = GameEventLinks[ge.name].Count - 1; i >= 0; i--)
            {
                if (ge.DebugThisEvent)
                    Debug.Log(
                        $"GameEventManager: GameEvent: Raise: {owner.name} Event: {ge.name} Listener: {GameEventLinks[ge.name][i]} -------DEBUG-------");


                //Debug.Log($"GameEventManager: RaiseEvent: {ge} Owner: {owner.name} Contains Name: {GameEventLinks.ContainsKey(ge.name)} EventLinkCount: {GameEventLinks[ge.name].Count} i: {i}");
                if (GameEventLinks[ge.name][i] == null)
                {
                    if (DebugThisManager)
                        Debug.Log($"GameEventManager: {ge.name} found a null entry at index {i}. Removing");
                    GameEventLinks[ge.name].RemoveAt(i);
                }
                else
                    GameEventLinks[ge.name][i].OnEventRaised(owner);
            }
        }

        public void RegisterListenerToEvent(GameEvent ge, GameEventListener gel)
        {
            if (!eventsInit)
                InitEvents();

            if (GameEventLinks == null || ge == null)
                Debug.LogError($"GameEventManager: RegisterListenerToEvent: Links: {GameEventLinks} GameEvent: {ge}");

            if (!GameEventLinks.ContainsKey(ge.name))
            {
                Debug.LogError(
                    $"GameEventManager: RegisterListenerToEvent: No Event: {ge.name} in the GameEventLinks. Please Add it to the GameEventManager prefab. Keep in mind that the local GameManager might have overrides in the sub-prefab GameEventManager. This should not be the case. Revert that prefab to it's original.");
                return;
            }

            if (GameEventLinks[ge.name].Contains(gel))
            {
                Debug.LogError(
                    $"GameEventManager: RegisterListenerToEvent: Trying to register {gel.gameObject.name} in the GameEventLinks but it's already registered.");
                return;
            }

            GameEventLinks[ge.name].Add(gel);
        }

        public void UnregisterListenerToEvent(GameEvent ge, GameEventListener gel)
        {
            //Debug.Log($"GameEventManager: UnregisterListenerToEvent: Event: {ge} Listener: {gel}");
            if (!eventsInit)
                InitEvents();

            if (!GameEventLinks.ContainsKey(ge.name))
            {
                Debug.LogError(
                    $"GameEventManager: UnregisterListenerToEvent: No Event: {ge.name} in the GameEventLinks.");
                return;
            }

            if (!GameEventLinks[ge.name].Contains(gel))
            {
                Debug.LogError(
                    $"GameEventManager: UnregisterListenerToEvent: Trying to unregister {gel.gameObject.name} in the GameEventLinks but it's not there.");
                return;
            }

            GameEventLinks[ge.name].Remove(gel);
        }
    #endregion
    }
}
