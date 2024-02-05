using UnityEngine;

using ProyectNelement.Common.Interfaces.Entities;

namespace ProyectNelement.Gameplay.Level.Avoidables
{
    public class KillZone : MonoBehaviour
    {
        #region UNITY_CALLS
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.OnKillInstant();  
            }
        }
        #endregion
    }
}