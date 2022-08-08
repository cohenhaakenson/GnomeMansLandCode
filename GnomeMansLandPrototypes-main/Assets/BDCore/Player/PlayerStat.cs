using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BD
{
    [System.Serializable]
    public class PlayerStat
    {
        public GameEvent ChangeEvent;
        public PlayerStatType StatType;
        public float Value;
        public float ValueMin = 0;
        public float ValueMax = 100;

        public void AddTo(float val, GameObject referringObject)
        {
            float newVal = Value + val;
            Value = Mathf.Clamp(newVal, ValueMin, ValueMax);
            if (ChangeEvent)
                GameEventManager.RaiseGlobalEvent(ChangeEvent, referringObject);
        }

        public void TakeFrom(float val, GameObject referringObject)
        {
            AddTo(-val, referringObject);
        }

        public void Set(float val, GameObject referringObject)
        {
            if (val != Value)
            {
                Value = Mathf.Min(Mathf.Max(ValueMin, val), ValueMax);
                if (ChangeEvent)
                    GameEventManager.RaiseGlobalEvent(ChangeEvent, referringObject);
            }
        }
    }    
}
