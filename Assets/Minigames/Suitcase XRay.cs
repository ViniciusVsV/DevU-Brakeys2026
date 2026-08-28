using DG.Tweening;
using Person;
using UnityEngine;

namespace Minigames
{
    public class SuitcaseXRay : MonoBehaviour, IPlayable
    {
        //O minigame funcionará colocando a mala da pessoa em uma esteira
        //A mala demorará um tempo com uma pequena variação aleatória para chegar ao raioX
        //Aop chegar no raioX, haverá uma spriteMask que irá mostrar o conteúdo dentro da mala
        //Terminar o minigame (aprovar ou recusar a pessoa) irá iniciar uma animação com DOTween da mala diminuindo de tamanho até ser deletada
        [SerializeField] private MinigamesData minigamesData;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform xRayPoint;
        [SerializeField] private Transform endPoint;
        private GameObject currentSuitcase;

        private Sequence spawnSequence;
        private Sequence despawnSequence;

        public void PlayMinigame(PersonBehaviour person)
        {
            SpawnSuitcase(person);
        }
        public void StopMinigame()
        {
            if (spawnSequence != null && spawnSequence.IsActive())
            {
                spawnSequence.timeScale = 2f;

                spawnSequence.OnComplete(() => DespawnSuitcase(currentSuitcase));
            }
            else
                DespawnSuitcase(currentSuitcase);
        }

        private void SpawnSuitcase(PersonBehaviour person)
        {
            currentSuitcase = person.GetSuitcase();

            currentSuitcase.transform.position = spawnPoint.position;
            currentSuitcase.transform.parent = null;
            currentSuitcase.SetActive(true);

            spawnSequence?.Kill();
            spawnSequence = DOTween.Sequence();

            spawnSequence.Append(currentSuitcase.transform.DOMoveY(startPoint.position.y, minigamesData.suitacseSpawnDuration).SetEase(minigamesData.suitcaseSpawnEase));

            spawnSequence.Append(currentSuitcase.transform.DOMoveX(xRayPoint.position.x, Random.Range(minigamesData.minSuitcaseMoveDuration, minigamesData.maxSuitcaseMoveDuration))
                .SetEase(Ease.Linear));
        }

        private void DespawnSuitcase(GameObject suitcase)
        {
            despawnSequence = DOTween.Sequence();

            // Move a mala até o final da esteira
            despawnSequence.Append(
                suitcase.transform
                    .DOMoveX(endPoint.position.x, minigamesData.suitcaseEndDuration)
                    .SetEase(minigamesData.suitcaseEndEase)
            );

            // Faz a mala cair para baixo
            despawnSequence.Append(
                suitcase.transform
                    .DOMoveY(suitcase.transform.position.y - 30f, 1f)
                    .SetEase(Ease.InQuad)
            );

            // Rotaciona enquanto cai
            despawnSequence.Join(
                suitcase.transform
                    .DORotate(new Vector3(0, 0, 180f), 1f, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InQuad)
            );

            despawnSequence.OnComplete(() => Destroy(suitcase));
        }
    }
}