using System;
using System.Collections.Generic;
using TacticsCore.Data;
using TacticsCore.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Editor
{
    [CustomEditor(typeof(UnitDatabase))]
    public class UnitDatabaseEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            Button button = new(InitializeUnitStats)
            {
                text = "Init Stats"
            };
            
            MinMaxSlider minMaxSlider = new ("test", 0, 100);
            root.Add(minMaxSlider);

            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            root.Add(button);
            
            return root;
        }

        private void InitializeUnitStats()
        {
            UnitDatabase unitDatabase = target as UnitDatabase;

            foreach (UnitGroup unitGroup in unitDatabase.unitGroups)
            { 
                foreach (Unit unit in unitGroup.units)
                {
                    unit.Stats.Clear();

                    foreach (StatType statType in Enum.GetValues(typeof(StatType)))
                    {
                        unit.Stats.Add(new Stat(statType, Random.Range(20, 100)));
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