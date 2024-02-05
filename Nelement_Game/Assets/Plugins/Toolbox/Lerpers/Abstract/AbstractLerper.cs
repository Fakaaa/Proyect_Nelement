using System;
using UnityEngine;

namespace ProyectNelement.Toolbox.Lerpers
{
    public abstract class AbstractLerper<T>
    {
        #region PRIVATE_FIELDS
        private float lerpTime = 1.0f;
        private float currentLerpTime = 0.0f;

        private SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE;
        private TIME_TYPE timeType = TIME_TYPE.DELTA;
        #endregion

        #region PROTECTED_FIELDS
        protected T start = default;
        protected T end = default;

        protected T currentValue = default;
        protected bool on = false;
        #endregion

        #region ENUMS
        public enum SMOOTH_TYPE
        {
            NONE,
            EASE_IN,
            EASE_OUT,
            EXPONENTIAL,
            STEP_SMOOTH,
            STEP_SMOOTHER
        }

        public enum TIME_TYPE
        {
            DELTA,
            UNSCALED
        }
        #endregion

        #region ACTIONS
        private Action<T> OnUpdated = null;
        private Action OnReached = null;
        #endregion

        #region CONSTRUCTORS
        public AbstractLerper(float lerpTime, Action<T> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
        {
            Init(lerpTime, smoothType, timeType, onUpdate);
        }

        public AbstractLerper(T start, T end, float lerpTime, Action<T> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
        {
            Init(lerpTime, smoothType, timeType, onUpdate);
            this.start = start;
            this.end = end;
        }
        #endregion

        #region PUBLIC METHODS
        public void Update()
        {
            if (!on) 
            {
                return;
            }

            currentLerpTime += GetTimeScaled();
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float perc = currentLerpTime / lerpTime;
            perc = SmoothLerp(perc, smoothType);

            UpdateCurrentValue(perc);
            OnUpdated?.Invoke(currentValue);

            if (CheckReached())
            {
                SwitchState(false);
                OnReached?.Invoke();
            }
        }

        public void SetValues(T start, T end, bool on = false, Action onReached = null)
        {
            this.start = start;
            this.end = end;
            this.on = on;
            OnReached = onReached;
            Reset();
        }

        public void SetLerpTime(float time)
        {
            lerpTime = time;
        }

        public void SwitchState(bool on)
        {
            this.on = on;
        }

        public void SetUpdateAction(Action<T> onUpdate)
        {
            OnUpdated = onUpdate;
        }

        public float GetCurrentPerc()
        {
            return currentLerpTime / lerpTime;
        }

        public bool IsActive()
        {
            return on;
        }

        public void SetSmoothType(SMOOTH_TYPE mode)
        {
            smoothType = mode;
        }
        #endregion

        #region PROTECTED METHODS
        protected void Init(float lerpTime, SMOOTH_TYPE smoothType, TIME_TYPE timeType)
        {
            this.lerpTime = lerpTime;
            this.smoothType = smoothType;
            this.timeType = timeType;
        }

        protected void Init(float lerpTime, SMOOTH_TYPE smoothType, TIME_TYPE timeType, Action<T> onUpdate)
        {
            this.lerpTime = lerpTime;
            this.smoothType = smoothType;
            this.timeType = timeType;
            OnUpdated = onUpdate;
        }

        protected abstract void UpdateCurrentValue(float perc);

        protected void Reset()
        {
            currentLerpTime = 0.0f;
            UpdateCurrentValue(0.0f);
        }
        #endregion

        #region PRIVATE_METHODS
        private float SmoothLerp(float value, SMOOTH_TYPE mode)
        {
            float smooth = mode switch
            {
                SMOOTH_TYPE.NONE => value,
                SMOOTH_TYPE.EASE_IN => 1f - Mathf.Cos(value * Mathf.PI * 0.5f),
                SMOOTH_TYPE.EASE_OUT => Mathf.Sin(value * Mathf.PI * 0.5f),
                SMOOTH_TYPE.EXPONENTIAL => value * value,
                SMOOTH_TYPE.STEP_SMOOTH => value * value * (3f - 2f * value),
                SMOOTH_TYPE.STEP_SMOOTHER => value * value * value * (value * (6f * value - 15f) + 10f),
                _ => throw new System.NotImplementedException(),
            };

            return smooth;
        }

        private float GetTimeScaled()
        {
            float time = timeType switch
            {
                TIME_TYPE.DELTA => UnityEngine.Time.deltaTime,
                TIME_TYPE.UNSCALED => UnityEngine.Time.unscaledDeltaTime,
                _ => throw new System.NotImplementedException(),
            };

            return time;
        }

        private bool CheckReached()
        {
            return currentLerpTime >= lerpTime;
        }
        #endregion
    }

    public class FloatLerper : AbstractLerper<float>
    {
        #region CONSTRUCTORS
        public FloatLerper(float lerpTime, Action<float> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
            : base(lerpTime, onUpdate, smoothType, timeType) { }
        public FloatLerper(float start, float end, float lerpTime, Action<float> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
            : base(start, end, lerpTime, onUpdate, smoothType, timeType) { }
        #endregion

        #region OVERRIDE
        protected override void UpdateCurrentValue(float perc)
        {
            currentValue = Mathf.Lerp(start, end, perc);
        }
        #endregion
    }

    public class Vector2Lerper : AbstractLerper<Vector2>
    {
        #region CONSTRUCTORS
        public Vector2Lerper(float lerpTime, Action<Vector2> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
            : base(lerpTime, onUpdate, smoothType, timeType) { }
        public Vector2Lerper(Vector2 start, Vector2 end, float lerpTime, Action<Vector2> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
            : base(start, end, lerpTime, onUpdate, smoothType, timeType) { }
        #endregion

        #region OVERRIDE
        protected override void UpdateCurrentValue(float perc)
        {
            currentValue = Vector2.Lerp(start, end, perc);
        }
        #endregion
    }

    public class Vector3Lerper : AbstractLerper<Vector3>
    {
        #region CONSTRUCTORS
        public Vector3Lerper(float lerpTime, Action<Vector3> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA) 
            : base(lerpTime, onUpdate, smoothType, timeType) { }
        public Vector3Lerper(Vector3 start, Vector3 end, float lerpTime, Action<Vector3> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA) 
            : base(start, end, lerpTime, onUpdate, smoothType, timeType) { }
        #endregion

        #region OVERRIDE
        protected override void UpdateCurrentValue(float perc)
        {
            currentValue = Vector3.Lerp(start, end, perc);
        }
        #endregion
    }

    public class ColorLerper : AbstractLerper<Color>
    {
        #region CONSTRUCTORS
        public ColorLerper(float lerpTime, Action<Color> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
            : base(lerpTime, onUpdate, smoothType, timeType) { }
        public ColorLerper(Color start, Color end, float lerpTime, Action<Color> onUpdate, SMOOTH_TYPE smoothType = SMOOTH_TYPE.NONE, TIME_TYPE timeType = TIME_TYPE.DELTA)
            : base(start, end, lerpTime, onUpdate, smoothType, timeType) { }
        #endregion

        #region OVERRIDE
        protected override void UpdateCurrentValue(float perc)
        {
            currentValue = Color.Lerp(start, end, perc);
        }
        #endregion
    }
}
