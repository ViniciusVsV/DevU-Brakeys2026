using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private PlayerBehaviourData playerBehaviourData;

        [Header("Player Scripts")]
        [SerializeField] private EffectsController effectsController;
        [SerializeField] private SpriteController spriteController;

        private Rigidbody2D rb;

        private int moveDirection;
        private int lastMoveDirection = 1;
        private int remainingJumps;
        private float jumpBufferTimer;
        private float coyoteTimer;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Vector2 groundCheckBoxSize;
        [SerializeField] private LayerMask groundLayerMask;
        private bool isGrounded;

        [Header("Booleans")]
        public bool canMove;
        public bool canJump;
        public bool canDash;
        public bool isDashing;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            canMove = true;
            canJump = true;
            canDash = true;
        }

        private void Update()
        {
            isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckBoxSize, 0f, groundLayerMask);

            coyoteTimer = isGrounded ? playerBehaviourData.coyoteTime : coyoteTimer - Time.deltaTime;
            remainingJumps = isGrounded ? playerBehaviourData.extraJumps : remainingJumps;

            jumpBufferTimer -= Time.deltaTime;
            Jump();

            float gravity = rb.linearVelocityY >= 0 ? playerBehaviourData.baseGravity : playerBehaviourData.fallGravity;
            rb.gravityScale = isDashing ? 0 : gravity;

            spriteController.SetMovementValues(Mathf.Abs(rb.linearVelocityX), rb.linearVelocityY);
            spriteController.SetJumpBoolean(!isGrounded);

            effectsController.PlayWalkEffects(isGrounded);
        }

        private void FixedUpdate()
        {
            if (canMove)
                rb.linearVelocityX = moveDirection * playerBehaviourData.moveSpeed;
        }

        public void Move(int moveDirection)
        {
            this.moveDirection = moveDirection;

            if (moveDirection != 0)
            {
                if (moveDirection != lastMoveDirection)
                    spriteController.Flip();

                lastMoveDirection = moveDirection;
            }
        }

        public void BufferJump()
        {
            jumpBufferTimer = playerBehaviourData.jumpBuffer;
        }
        public void Jump()
        {
            if (!canJump)
                return;

            bool hasCoyoteTime = coyoteTimer >= Mathf.Epsilon;
            bool hasRemainingJumps = remainingJumps > 0;

            if (jumpBufferTimer < Mathf.Epsilon || (!hasCoyoteTime && !hasRemainingJumps))
                return;

            if (!hasCoyoteTime)
                remainingJumps--;

            jumpBufferTimer = 0;

            rb.linearVelocityY = 0;
            rb.AddForce(playerBehaviourData.jumpForce * Vector2.up, ForceMode2D.Impulse);

            effectsController.PlayJumpEffects();
        }
        public void JumpCut()
        {
            if (rb.linearVelocityY > 0)
            {
                rb.linearVelocityY *= playerBehaviourData.jumpCutMultiplier;

                coyoteTimer = 0;
            }
        }

        public IEnumerator Dash()
        {
            if (!canDash)
                yield break;

            effectsController.ToggleDashEffects(true);

            canMove = false;
            isDashing = true;

            rb.linearVelocity = Vector2.zero;

            rb.linearVelocityX = playerBehaviourData.dashSpeed * lastMoveDirection;

            yield return new WaitForSeconds(playerBehaviourData.dashDuration);

            rb.linearVelocityX = 0;

            canMove = true;
            isDashing = false;

            effectsController.ToggleDashEffects(false);
        }
    }
}