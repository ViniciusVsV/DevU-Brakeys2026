using System.Collections;
using Person;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames
{
    public class DogSniffing : MonoBehaviour, IPlayable
    {
        //Minigame do cachorro cheirar o sujeito para detectar droga
        //Demora um tempo com uma pequena variação aleatória e tem a chace de errar e cheirar denovo por um pouco mais de tempo
        //Atualmente, quando o sprite é amarelo ele está parado, quando é azul ele está cheirando, quando é verde o sujeito n tem droga e quando está vermelho tem droga

        [SerializeField] private MinigamesData minigamesData;
        [SerializeField] private AudioController audioController;

        [SerializeField] private Animator animator;

        private Coroutine coroutine;

        public void PlayMinigame(PersonBehaviour person)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Sniff(person));
        }

        public void StopMinigame()
        {
            //minigame acaba, mata a corrotina e volta ao idle
            if (coroutine != null)
                StopCoroutine(coroutine);

            audioController.StopSniffingSFX();
            animator.Play("Idle");
        }

        private IEnumerator Sniff(PersonBehaviour person)
        {
            //Da play na animação de xeirar
            audioController.StartSniffingSFX();

            animator.Play("Sniff");

            yield return new WaitForSeconds(Random.Range(minigamesData.minSniffingDuration, minigamesData.maxSniffingDuration));

            if (Random.Range(0f, 1f) < minigamesData.repeatSniffingChance)
            {
                //Mostra um sprite aleatório de que detectou droga ou não (trollage kapakapa)
                if (Random.Range(0f, 1f) < 0.5f)
                    animator.Play("Happy");
                else
                    animator.Play("Angry");

                yield return new WaitForSeconds(Random.Range(minigamesData.minRepeatSniffingDelay, minigamesData.maxRepeatSniffingDelay));

                //Volta a animação de xeirar
                animator.Play("Sniff");

                yield return new WaitForSeconds(Random.Range(minigamesData.minRepeatSniffingDuration, minigamesData.maxRepeatSniffingDuration));
            }

            audioController.StopSniffingSFX();

            audioController.PlayFinishSniffingSFX();

            if (person.GetDrugs())
                animator.Play("Angry");
            else
                animator.Play("Happy");

            coroutine = null;
        }
    }
}