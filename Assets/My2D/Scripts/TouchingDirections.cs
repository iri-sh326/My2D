using My2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class TouchingDirections : MonoBehaviour
    {
        #region Variables
        private CapsuleCollider2D touchingCollider;
        private Animator animator;

        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private float ceilingDistance = 0.05f;
        [SerializeField] private float wallDistance = 0.2f;

        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];

        [SerializeField] private bool isGround;
        public bool IsGround
        {
            get { return isGround; }
            private set
            {
                isGround = value;
                animator.SetBool(AnimationString.IsGround, value);
            }
        }

        [SerializeField] private bool isCeiling;
        public bool IsCeiling
        {
            get { return isCeiling; }
            private set
            {
                isCeiling = value;
                animator.SetBool(AnimationString.IsCeiling, value);
            }
        }

        [SerializeField] private bool isWall;
        public bool IsWall
        {
            get { return isWall; }
            private set
            {
                isWall = value;
                animator.SetBool(AnimationString.IsWall, value);
            }
        }

        private Vector2 WalkDirection => (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
        #endregion

        private void Awake()
        {
            // ����
            touchingCollider = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            IsGround = (touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0);
            IsCeiling = (touchingCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0);
            IsWall = (touchingCollider.Cast(WalkDirection, contactFilter, wallHits, wallDistance) > 0);
        }
    }
}
