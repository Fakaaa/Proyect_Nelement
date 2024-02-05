using System;
using UnityEngine;

namespace ProyectNelement.Toolbox.Lerpers
{
    public class Vector2LerperMono : AbstractLerperMono<Vector2>
    {
        #region PRIVATE_FIELDS
        private Vector2Lerper lerper = null;
        #endregion

        #region PROPERTIES
        private Vector2Lerper Lerper 
        {
            get 
            {
                if (lerper == null)
                {
                    lerper = new Vector2Lerper(lerpTime, null, smoothType, timeType);
                }

                return lerper;
            }        
        }
        public float LerpTime { get => lerpTime; }
        #endregion

        #region UNITY_CALLS
        protected override void Update()
        {
            Lerper.Update();
        }
        #endregion

        #region PUBLIC_METHODS
        public float GetLerpTime()
        {
            return lerpTime;
        }
        #endregion
        
        #region OVERRIDE
        public override void SetValues(Vector2 start, Vector2 end, bool on = false, Action onReached = null)
        {
            Lerper.SetValues(start, end, on, onReached);
        }

        public override void SwitchState(bool status)
        {
            Lerper.SwitchState(status);
        }

        public override void SetUpdateAction(Action<Vector2> onUpdated)
        {
            Lerper.SetUpdateAction(onUpdated);
        }

        public override void SetLerpTime(float lerpTime)
        {
            Lerper.SetLerpTime(lerpTime);
        }
        
        public override float GetCurrentPerc()
        {
            return Lerper.GetCurrentPerc();
        }
        #endregion
    }
}