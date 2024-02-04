using System;

using ProyectNelement.Gameplay.Controllers.Level;
using ProyectNelement.Gameplay.Controllers.Player;
using ProyectNelement.Gameplay.Controllers.GameCamera;

using UnityEngine;

namespace ProyectNelement.Gameplay.Controllers.GameManager
{
    public class GameManager : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("LEVEL")]
        [SerializeField] private LevelHandler levelHandler = null;

        [Header("CONTROLLERS")] 
        [SerializeField] private CameraController cameraController = null;
        [SerializeField] private PlayerController playerController = null;
        #endregion

        #region PRIVATE_FIELDS
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            playerController.Init();
            cameraController.Init(playerController);
            levelHandler.Init(cameraController, playerController);
        }

        private void Update()
        {
            playerController.UpdatePlayer();
            cameraController.UpdateCamera();
        }
        #endregion
    }
}