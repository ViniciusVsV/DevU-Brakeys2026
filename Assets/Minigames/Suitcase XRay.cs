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

        public void PlayMinigame(PersonBehaviour person)
        {
            SpawnSuitcase(person);
        }
        public void StopMinigame()
        {
            DespawnSuitcase();
        }

        private void SpawnSuitcase(PersonBehaviour person)
        {
            currentSuitcase = person.GetSuitcase();

            currentSuitcase.transform.position = spawnPoint.position;
            currentSuitcase.transform.parent = null;
            currentSuitcase.SetActive(true);

            Sequence sequence = DOTween.Sequence();

            sequence.Append(currentSuitcase.transform.DOMoveY(startPoint.position.y, minigamesData.suitacseSpawnDuration).SetEase(minigamesData.suitcaseSpawnEase));

            sequence.Append(currentSuitcase.transform.DOMoveX(xRayPoint.position.x, Random.Range(minigamesData.minSuitcaseMoveDuration, minigamesData.maxSuitcaseMoveDuration))
                .SetEase(Ease.Linear));
        }

        private void DespawnSuitcase()
        {
            currentSuitcase.transform.DOKill();
            GameObject lastSuitcase = currentSuitcase;

            Sequence sequence = DOTween.Sequence();

            // Move a mala até o final da esteira
            sequence.Append(
                lastSuitcase.transform
                    .DOMoveX(endPoint.position.x, minigamesData.suitcaseEndDuration)
                    .SetEase(minigamesData.suitcaseEndEase)
            );

            // Faz a mala cair para baixo
            sequence.Append(
                lastSuitcase.transform
                    .DOMoveY(lastSuitcase.transform.position.y - 30f, 1f)
                    .SetEase(Ease.InQuad)
            );

            // Rotaciona enquanto cai
            sequence.Join(
                lastSuitcase.transform
                    .DORotate(new Vector3(0, 0, 180f), 1f, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InQuad)
            );

            sequence.OnComplete(() => Destroy(lastSuitcase));
        }
    }
}