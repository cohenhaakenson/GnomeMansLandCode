using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BD
{
    [System.Serializable]
    public class PlayerStatManager : MonoBehaviour
    {
        public static PlayerStatManager InstanceBase;
        public List<PlayerStat> Stats = new List<PlayerStat>();
        
        public virtual void Awake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (InstanceBase == null)
                InstanceBase = this;
            else
                Debug.LogError($"PlayerStatManager: Trying to set instance but there is already one! {InstanceBase}");
        }

        public void CreateStat(PlayerStatType StatType)
        {
            PlayerStat st = Stats.Find(x => x.StatType == StatType);
            if (st != null)
            {
                Debug.LogError($"PlayerStatManager:CreateStat: Trying to create a stat when one already exists. {StatType.name}");
                return;
            }

            PlayerStat ns = new PlayerStat();
            ns.StatType = StatType;
            Stats.Add(ns);
        }

        public bool HasStat(PlayerStatType StatType)
        {
            PlayerStat st = Stats.Find(x => x.StatType == StatType);
            return st != null;
        }
        public void StatCheck(PlayerStatType StatType)
        {
            if (!HasStat(StatType))
                CreateStat(StatType);
        }
        
        public PlayerStat GetStat(PlayerStatType StatType)
        {
            StatCheck(StatType);
            PlayerStat st = Stats.Find(x => x.StatType == StatType);
            return st;
        }

        public static float GetStatVal(PlayerStatType StatType)
        {
            return InstanceBase.GetStatValue(StatType);
        }

        public float GetStatValue(PlayerStatType StatType)
        {
            StatCheck(StatType);
            PlayerStat st = Stats.Find(x => x.StatType == StatType);
            if (st == null)
            {
                Debug.LogWarning($"PlayerData:GetStat: Couldn't find stat for type {StatType}");
                return 0.0f;
            }
            return st.Value;
        }

        public static void SetStatVal(PlayerStatType StatType, float val, GameObject owner) {InstanceBase.SetStatValue(StatType, val, owner); }
        public void SetStatValue(PlayerStatType StatType, float val, GameObject owner)
        {
            StatCheck(StatType);
            PlayerStat st = Stats.Find(x => x.StatType == StatType);
            if (st == null)
            {
                Debug.LogWarning($"PlayerData:GetStat: Couldn't find stat for type {StatType}");
                return;
            }

            st.Value = val;
            GameEventManager.RaiseGlobalEvent(st.ChangeEvent, owner ? owner : PlayerManagerBase.InstanceBase.gameObject);
        }

        
    }

}