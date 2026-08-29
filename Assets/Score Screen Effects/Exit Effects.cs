using System;
using UnityEngine;

namespace ScoreScreenEffects
{
    public class ExitEffects : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;

        public static event Action<string> OnSceneExit;

        public void ApplyEffect()
        {
            OnSceneExit?.Invoke(nextSceneName);
        }
    }
}