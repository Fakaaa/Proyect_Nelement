using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectNelement.Gameplay.Controllers.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CapsuleCollider2D collision;
        #endregion
    }
}