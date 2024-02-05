namespace ProyectNelement.Common.Interfaces.Entities
{
    public interface IDamageable
    {
        #region PROPERTIES
        public bool State { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        #endregion
        
        #region PUBLIC_METHODS
        public void OnHit();
        public void OnKillInstant();
        #endregion
    }
}