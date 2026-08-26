using System.Collections;
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

        [SerializeField] private Transform suitcaseSpawnPoint;
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
            GameObject suitcasePrefab = person.GetSuitcase();

            currentSuitcase = Instantiate(suitcasePrefab, suitcaseSpawnPoint.position, Quaternion.identity);

            currentSuitcase.transform.DOMoveX(xRayPoint.position.x, Random.Range(minigamesData.minSuitcaseMoveDuration, minigamesData.maxSuitcaseMoveDuration))
                .SetEase(Ease.Linear);
        }

        private void DespawnSuitcase()
        {
            currentSuitcase.transform.DOKill();

            //Move a mala até a beira esquerda da esteira e faz ela desaparecer
            Sequence sequence = DOTween.Sequence();

            sequence.Append(currentSuitcase.transform.DOMoveX(endPoint.position.x, minigamesData.suitcaseEndDuration)
                .SetEase(minigamesData.suitcaseEndEase));

            sequence.Append(currentSuitcase.transform.DOScale(Vector2.zero, minigamesData.suitcaseDisappearDuration)
                .SetEase(minigamesData.suitcaseDisappearEase));

            sequence.OnComplete(() => Destroy(currentSuitcase));
        }
    }
}