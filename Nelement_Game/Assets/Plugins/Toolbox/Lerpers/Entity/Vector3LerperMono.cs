using System;
using UnityEngine;

namespace ProyectNelement.Toolbox.Lerpers
{
    public class Vector3LerperMono : AbstractLerperMono<Vector3>
    {
        #region PRIVATE_FIELDS
        private Vector3Lerper lerper = null;
        #endregion

        #region PROPERTIES
        private Vector3Lerper Lerper
        {
            get
            {
                if (lerper == null)
                {
                    lerper = new Vector3Lerper(lerpTime, null, smoothType, timeType);
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
        public override void SetValues(Vector3 start, Vector3 end, bool on = false, Action onReached = null)
        {
            Lerper.SetValues(start, end, on, onReached);
        }

        public override void SwitchState(bool status)
        {
            Lerper.SwitchState(status);
        }

        public override void SetUpdateAction(Action<Vector3> onUpdated)
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