using System;

namespace ProyectNelement.Toolbox.Lerpers
{
    public class FloatLerperMono : AbstractLerperMono<float>
    {
        #region PRIVATE_FIELDS
        private FloatLerper lerper = null;
        #endregion

        #region PROPERTIES
        private FloatLerper Lerper
        {
            get
            {
                if (lerper == null)
                {
                    lerper = new FloatLerper(lerpTime, null, smoothType, timeType);
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
        public override void SetValues(float start, float end, bool on = false, Action onReached = null)
        {
            Lerper.SetValues(start, end, on, onReached);
        }

        public override void SwitchState(bool status)
        {
            Lerper.SwitchState(status);
        }

        public override void SetUpdateAction(Action<float> onUpdated)
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
