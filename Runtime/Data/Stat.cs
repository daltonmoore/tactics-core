using System;
using UnityEngine;

namespace TacticsCore.Data
{
    [Serializable]
    public class Stat
    {
        [SerializeField] public StatType type;
        [SerializeField] public int value;
        [SerializeField] public Range range;
        
        public Stat(StatType type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }

    [Serializable]
    public enum StatType
    {
        Initiative,
        Damage,
    }
}