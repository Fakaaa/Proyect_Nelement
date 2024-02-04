using UnityEngine;

using ProyectNelement.Gameplay.Controllers.GameCamera;
using ProyectNelement.Gameplay.Controllers.Player;

namespace ProyectNelement.Gameplay.Controllers.Level
{
    public class LevelHandler : MonoBehaviour
    {
        #region PRIVATE_FIELDS
        private CameraController cameraController = null;
        private PlayerController playerController = null;
        #endregion

        #region PUBLIC_METHODS
        public void Init(CameraController cameraController, PlayerController playerController)
        {
            this.cameraController = cameraController;
            this.playerController = playerController;
        }
        #endregion
    }
}