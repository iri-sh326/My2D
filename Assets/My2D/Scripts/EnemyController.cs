using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace My2D
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        private TouchingDirections touchingDirections;
        // 플레이어 감지
        public DetectionZone detectionZone;

        // 이동속도
        [SerializeField] private float runSpeed = 4f;
        // 이동방향
        private Vector2 directionVector = Vector2.right;

        // 이동 가능 방향
        public enum WalkableDirection { Left, Right }

        // 현재 이동 방향
        private WalkableDirection walkDirection = WalkableDirection.Right;
        public WalkableDirection WalkDirection
        {
            get { return walkDirection; }
            private set
            {
                // 이미지 플립
                transform.localScale *= new Vector2(-1, 1);

                // 이동하는 방향값
                if (value == WalkableDirection.Left)
                {
                    directionVector = Vector2.left;
                }
                else if (value == WalkableDirection.Right)
                {
                    directionVector = Vector2.right;
                }
                walkDirection = value;
            }
        }

        // 공격 타겟 설정
        [SerializeField] private bool hasTarget = false;
        public bool HasTarget
        {
            get { return hasTarget; }
            private set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.HasTarget, value);
            }
        }

        // 이동 가능 상태 / 불가능 상태 - 이동 제한
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
        }

        // 감속 계수
        [SerializeField] private float stopRate = 0.2f;
        #endregion

        private void Awake()
        {
            // 참조
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();
        }

        private void Update()
        {
            // 적 감지 충돌체의 리스트 갯수가 0보다 크면 적이 감지된 것이다
            HasTarget = (detectionZone.detectedColliders.Count > 0);
        }

        private void FixedUpdate()
        {   
            // 땅에서 이동시 벽을 만나면 방향 전환
            if (touchingDirections.IsWall && touchingDirections.IsGround)
            {
                // 방향 전환
                Flip();
            }

            if (CanMove)
            {
                rb2D.velocity = new Vector2(directionVector.x * runSpeed, rb2D.velocity.y);
            }
            else
            {
                //rb2D.velocity.x -> 0 : Lerp
                rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, 0f, stopRate), rb2D.velocity.y);
            }

        }

        // 방향 전환
        private void Flip()
        {
            if(WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if(WalkDirection == WalkableDirection.Right)
            {
                WalkDirection= WalkableDirection.Left;
            }
            else
            {
                Debug.Log("Error Flip Direciton");
            }
        }
    }

}

