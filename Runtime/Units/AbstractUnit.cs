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
        BackBottom = 0,
        BackCenter = 1,
        BackTop = 2,
        FrontBottom = 3,
        FrontCenter = 4,
        FrontTop = 5,
    }
}