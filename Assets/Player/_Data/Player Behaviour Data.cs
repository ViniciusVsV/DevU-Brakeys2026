using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerBehaviourData", menuName = "Scriptable Objects/Player/BehaviourData")]
    public class PlayerBehaviourData : ScriptableObject
    {
        [Header("Movement")]
        public float moveSpeed;

        [Header("Gravity")]
        public float baseGravity;
        public float fallGravity;

        [Header("Jump")]
        public float jumpForce;
        [Range(0, 1)] public float jumpCutMultiplier;
        public int extraJumps;
        public float coyoteTime;
        public float jumpBuffer;

        [Header("Dash")]
        public float dashCooldown;
        public float dashSpeed;
        public float dashDuration;

        [Header("Death")]
        public string[] hostileTags;
    }
}