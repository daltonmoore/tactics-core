using TacticsCore.Data;
using TacticsCore.Interfaces;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace TacticsCore.Units
{
    public abstract class AbstractUnit : AbstractCommandable
    {
        [SerializeField] private AnimationConfigSO animationConfig;
        [SerializeField] public GameObject deathPrefab;

        public new virtual UnitSO UnitSO { get; set; }


        protected override void Awake()
        {
            base.Awake();
            
            UnitSO = base.UnitSO as UnitSO;
        }
    }
    
    public enum BattleUnitPosition
    {
        BackBottom = 0,     // mod 3 is back to 0
        BackCenter = 1,     // mod 3 is 1
        BackTop = 2,         // mod 3 is 2
        FrontBottom = 3,    // mod 3 is 0
        FrontCenter = 4,    // mod 3 is 1
        FrontTop = 5,       // mod 3 is 2
    }
}