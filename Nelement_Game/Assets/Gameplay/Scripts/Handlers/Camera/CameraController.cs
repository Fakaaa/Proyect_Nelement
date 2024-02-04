using System;
using System.Collections;

using UnityEngine;

using ProyectNelement.Gameplay.Controllers.Player;

namespace ProyectNelement.Gameplay.Controllers.GameCamera
{
    public class CameraController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Transform target = null;
        [SerializeField] private float followSpeed = 0f;
        [SerializeField] private float offsetCamera = 0f;
        [SerializeField] private float offsetHeightCamera = 0f;
        #endregion

        #region PRIVATE_FIELDS
        private Vector3 targetPosition = default;
        private Camera mainCamera = null;
        private bool isAttachedToPlayer = true;
        private bool initialized = false;

        private PlayerController player = null;
        #endregion

        #region PROPERTIES
        public Camera MainCamera { get => mainCamera; }
        public bool IsAttachedToPlayer { get => isAttachedToPlayer; set => isAttachedToPlayer = value; }
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            if (!IsAttachedToPlayer)
                return;

            if (target == null)
                return;

            float apexFallSpeed = 0;
            if (player != null)
            {
                apexFallSpeed = player.Velocity.y;
                Debug.Log("Player Y Speed" + apexFallSpeed);
            }
            targetPosition = new Vector3(target.transform.position.x, target.transform.position.y + offsetHeightCamera, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        #endregion

        #region PUBLIC_METHODS
        public void Init(PlayerController player)
        {
            target = player.transform;
            this.player = player;

            mainCamera = GetComponent<Camera>();

            ToggleAttachToPlayer(true);
            
            initialized = true;
        }

        public void MoveCamera(Vector3 worldPosition, Action onMoveEnded = null)
        {
            StopAllCoroutines();

            StartCoroutine(LerpToPosition(worldPosition, onMoveEnded));
        }

        public void ToggleAttachToPlayer(bool state)
        {
            IsAttachedToPlayer = state;
        }
        #endregion

        #region CORUTINES
        private IEnumerator LerpToPosition(Vector3 worldPosition, Action onComplete = null)
        {
            float minDistance = 0.15f;

            Vector3 targetPosition = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

            while (Vector3.Distance(transform.position, targetPosition) > minDistance)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

                yield return null;
            }

            onComplete?.Invoke();
        }
        #endregion
    }
}