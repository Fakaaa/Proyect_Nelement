using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProyectNelement.Toolbox.Editor.CustomProperties;

namespace ProyectNelement.Gameplay.Controllers.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [Header("PLAYER MAIN SETTINGS")]
        [SerializeField, Tooltip("The actual speed of the player on the X coordinate"), DisplayWithoutEdit] 
        private float ActualSpeedX;
        [SerializeField, Tooltip("The value of acceleration that multiplies the speed for apply movement"), Range(50,250)] 
        private float Acceleration;
        [SerializeField, Tooltip("The value for decrease the player speed over time when start stop moving"),Range(5,15)] 
        private float AccelerationDamper;
        [SerializeField, Tooltip("The max speed when move on X coordinate")]
        private float MaxMoveSpeed;
        [SerializeField, Tooltip("The jump height of the normal jump"), Range(10,35)] 
        private float JumpHegight;
        [SerializeField, Tooltip("The gravity force that applies over the player"), Range(1,20)] 
        private float Gravity;
        [SerializeField, Tooltip("The layers that will be detected like ground and will check collision")] LayerMask collisionLayer = default;
        #endregion

        #region PRIVATE_FIELDS
        private Rigidbody2D rb = null;
        private CapsuleCollider2D collision = null;

        private float horizontalSpeed = 0;
        private float verticalSpeed = 0;
        private float lenghtCheckGround = 1.5f;

        private bool onGround = false;
        private bool isEnable = false;
        private bool changingDirection => ((rb.velocity.x > 0f && horizontalSpeed < 0f) || (rb.velocity.x < 0f && horizontalSpeed > 0f));
        #endregion

        #region ACTIONS
        #endregion

        #region PROPERTIES
        public bool IsEnable { get { return isEnable; } set { isEnable = value; } }
        #endregion

        #region UNITY_CALLS

        private void Start()
        {
            Init();

            isEnable = true;
        }

        private void Update()
        {
            UpdateInputs();
        }

        private void FixedUpdate()
        {
            MovePlayer();
            ApplyLinearDrag();
        }

        #endregion
        
        #region PUBLIC_METHODS
        public void Init()
        {
            rb = GetComponentInChildren<Rigidbody2D>();
            collision = GetComponentInChildren<CapsuleCollider2D>();

            InitializePhysics();
        }

        public void UpdateInputs()
        {
            if(!isEnable || !NeedsInitialized()) return;

            horizontalSpeed = GetInput().x;

            if (Input.GetButtonDown("Jump") && onGround) 
                Jump();
        }

        public void MovePlayer()
        {
            if(!isEnable || !NeedsInitialized()) return;

            rb.AddForce(new Vector2(horizontalSpeed, verticalSpeed) * Acceleration);
            
            if (Mathf.Abs(rb.velocity.x) > MaxMoveSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxMoveSpeed, rb.velocity.y);
            }
            
            ActualSpeedX = rb.velocity.magnitude;
        }
        #endregion

        #region PRIVATE_METHODS

        private void ApplyLinearDrag()
        {
            if (Mathf.Abs(horizontalSpeed) < 0.4f || changingDirection)
                rb.drag = AccelerationDamper;
            else
                rb.drag = 0;
        }
        
        private void InitializePhysics()
        {
            if (rb == null || collision == null) return;

            rb.gravityScale = Gravity;
        }

        private void Jump()
        {
            if(!CheckOnGround())
                return;

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * JumpHegight, ForceMode2D.Impulse);
        }

        private bool CheckOnGround()
        {
            return Physics2D.Raycast(rb.position, Vector2.down, lenghtCheckGround, collisionLayer);
        }
        
        private Vector2 GetInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") );
        }

        private bool NeedsInitialized()
        {
            return rb != null && collision != null;
        }
        #endregion
    }
}