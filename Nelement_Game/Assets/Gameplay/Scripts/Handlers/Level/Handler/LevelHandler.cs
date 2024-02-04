using UnityEngine;

using ProyectNelement.Gameplay.Controllers.GameCamera;
using ProyectNelement.Gameplay.Controllers.Player;

namespace ProyectNelement.Gameplay.Controllers.Level
{
    public class LevelHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private CameraController cameraController = null;
        [SerializeField] private PlayerController playerController = null;
        #endregion
    }
}