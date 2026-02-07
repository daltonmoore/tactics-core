using System;
using System.Collections.Generic;
using TacticsCore.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    [CustomEditor(typeof(UnitDatabase))]
    public class UnitDatabaseEditor : UnityEditor.Editor
    {
        private void InitializeUnitStats()
        {
            UnitDatabase unitDatabase = target as UnitDatabase;

            foreach (UnitGroup unitGroup in unitDatabase.unitGroups)
            { 
                Debug.Log($"UnitGroup: {unitGroup.Name}");
                foreach (Unit unit in unitGroup.units)
                {
                    Debug.Log($"Unit: {unit.Name}");
                    if (unit.Stats == null || unit.Stats.Count == 0)
                    {
                        Debug.Log($"Adding stats to unit {unit.Name}");
                        unit.Stats = new List<Stat>
                        {
                            new (StatType.Initiative, 0)
                        };
                    }
                    else
                    {
                        Debug.Log($"Unit {unit.Name} already has stats");
                    }
                }
            }
        }

        private void OnValidate()
        {
            InitializeUnitStats();
        }
    }
}