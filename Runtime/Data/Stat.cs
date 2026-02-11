using System;
using PropertyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

namespace TacticsCore.Data
{
    [Serializable]
    public class Stat
    {
        [SerializeField] public StatType type;
        [SerializeField, HideIf("useRolls", true)] public int value;

        [SerializeField] public bool useRolls;
        [SerializeField, HideIf("useRolls", false), MinMax(0, 100, ShowEditRange = true)]
        public Vector2 range;

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