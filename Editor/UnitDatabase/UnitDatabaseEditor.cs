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
        [SerializeField] private VisualTreeAsset visualTreeAsset;
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new();
            
            Button button = new(InitializeUnitStats)
            {
                text = "Init Stats"
            };
            inspector.Add(button);

            // Manually add the unitGroups field to apply styling
            var unitGroupsProp = serializedObject.FindProperty("unitGroups");
            if (unitGroupsProp != null)
            {
                // Create a container to hold the property field and apply styling to the container
                VisualElement container = new VisualElement();
                container.style.minHeight = 400; 
                container.style.flexGrow = 1;

                PropertyField unitGroupsField = new(unitGroupsProp);
                container.Add(unitGroupsField);
                
                inspector.Add(container);
            }
        
            if (visualTreeAsset != null)
            {
                VisualElement uxmlContent = visualTreeAsset.CloneTree();
                inspector.Add(uxmlContent);
            }
            
            // Use binding path to avoid duplication if using FillDefaultInspector
            // Or just don't call FillDefaultInspector if we've handled everything.
            // Since UnitDatabase only has unitGroups, we can skip FillDefaultInspector
            // if we want to be sure there's no duplication.
            // InspectorElement.FillDefaultInspector(inspector, serializedObject, this);
        
            return inspector;
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
        
        // private void OnValidate()
        // {
        //     InitializeUnitStats();
        // }
    }
}