using System;
using UnityEngine;

namespace ProyectNelement.Toolbox.Lerpers
{
    public abstract class AbstractLerperMono<T> : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] protected float lerpTime = 1.0f;
        [SerializeField] protected AbstractLerper<T>.SMOOTH_TYPE smoothType = AbstractLerper<T>.SMOOTH_TYPE.NONE;
        [SerializeField] protected AbstractLerper<T>.TIME_TYPE timeType = AbstractLerper<T>.TIME_TYPE.DELTA;
        #endregion

        #region UNITY_CALLS
        protected abstract void Update();
        #endregion

        #region ABSTRACT_METHODS
        public abstract void SetValues(T start, T end, bool on = false, Action onReached = null);
        public abstract void SwitchState(bool status);
        public abstract void SetLerpTime(float lerpTime);
        public abstract void SetUpdateAction(Action<T> onUpdated);
        public abstract float GetCurrentPerc();
        #endregion
    }
}
