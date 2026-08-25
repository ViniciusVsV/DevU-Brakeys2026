using DG.Tweening;
using Person;
using UnityEngine;

namespace Minigames
{
    public class PassportViewing : MonoBehaviour, IPlayable
    {
        [SerializeField] private MinigamesData minigamesData;

        [SerializeField] private Transform passportSpawnPoint;
        private GameObject currentPassport;

        public void PlayMinigame(PersonBehaviour person)
        {
            ShowPassport(person);
        }
        public void StopMinigame()
        {
            HidePassport();
        }

        private void ShowPassport(PersonBehaviour person)
        {
            GameObject passportPrefab = person.GetPassport();

            float randomRotation = Random.Range(-minigamesData.maxPassportRotation, minigamesData.maxPassportRotation);
            float yPosition = passportSpawnPoint.position.y;

            currentPassport = Instantiate(passportPrefab, passportSpawnPoint.position, Quaternion.Euler(0f, 0f, randomRotation));

            Sequence sequence = DOTween.Sequence();

            sequence.Append(currentPassport.transform.DOScale(Vector2.one * Random.Range(1f, minigamesData.maxPassportSizeMultiplier),
                                                                minigamesData.passportShowDuration).SetEase(minigamesData.passportShowEase));

            sequence.Join(currentPassport.transform.DOMoveY(Random.Range(yPosition - minigamesData.minPassportMovement, yPosition - minigamesData.maxPassportMovement),
                                                                minigamesData.passportShowDuration).SetEase(minigamesData.passportShowEase));
        }

        private void HidePassport()
        {
            currentPassport.transform.DOScale(Vector2.zero, 0.3f)
                .OnComplete(() => { Destroy(currentPassport); });
        }
    }
}