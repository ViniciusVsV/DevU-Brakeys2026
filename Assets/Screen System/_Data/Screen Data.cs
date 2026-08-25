using UnityEngine;

namespace ScreenSystem
{
    [CreateAssetMenu(fileName = "ScreenData", menuName = "Scriptable Objects/ScreenData")]
    public class ScreenData : ScriptableObject
    {
        public float transitionDuration;
    }
}