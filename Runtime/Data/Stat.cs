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
        [SerializeField] public int value;

        [SerializeField] public bool dontUseRolls = true;
        [SerializeField, HideIf("dontUseRolls", true), MinMax(0, 1, ShowEditRange = true)]
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