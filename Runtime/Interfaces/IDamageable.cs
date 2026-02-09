using TacticsCore.Data;
using Slider = UnityEngine.UI.Slider;

namespace TacticsCore.Interfaces
{
    public interface IDamageable
    {
        public Slider HealthBar { get; }
        public Owner Owner { get; set; }
        
        public void TakeDamage(float damage);
    }
}