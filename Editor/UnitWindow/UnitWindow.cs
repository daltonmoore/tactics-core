using System;
using System.Collections.Generic;
using TacticsCore.Data;
using TacticsCore.Units;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TacticsCore.Editor
{
    public class UnitWindow : EditorWindow
    {
        [SerializeField] protected VisualTreeAsset uxmlAsset;
        [SerializeField] protected UnitDatabase unitDatabase; 
        
        public Action<IUnitOrGroup> OnSelectedUnitForPartyCallback;

        // Expresses unit data as a list of the units themselves. Needed for ListView and MultiColumnView.
        protected List<Unit> Units
        {
            get
            {
                var retVal = new List<Unit>();
                foreach (UnitGroup group in unitDatabase.unitGroups)
                {
                    retVal.AddRange(group.units);
                }
                
                return retVal;
            }
        }
        
        // Expresses unit data as a list of TreeViewItemData objects. Needed for TreeView and MultiColumnTreeView.
        protected IList<TreeViewItemData<IUnitOrGroup>> TreeRoots
        {
            get
            {
                int id = 0;
                var roots = new List<TreeViewItemData<IUnitOrGroup>>(unitDatabase.unitGroups.Count);
                foreach (var group in unitDatabase.unitGroups)
                {
                    var unitsInGroup = new List<TreeViewItemData<IUnitOrGroup>>(group.units.Count);
                    foreach (var unit in group.units)
                    {
                        unitsInGroup.Add(new TreeViewItemData<IUnitOrGroup>(id++, unit));
                    }

                    roots.Add(new TreeViewItemData<IUnitOrGroup>(id++, group, unitsInGroup));
                }
                return roots;
            }
        }
    }
    
    // Nested class that represents a group of units
    [Serializable]
    public class UnitGroup : IUnitOrGroup
    {
        [field: SerializeField]
        public string Name { get; set; }
        

        public bool Populated
        {
            get
            {
                var anyUnitPopulated = false;
                foreach (Unit unit in units)
                {
                    anyUnitPopulated = anyUnitPopulated || unit.Populated;
                }

                return anyUnitPopulated;
            }
        }

        public Sprite Icon { get; }
        public List<Stat> Stats { get; }

        [SerializeField]
        public List<Unit> units;

        public UnitGroup(string name, List<Unit> units, Sprite icon)
        {
            this.Name = name;
            this.units = units;
            this.Icon = icon;
        }
    }
    
    // Nested interface that can be either a single unit or a group of units.
    public interface IUnitOrGroup
    {
        public string Name { get; }

        public bool Populated { get; }
        
        public Sprite Icon { get; }

        public List<Stat> Stats { get; }

    }

    [Serializable]
    public class Unit : IUnitOrGroup
    {
        [field: SerializeField]
        public string Name { get; set; }
        
        [field: SerializeField]
        public Sprite Icon { get; set; }

        [field: SerializeField]
        public List<Stat> Stats { get; set; }

        public bool Populated { get; }

        public Unit(string name, bool populated, Sprite icon, BattleUnitPosition battleUnitPosition)
        {
            this.Name = name;
            this.Populated = populated;
            this.Icon = icon;
        }
    }
}
