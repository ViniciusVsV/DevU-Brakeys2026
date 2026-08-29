using System;
using System.Collections;
using System.Collections.Generic;
using Person;
using Unity.Cinemachine;
using UnityEngine;

namespace Sections
{
    public class SectionBehaviour : MonoBehaviour
    {
        //Script responsável pelo comportamento de uma área do jogo
        //Irá lidar com as pessoas dentro dela e retornar onde uma nova pessoa deverá entrar
        //Toda área tem uma referência para a área anterior e seguinte
        //O sistema funciona como uma lista encadeada, na qual cada área, um nó, irá movimentar as pessoas dentro de si e para os nós seguintes
        [SerializeField] private SectionData sectionData;

        [Header("Section Elements")]
        [SerializeField] private AudioController audioController;
        [SerializeField] private SectionBehaviour nextSection;
        [SerializeField] private Transform waitPoint;
        [SerializeField] private Transform activePoint;
        [SerializeField] private Transform dialoguePoint;
        public CinemachineCamera sectionCamera;
        public string sectionRules;

        [Header("People")]
        private PersonBehaviour activePerson;
        [HideInInspector] public List<PersonBehaviour> peopleInLine = new();

        [Header("Minigame")]
        [SerializeField] private GameObject minigameObject;
        private IPlayable minigamePlayable;

        [Header("Section Type")]
        public bool isSpawnSection;
        public bool isEndSection;
        public bool isCounterSection;

        private bool isBusy;

        public static event Action<PersonBehaviour, bool> OnPersonProcessed;
        public static event Action<CinemachineCamera> OnGameDefeat;

        private void Awake()
        {
            if (minigameObject != null)
                minigamePlayable = minigameObject.GetComponent<IPlayable>();
            if (!isSpawnSection && !isEndSection && minigamePlayable == null)
                Debug.LogWarning("Seção sem minigame!");
        }

        public void ReceivePerson(PersonBehaviour newPerson)
        {
            //Recebe uma nova pessoa na seção
            if (isEndSection)
            {
                StartCoroutine(MovePersonToActivePoint(newPerson));
                return;
            }

            if (activePerson != null)
            {
                MovePersonToLine(newPerson);
                peopleInLine.Add(newPerson);
            }
            else
            {
                activePerson = newPerson;
                StartCoroutine(MovePersonToActivePoint());
            }
        }

        public void ApprovePerson()
        {
            if (isBusy || activePerson == null)
                return;

            //Se a próxima seção está cheia
            if (nextSection.peopleInLine.Count == sectionData.maxWaitLength)
            {
                //Se a seção atula é uma seção de spawn --> Perde o jogo (primeira seção está lotada e a fila estorou)
                if (isSpawnSection)
                    OnGameDefeat?.Invoke(nextSection.sectionCamera);

                return;
            }

            //Para o minigame atual
            minigamePlayable?.StopMinigame();

            if (!isSpawnSection)
                audioController.PlayApprovePersonSFX();

            nextSection.ReceivePerson(activePerson);

            StartCoroutine(ReorderPeople());
        }

        public void DisapprovePerson()
        {
            if (isBusy || activePerson == null)
                return;

            //Para o minigame atual
            minigamePlayable?.StopMinigame();

            PersonBehaviour personCopy = activePerson;
            OnPersonProcessed?.Invoke(personCopy, false);

            audioController.PlayDenyPersonSFX();

            activePerson.Die();
            activePerson.Speak(dialoguePoint, true);    //Chama a possível fala de morte da pessoa

            StartCoroutine(ReorderPeople());
        }

        private IEnumerator MovePersonToActivePoint(PersonBehaviour usedPerson = null)
        {
            if (usedPerson == null)
                usedPerson = activePerson;

            isBusy = true;

            bool finished = false;

            usedPerson.Move(activePoint.position, () => { finished = true; });

            yield return new WaitUntil(() => finished);

            isBusy = false;

            if (isSpawnSection)
            {
                ApprovePerson();
                yield break;
            }
            if (isEndSection)
            {
                PersonBehaviour personCopy = usedPerson;
                OnPersonProcessed?.Invoke(personCopy, true);

                usedPerson.Die();

                yield break;
            }
            if (isCounterSection)
                audioController.PlayNewPersonSFX();

            //Chama o minigame vinculado à seção
            minigamePlayable?.PlayMinigame(usedPerson);

            usedPerson.Speak(dialoguePoint, false);
        }

        private void MovePersonToLine(PersonBehaviour person)
        {
            Vector2 positionInLine = (Vector2)waitPoint.position + new Vector2(peopleInLine.Count * 2, peopleInLine.Count * 0.1f);

            person.Move(positionInLine, () => { });
        }

        private IEnumerator ReorderPeople()
        {
            if (peopleInLine.Count == 0)
            {
                activePerson = null;
                yield break;
            }

            //Caso haja pessoas na fila:
            PersonBehaviour nextInLine = peopleInLine[0];
            peopleInLine.RemoveAt(0);

            activePerson = nextInLine;

            StartCoroutine(MovePersonToActivePoint());

            for (int i = 0; i < peopleInLine.Count; i++)
            {
                Vector2 positionInLine = (Vector2)waitPoint.position + new Vector2(i * 2, i * 0.1f);
                peopleInLine[i].Move(positionInLine, () => { });

                yield return new WaitForSeconds(sectionData.lineReorderDelay);
            }
        }

        public int GetPeopleCount()
        {
            int count = activePerson != null ? 1 : 0;

            return count + peopleInLine.Count;
        }
    }
}