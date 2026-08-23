using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TransitionSystem
{
    public class TransitionManager : MonoBehaviour
    {
        //Código responsável por aplicar transições de cenas
        //Detecta e roda automaticamente transiçõa de entrada de cena
        //Deve ser chamado externamente para executar transiçõies de saída de cena
        public static TransitionManager Instance;

        [Header("Transition Elements")]
        [SerializeField] private SceneEnter sceneEnter;
        [SerializeField] private SceneReEnter sceneReEnter;
        [SerializeField] private SceneFail sceneFail;
        [SerializeField] private SceneExit sceneExit;

        public static event Action OnSceneEnter;
        public static event Action OnSceneReEnter;
        public static event Action OnSceneExit;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += EnterScene;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= EnterScene;
        }

        private void EnterScene(Scene arg0, LoadSceneMode arg1)
        {
            StartCoroutine(EnterRoutine());
        }
        private IEnumerator EnterRoutine()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string lastSceneName = PlayerPrefs.GetString("LastSceneName", null);

            bool finished = false;

            if (currentSceneName != lastSceneName)
            {
                sceneEnter.EnterScene(() => { finished = true; });

                yield return new WaitUntil(() => finished);

                OnSceneEnter?.Invoke();

                PlayerPrefs.SetString("LastSceneName", currentSceneName);
                PlayerPrefs.Save();
            }
            else
            {
                sceneReEnter.ReEnterScene(() => { finished = true; });

                yield return new WaitUntil(() => finished);

                OnSceneReEnter?.Invoke();
            }
        }

        public void ExitScene(string nextSceneName)
        {
            OnSceneExit?.Invoke();

            sceneExit.ExitScene(nextSceneName);
        }

        public void FailScene()
        {
            sceneFail.FailScene();
        }
    }
}