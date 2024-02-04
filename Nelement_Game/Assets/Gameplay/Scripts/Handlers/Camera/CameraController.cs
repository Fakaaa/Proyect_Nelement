using System;
using System.Collections;

using UnityEngine;

using ProyectNelement.Gameplay.Controllers.Player;

namespace ProyectNelement.Gameplay.Controllers.GameCamera
{
    public class CameraController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("GENERAL")]
        [SerializeField] private bool enable = false;
        [Header("MOVEMENT")]
        [Header("-CameraFollow-")]
        [SerializeField] private float followSpeed = 0f;
        [SerializeField] private float offsetHeightCamera = 0f;
        [Header("-PlayerTracking-")]
        [SerializeField] private float apexFallSpeedThreshold = 4f;
        #endregion

        #region PRIVATE_FIELDS
        private Transform target = null;
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

        #region PUBLIC_METHODS
        public void Init(PlayerController player)
        {
            target = player.transform;
            this.player = player;

            mainCamera = GetComponent<Camera>();

            ToggleAttachToPlayer(true);
            
            initialized = true;
            enable = true;
        }

        public void UpdateCamera()
        {
            if(!enable || !initialized)
                return;
            
            if (!IsAttachedToPlayer)
                return;

            if (target == null)
                return;

            Vector3 cameraPosition = transform.position;
            Vector3 targetPos = target.transform.position;
        
            targetPosition = new Vector3(targetPos.x + GetPlayerMoveSpeed(1.5f), targetPos.y + offsetHeightCamera + GetPlayerFallSpeed(apexFallSpeedThreshold), cameraPosition.z);
            transform.position = Vector3.Lerp(cameraPosition, targetPosition, followSpeed * Time.deltaTime);
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

        #region PRIVATE_METHODS
        private float GetPlayerFallSpeed(float thresholdValue = 0)
        {
            float fallSpeed = 0;

            if (player != null && player.RawMovement.y < 0)
            {
                fallSpeed = player.RawMovement.y / (thresholdValue > 0 ?  thresholdValue : 1);
                
                Debug.Log("Fall speed: " + fallSpeed);
            }
            
            return fallSpeed;
        }

        private float GetPlayerMoveSpeed(float thresholdValue)
        {
            float moveSpeed = 0;

            if (player != null)
            {
                moveSpeed = player.RawMovement.x / (thresholdValue > 0 ?  thresholdValue : 1);
            }
            
            return moveSpeed;
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