using System.Collections.Generic;
using TacticsCore.Data;
using UnityEngine;

namespace TacticsCore.Interfaces
{
    public interface IAttackable
    {
        public Transform Transform { get; }
        public List<UnitSO> PartyList { get; }
    }
}