using DG.Tweening;
using UnityEngine;

namespace TransitionSystem
{
    [CreateAssetMenu(fileName = "TransitionData", menuName = "Scriptable Objects/TransitionData")]
    public class TransitionData : ScriptableObject
    {
        public Material transitionShaderMaterial;

        [Header("Scene Enter")]
        public float enterDuration;
        public Ease enterEase;
        public Texture2D enterTexture;

        [Header("Scene ReEnter")]
        public float reRenterDuration;
        public Ease reRenterEase;
        public Texture2D reRenterTexture;

        [Header("Scene Fail")]
        public float failStartDelay;
        public float failDuration;
        public Ease failEase;
        public Texture2D failTexture;

        [Header("Scene Exit")]
        public float exitDuration;
        public Ease exitEase;
        public Texture2D exitTexture;
    }
}