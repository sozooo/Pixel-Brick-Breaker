using UnityEngine;

namespace Project.Scripts.Utils.Interfaces
{
    public interface IDamageable
    {
        public void ApplyDamage(Vector2 point, float radius);
    }
}