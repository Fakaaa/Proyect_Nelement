using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using ProyectNelement.Toolbox.Lerpers;

namespace ProyectNelement.Gameplay.Level.Loading.View
{
    public class LoadingView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private float fadeSpeed = .15f;
        [SerializeField] private Image fadeView = null;
        #endregion

        #region PRIVATE_FIELDS
        private FloatLerper floatLerper = null;
        #endregion
        
        #region PUBLIC_METHODS
        public void Init()
        {
            floatLerper = new FloatLerper(fadeSpeed, UpdateAlphaImage, AbstractLerper<float>.SMOOTH_TYPE.EXPONENTIAL);
        }
        
        public void ExecuteTransition(bool state, Action onCompleted)
        {
            StartCoroutine(AnimateTransition(state, onCompleted));
        }
        #endregion

        #region PRIVATE_METHODS
        private void UpdateAlphaImage(float newValue)
        {
            Color lastColor = fadeView.color;
            fadeView.color = new Color(lastColor.r, lastColor.g, lastColor.b, newValue);
        }
        #endregion
        
        #region COROUTINES
        private IEnumerator AnimateTransition(bool state, Action onComplete)
        {
            float targetValue = state ? 0 : 1;
            float startValue = state ? 1 : 0;
            
            floatLerper.SetValues(startValue, targetValue, true);
            
            while (floatLerper.IsActive())
            {
                floatLerper.Update();
                yield return null;
            } 
            
            onComplete.Invoke();
        }
        #endregion
    }
}