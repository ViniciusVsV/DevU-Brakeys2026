using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LogoSystem
{
    public class LogoManager : MonoBehaviour
    {
        [SerializeField] private List<LogoBase> logos;
        private int index = 0;

        [SerializeField] private string nextScene;

        void Awake()
        {
            // garante que todas come�am desligadas
            foreach (var logo in logos)
                logo.gameObject.SetActive(false);

            StartCoroutine(RunSequence());
        }

        IEnumerator RunSequence()
        {
            while (index < logos.Count)
            {
                LogoBase logo = logos[index];

                logo.gameObject.SetActive(true);

                bool finished = false;

                logo.Play(() =>
                {
                    finished = true;
                });

                // espera a logo terminar
                yield return new WaitUntil(() => finished);

                logo.gameObject.SetActive(false);

                index++;
            }

            Debug.Log("Todas as logos terminaram");

            LoadNextScene();
        }

        void LoadNextScene()
        {
            if (string.IsNullOrWhiteSpace(nextScene))
            {
                Debug.LogError("[LogoManager] nextScene está vazio ou nulo!");
                return;
            }

            if (!Application.CanStreamedLevelBeLoaded(nextScene))
            {
                Debug.LogError($"[LogoManager] Cena '{nextScene}' não está no Build Settings!");
                return;
            }

            Debug.Log($"[LogoManager] Carregando cena: {nextScene}");
            SceneManager.LoadScene(nextScene);
        }
    }
}