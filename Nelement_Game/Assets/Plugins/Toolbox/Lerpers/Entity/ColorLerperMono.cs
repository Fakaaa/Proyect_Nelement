using System;
using UnityEngine;

namespace ProyectNelement.Toolbox.Lerpers
{
    public class ColorLerperMono : AbstractLerperMono<Color>
    {
        #region PRIVATE_FIELDS
        private ColorLerper lerper = null;
        #endregion

        #region PROPERTIES
        private ColorLerper Lerper
        {
            get
            {
                if (lerper == null)
                {
                    lerper = new ColorLerper(lerpTime, null, smoothType, timeType);
                }

                return lerper;
            }
        }
        #endregion

        #region UNITY_CALLS
        protected override void Update()
        {
            Lerper.Update();
        }
        #endregion

        #region OVERRIDE
        public override void SetValues(Color start, Color end, bool on = false, Action onReached = null)
        {
            Lerper.SetValues(start, end, on, onReached);
        }

        public override void SwitchState(bool status)
        {
            Lerper.SwitchState(status);
        }

        public override void SetUpdateAction(Action<Color> onUpdated)
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
