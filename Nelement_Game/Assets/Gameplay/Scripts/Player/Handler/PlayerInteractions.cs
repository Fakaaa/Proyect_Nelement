using System;

using UnityEngine;

using ProyectNelement.Common.Interfaces.Entities;

using ProyectNelement.Gameplay.Player.Views;
using ProyectNelement.Gameplay.Player.Handler;

namespace ProyectNelement.Gameplay.Player.Interactions
{
    public class PlayerInteractions : MonoBehaviour, IDamageable
    {
        #region PROPERTIES
        public bool State { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        #endregion

        #region PRIVATE_FIELDS
        private Action onDie = null;
        private Action onRespawn = null;

        private PlayerController playerController = null;
        private PlayerView playerView = null;
        #endregion

        #region PUBLIC_METHODS
        public void Init(int playerMaxHealth, Action onDie, Action onRespawn)
        {
            MaxHealth = playerMaxHealth;
            CurrentHealth = MaxHealth;
            
            this.onDie = onDie;
            this.onRespawn = onRespawn;

            playerController = GetComponent<PlayerController>();
            playerView = GetComponentInChildren<PlayerView>();

            this.onDie += () =>
            {
                playerController.ToggleController(false);
                playerView.ToggleView(false);
            };

            this.onRespawn += () =>
            {
                playerView.ToggleView(true);
                playerController.ToggleController(true);
            };
        }

        public void RestoreHealth()
        {
            CurrentHealth = MaxHealth;
            onRespawn.Invoke();
        }
        #endregion
        
        #region INTERFACES
        public void OnHit()
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth--;
            }

            if (CurrentHealth <= 0)
            {
                onDie.Invoke();
            }
        }

        public void OnKillInstant()
        {
            CurrentHealth = 0;
            onDie.Invoke();
        }
        #endregion
    }
}