using TacticsCore.HexGrid;
using TacticsCore.Units;
using UnityEngine;

namespace TacticsCore.Data
{
    public abstract class AbstractUnitSO : ScriptableObject
    {
        [field: SerializeField, Range(PathfindingHex.MOVE_STRAIGHT_COST, PathfindingHex.MOVE_STRAIGHT_COST * 30)] 
        public int MovePoints { get; private set; } = 64;
        [field: SerializeField, Range(0, 100)]
        public float Health { get; set; } = 100;
        

        [SerializeField] public string characterName;
        [SerializeField] public int level;
        [SerializeField] public Sprite icon;
        [SerializeField] public bool isLeader;
        [SerializeField] public int initiative;
        [SerializeField] public Owner owner;

        public virtual void Initialize(string name, int level, Sprite icon, BattleUnitPosition battleUnitPosition,
            bool isLeader, int initiative)
        {
            this.characterName = name;
            this.level = level;
            this.icon = icon;
            this.isLeader = isLeader;
            this.initiative = initiative;
        }

        public override string ToString()
        {
            return 
                $" Name {characterName}" 
                + $" Lv. {level}"
                + $" Icon {(icon ? icon.name : " none ")}"
                + $" Initiative {initiative}"
                + " Leader " + isLeader;
        }
    }
}