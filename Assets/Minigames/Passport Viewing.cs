using DG.Tweening;
using Person;
using UnityEngine;

namespace Minigames
{
    public class PassportViewing : MonoBehaviour, IPlayable
    {
        [SerializeField] private MinigamesData minigamesData;

        [SerializeField] private Transform carriedPassportStartPoint;
        [SerializeField] private Transform carriedPassportEndPoint;
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

            carriedPassport.transform.position = carriedPassportStartPoint.position;
            carriedPassport.transform.parent = null;
            carriedPassport.SetActive(true);

            referencePassport.transform.position = referencePassportPoint.position;
            referencePassport.transform.parent = null;
            referencePassport.SetActive(true);

            carriedPassport.transform.DOMoveY(carriedPassportEndPoint.position.y, minigamesData.passportShowDuration).SetEase(minigamesData.passportShowEase);
        }

        private void HidePassports()
        {
            carriedPassport.transform.DOKill();

            carriedPassport.transform.DOMoveY(carriedPassportStartPoint.position.y, minigamesData.passportHideDuration).SetEase(minigamesData.passportHideEase)
                .OnComplete(() =>
                {
                    Destroy(carriedPassport);
                    Destroy(referencePassport);
                });
        }
    }
}