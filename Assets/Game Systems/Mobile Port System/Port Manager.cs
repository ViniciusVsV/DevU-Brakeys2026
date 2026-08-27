using UnityEngine;

namespace MobilePortSystem
{
    public class PortManager : MonoBehaviour
    {
        public static PortManager Instance;

        [SerializeField] private GameObject canvasObject;

        [Header("Pause Toggle")]
        [SerializeField] private GameObject[] disableableObjects;

        public bool finishedStart;

        public bool isEnabled;

        private void Awake()
        {
            isEnabled = false;
            finishedStart = false;

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            RuntimePlatform platform = Application.platform;

            if (
                platform == RuntimePlatform.Android ||
                platform == RuntimePlatform.IPhonePlayer ||
                (platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
            )
            {
                PlayerPrefs.SetInt("IsOnMobile", 1);
                PlayerPrefs.Save();
            }
            else
                PlayerPrefs.DeleteKey("IsOnMobile");

            finishedStart = true;
        }

        //Para ativar o sistema
        public void EnableControls()
        {
            if (PlayerPrefs.GetInt("IsOnMobile", 0) == 0)
                return;

            isEnabled = true;

            canvasObject.SetActive(true);
        }

        //Para desativar o sistema, como em cutscenes ou alguns menus
        public void DisableControls()
        {
            canvasObject.SetActive(false);
        }

        //Para desativar todos os botões menos o de pause, quando o jogo for pausado
        public void PauseControls(bool isPausing)
        {
            if (isPausing)
                foreach (GameObject obj in disableableObjects)
                    obj.SetActive(false);
            else
                foreach (GameObject obj in disableableObjects)
                    obj.SetActive(true);
        }
    }
}