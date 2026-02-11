using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TacticsCore.Editor
{
    [CustomPropertyDrawer(typeof(Unit))]
    public class UnitPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new Foldout();
            container.text = property.displayName;
            
            var nameField = new PropertyField(property.FindPropertyRelative("<Name>k__BackingField"), "Unit Name");
            var iconField = new PropertyField(property.FindPropertyRelative("<Icon>k__BackingField"), "Unit Icon");
            var statsField = new PropertyField(property.FindPropertyRelative("<Stats>k__BackingField"), "Unit Stats");

            container.Add(nameField);
            container.Add(iconField);
            container.Add(statsField);

            return container;
        }
    }

    [CustomPropertyDrawer(typeof(UnitGroup))]
    public class UnitGroupPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new Foldout();
            container.text = property.displayName;

            var nameField = new PropertyField(property.FindPropertyRelative("<Name>k__BackingField"), "Group Name");
            var unitsField = new PropertyField(property.FindPropertyRelative("units"), "Units");

            container.Add(nameField);
            container.Add(unitsField);

            return container;
        }
    }
}