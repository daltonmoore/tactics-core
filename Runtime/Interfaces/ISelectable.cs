using UnityEngine;

namespace TacticsCore.Interfaces
{
    public interface ISelectable
    {
        [field: SerializeField] Transform Transform { get; }
        
        bool IsSelected { get; }
        void Select();
        void Deselect();
    }
}