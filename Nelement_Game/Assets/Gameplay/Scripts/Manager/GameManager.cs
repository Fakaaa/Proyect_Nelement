using ProyectNelement.Gameplay.Level.Handler;
using ProyectNelement.Gameplay.Player.Handler;
using ProyectNelement.Gameplay.GameCamera.Handler;
using ProyectNelement.Gameplay.Player.Interactions;

using UnityEngine;

namespace ProyectNelement.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("LEVEL")]
        [SerializeField] private LevelHandler levelHandler = null;

        [Header("CONTROLLERS")] 
        [SerializeField] private CameraController cameraController = null;
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private PlayerInteractions playerInteractions = null;

        [Header("-PLAYER-")] 
        [SerializeField] private int playerMaxHp = 1; 
        #endregion

        #region PRIVATE_FIELDS
        private bool stageInitialized = false;
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            playerController.Init();
            cameraController.Init(playerController);
            
            playerInteractions.Init(playerMaxHp,
                onDie: () =>
                {
                    
                },
                onRespawn: () =>
                {
                    
                });
            
            levelHandler.Init(cameraController, playerInteractions,
                onSuccess: () =>
                {
                    stageInitialized = true;
                    Debug.Log("LevelHandler has load correctly the Stages.");
                    
                    playerController.ToggleController(true);
                },
                onFailure: () =>
                {
                    Debug.LogError("Fail to initialize Game.");
                });
        }

        private void Update()
        {
            if(!stageInitialized)
                return;
            
            playerController.UpdatePlayer();
            cameraController.UpdateCamera();
        }
        #endregion
    }
}