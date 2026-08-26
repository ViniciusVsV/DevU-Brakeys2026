using System.Collections;
using Person;
using UnityEngine;

namespace Minigames
{
    public class DogSniffing : MonoBehaviour, IPlayable
    {
        //Minigame do cachorro cheirar o sujeito para detectar droga
        //Demora um tempo com uma pequena variação aleatória e tem a chace de errar e cheirar denovo por um pouco mais de tempo
        //Atualmente, quando o sprite é amarelo ele está parado, quando é azul ele está cheirando, quando é verde o sujeito n tem droga e quando está vermelho tem droga

        [SerializeField] private MinigamesData minigamesData;

        [SerializeField] private SpriteRenderer sr;

        private Coroutine coroutine;

        private void Awake()
        {
            sr.color = Color.yellow;
        }

        public void PlayMinigame(PersonBehaviour person)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Sniff(person));
        }

        public void StopMinigame()
        {
            //minigame acaba, mata a corrotina e volta ao idle
            StopCoroutine(coroutine);

            sr.color = Color.yellow;
        }

        private IEnumerator Sniff(PersonBehaviour person)
        {
            //Da play na animação de xeirar
            sr.color = Color.blue;

            yield return new WaitForSeconds(Random.Range(minigamesData.minSniffingDuration, minigamesData.maxSniffingDuration));

            if (Random.Range(0f, 1f) < minigamesData.repeatSniffingChance)
            {
                //Mostra um sprite aleatório de que detectou droga ou não (trollage kapakapa)
                if (Random.Range(0f, 1f) < 0.5f)
                    sr.color = Color.green;
                else
                    sr.color = Color.red;

                yield return new WaitForSeconds(Random.Range(minigamesData.minRepeatSniffingDelay, minigamesData.maxRepeatSniffingDelay));

                //Volta a animação de xeirar
                sr.color = Color.blue;

                yield return new WaitForSeconds(Random.Range(minigamesData.minRepeatSniffingDuration, minigamesData.maxRepeatSniffingDuration));
            }

            if (person.GetDrugs())
            {
                //Mostra um sprite diferente caso detectou droga
                sr.color = Color.red;
            }
            else
            {
                //Não tem droga
                sr.color = Color.green;
            }

            coroutine = null;
        }
    }
}