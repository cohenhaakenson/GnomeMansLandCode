using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BD
{
    [CreateAssetMenu(fileName = "New GameEvent", menuName = "BD/GameEvent", order = 1)]
    public class GameEvent : ScriptableObject
    {
        public bool DebugThisEvent;
    }
}
