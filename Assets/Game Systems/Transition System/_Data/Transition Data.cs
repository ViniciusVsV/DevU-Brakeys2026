using DG.Tweening;
using UnityEngine;

namespace TransitionSystem
{
    [CreateAssetMenu(fileName = "TransitionData", menuName = "Scriptable Objects/TransitionData")]
    public class TransitionData : ScriptableObject
    {
        [Header("Scene Enter")]
        public float enterDuration;
        public Ease enterEase;
        public TransitionDirection enterDirection;

        [Header("Scene ReEnter")]
        public float reRenterDuration;
        public Ease reRenterEase;
        public TransitionDirection reEnterDirection;

        [Header("Scene Fail")]
        public float failStartDelay;
        public float failDuration;
        public Ease failEase;
        public TransitionDirection failDirection;

        [Header("Scene Exit")]
        public float exitDuration;
        public Ease exitEase;
        public TransitionDirection exitDirection;

        public Vector2 GetDirectionVector(TransitionDirection transitionDirection)
        {
            return transitionDirection switch
            {
                TransitionDirection.Up => Vector3.up,
                TransitionDirection.Down => Vector3.down,
                TransitionDirection.Left => Vector3.left,
                TransitionDirection.Right => Vector3.right,

                _ => Vector3.zero
            };
        }
    }

    public enum TransitionDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}