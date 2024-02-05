using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AddressableAssets;

using ProyectNelement.Gameplay.Level.Model;
using ProyectNelement.Gameplay.GameCamera.Handler;
using ProyectNelement.Gameplay.Level.Loading.View;
using ProyectNelement.Gameplay.Player.Interactions;

using ProyectNelement.System.AddressablesHandeling;

namespace ProyectNelement.Gameplay.Level.Handler
{
    public class LevelHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("Others")]
        [SerializeField] private Transform holder = null;
        //Temporal to debug
        [SerializeField] private int currentStageToLoad = 0;
        [Header("Stages")]
        [SerializeField] private AssetLabelReference stagesLabel = null;
        [SerializeField] private LoadingView loadingView = null;
        #endregion
        
        #region PRIVATE_FIELDS
        private CameraController cameraController = null;
        private PlayerInteractions playerInteractions = null;

        private GameObject currentLevelGo = null;
        
        private List<StageData> stages = null;
        #endregion

        #region PUBLIC_METHODS
        public void Init(CameraController cameraController, PlayerInteractions playerInteractions, Action onSuccess, Action onFailure)
        {
            loadingView.Init();
            
            this.cameraController = cameraController;
            this.playerInteractions = playerInteractions;
            
            MakeTransition(false, 
                onComplete: () =>
                {
                    LoadStages(
                        onSuccess: () =>
                        {
                            InstantiateCurrentStage(
                                onComplete: () =>
                                {
                                    MakeTransition(true, onSuccess);
                                });
                        },
                        onFailure);
                });
        }
        #endregion

        #region PRIVATE_METHODS
        private void MakeTransition(bool status, Action onComplete)
        {
            loadingView.ExecuteTransition(status, onComplete);
        }
        
        private void LoadStages(Action onSuccess, Action onFailure)
        {
            AddressableManager.LoadAssetsByLabel<StageData>(stagesLabel.labelString, 
                onSuccess: (result) =>
                {
                    stages = result;
                    onSuccess.Invoke();
                },
                onFailure: () =>
                {
                    onFailure.Invoke();
                    Debug.LogError("Fail to load stages on Level Handler.");
                });
        }

        private void InstantiateCurrentStage(Action onComplete)
        {
            StageData currentStage = stages.Find(stage => stage.Index == currentStageToLoad);
            AddressableManager.LoadAsset<GameObject>(currentStage.LevelReference, "stage_" + currentStage.Index,
                onSuccess: (prefabGo) =>
                {
                    currentLevelGo = Instantiate(prefabGo, holder);
                    onComplete.Invoke();
                },
                onFailure: () =>
                {
                    Debug.LogError("Fail to load level prefab.");
                });
        }
        #endregion
    }
}