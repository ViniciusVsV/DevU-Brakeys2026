using UnityEngine;

namespace AudioSystem
{
    public class AudioEffects : MonoBehaviour
    {
        public static AudioEffects Instance;

        [SerializeField] private MusicFade musicFade;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void ApplyMusicFade(bool fadingOut)
        {
            if (fadingOut)
                musicFade.ApplyFadeOut();
            else
                musicFade.ApplyFadeIn();
        }
    }
}