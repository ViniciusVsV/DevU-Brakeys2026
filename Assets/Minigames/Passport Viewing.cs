using DG.Tweening;
using Person;
using UnityEngine;

namespace Minigames
{
    public class PassportViewing : MonoBehaviour, IPlayable
    {
        [SerializeField] private MinigamesData minigamesData;

        [SerializeField] private Transform carriedPassportPoint;
        [SerializeField] private Transform referencePassportPoint;

        private GameObject referencePassport;
        private GameObject carriedPassport;

        public void PlayMinigame(PersonBehaviour person)
        {
            ShowPassports(person);
        }
        public void StopMinigame()
        {
            HidePassports();
        }

        private void ShowPassports(PersonBehaviour person)
        {
            referencePassport = person.GetReferencePassport();
            carriedPassport = person.GetCarriedPassport();

            float randomRotation = Random.Range(-minigamesData.maxPassportRotation, minigamesData.maxPassportRotation);
            float yPosition = carriedPassportPoint.position.y;

            Vector3 initialScale = carriedPassport.transform.localScale;

            carriedPassport.transform.SetPositionAndRotation(carriedPassportPoint.position, Quaternion.Euler(0f, 0f, randomRotation));
            carriedPassport.transform.parent = null;
            carriedPassport.transform.localScale = initialScale * 0.8f;
            carriedPassport.SetActive(true);

            referencePassport.transform.position = referencePassportPoint.position;
            referencePassport.transform.parent = null;
            referencePassport.SetActive(true);

            Sequence sequence = DOTween.Sequence();

            sequence.Append(carriedPassport.transform.DOScale(initialScale * Random.Range(1f, minigamesData.maxPassportSizeMultiplier),
                                                                minigamesData.passportShowDuration).SetEase(minigamesData.passportShowEase));

            sequence.Join(carriedPassport.transform.DOMoveY(Random.Range(yPosition - minigamesData.minPassportMovement, yPosition - minigamesData.maxPassportMovement),
                                                                minigamesData.passportShowDuration).SetEase(minigamesData.passportShowEase));
        }

        private void HidePassports()
        {
            carriedPassport.transform.DOKill();

            carriedPassport.transform.DOScale(Vector2.zero, minigamesData.passportHideDuration)
                .OnComplete(() =>
                {
                    Destroy(carriedPassport);
                    Destroy(referencePassport);
                });
        }
    }
}