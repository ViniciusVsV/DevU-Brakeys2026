using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteController : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer sr;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }

        public void Flip()
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        public void SetMovementValues(float xSpeed, float ySpeed)
        {
            animator.SetFloat("xSpeed", xSpeed);
            animator.SetFloat("ySpeed", ySpeed);
        }

        public void SetJumpBoolean(bool isJumping)
        {
            animator.SetBool("isJumping", isJumping);
        }

        public void TriggerDeathAnimation()
        {
            animator.SetTrigger("hasDied");
        }

        public void DisableSprite()
        {
            sr.enabled = false;
        }

        public void SetUnscaledTime()
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        public SpriteRenderer GetSpriteRenderer() { return sr; }
    }
}