using System;

using UnityEngine;

using ProyectNelement.Gameplay.Controllers.Player;

namespace ProyectNelement.Gameplay.Views.Player
{
    public class PlayerView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("REFERENCES")]
        [SerializeField] private SpriteRenderer view = null;
        [SerializeField] private Animator animator = null;
        [Header("EFFECTS")] 
        [SerializeField] private float tiltWhenMove = 4f;
        [SerializeField] private float tiltSpeed = 4f;
        [SerializeField] private float distanceBetweenImages = 4f;
        #endregion

        #region PRIVATE_FIELDS
        private float lastImagePosX = 0;
        private float lastImagePosY = 0;
        #endregion
        
        #region PUBLIC_METHODS
        public void TiltView(LastDirection direction)
        {
            Quaternion lastRotation = transform.rotation;
            Vector3 rot = default;
            if (direction != LastDirection.None)
            {
                rot = new Vector3(lastRotation.x, lastRotation.y, direction == LastDirection.Right ? -tiltWhenMove : tiltWhenMove);
            }
            else
            {
                rot = Vector3.zero;
            }
            transform.rotation = Quaternion.Lerp(lastRotation, Quaternion.Euler(rot), Time.deltaTime * tiltSpeed);
        }

        public void SetFaceDirection(LastDirection direction)
        {
            view.flipX = direction == LastDirection.Right;
        }
        
        public void SetIdleState()
        {
            animator.SetTrigger("Idle");
        }
        
        public void SetWalkingState(float speed)
        {
            animator.SetFloat("Walking", speed);
        }

        public void BeginJump()
        {
            animator.SetTrigger("BeginJump");
        }
        
        public void JumpLoop()
        {
            if (animator.GetBool("OnGround"))
            {
                animator.SetBool("OnGround", false);
            }
        }
        
        public void EndJump(bool highFall = false, Action onHighFall = null)
        {
            if (!animator.GetBool("OnGround"))
            {
                animator.SetBool("OnGround", true);
            }

            if (highFall)
            {
                animator.SetTrigger("HighFall");
                onHighFall?.Invoke();
            }
        }
        #endregion
    }
}