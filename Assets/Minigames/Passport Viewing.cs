using DG.Tweening;
using Person;
using UnityEngine;

namespace Minigames
{
    public class PassportViewing : MonoBehaviour, IPlayable
    {
        [SerializeField] private MinigamesData minigamesData;
        [SerializeField] private AudioController audioController;

        [SerializeField] private Transform carriedPassportStartPoint;
        [SerializeField] private Transform carriedPassportEndPoint;
        [SerializeField] private Transform referencePassportPoint;
        [SerializeField] private GameObject tvStaticImage;

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

            //Pisca a televisão com estática por um tempinho e então mostra o passaporte na televisão
            audioController.PlayTVStatic();
            tvStaticImage.SetActive(true);

            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(0.5f);

            sequence.AppendCallback(() =>
            {
                tvStaticImage.SetActive(false);
                referencePassport.transform.position = referencePassportPoint.position;
                referencePassport.transform.parent = null;
                referencePassport.SetActive(true);
            });

            carriedPassport.transform.DOMoveY(carriedPassportEndPoint.position.y, minigamesData.passportShowDuration).SetEase(minigamesData.passportShowEase);
        }

        private void HidePassports()
        {
            carriedPassport.transform.DOKill();

            audioController.PlayTVStatic();

            tvStaticImage.SetActive(true);
            referencePassport.SetActive(false);

            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(0.5f);

            sequence.AppendCallback(() =>
            {
                tvStaticImage.SetActive(false);
            });

            carriedPassport.transform.DOMoveY(carriedPassportStartPoint.position.y, minigamesData.passportHideDuration).SetEase(minigamesData.passportHideEase)
                .OnComplete(() =>
                {
                    Destroy(carriedPassport);
                    Destroy(referencePassport);
                });
        }
    }
}