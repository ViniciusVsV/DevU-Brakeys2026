using System;
using System.Collections.Generic;
using DG.Tweening;
using Person;
using Unity.Cinemachine;
using UnityEngine;

namespace GameSections
{
    public class SectionBehaviour : MonoBehaviour
    {
        //Script responsável pelo comportamento de uma área do jogo
        //Irá lidar com as pessoas dentro dela e retornar onde uma nova pessoa deverá entrar
        //Toda área tem uma referência para a área anterior e seguinte
        //O sistema funciona como uma lista encadeada, na qual cada área, um nó, irá movimentar as pessoas dentro de si e para os nós seguintes
        [SerializeField] private SectionData sectionData;

        [Header("Section Elements")]
        [SerializeField] private SectionBehaviour nextSection;
        [SerializeField] private Transform waitPoint;
        [SerializeField] private Transform activePoint;
        public CinemachineCamera sectionCamera;
        public string sectionRules;

        [Header("People")]
        private Transform activePerson;
        private PersonBehaviour activePersonBehaviour;
        [HideInInspector] public List<Transform> peopleInLine = new();

        [Header("Minigame")]
        [SerializeField] private GameObject minigameObject;
        private IPlayable minigamePlayable;

        [Header("Section Type")]
        public bool isSpawnSection;
        public bool isEndSection;

        private bool isBusy;

        public static event Action<PersonBehaviour> OnPersonProcessed;
        public static event Action<CinemachineCamera> OnGameDefeat;

        private void Awake()
        {
            if (minigameObject != null)
                minigamePlayable = minigameObject.GetComponent<IPlayable>();
            if (!isSpawnSection && !isEndSection && minigamePlayable == null)
                Debug.LogWarning("Seção sem minigame!");
        }

        public void ReceivePerson(Transform newPerson)
        {
            //Recebe uma nova pessoa na seção
            if (activePerson != null)
            {
                MovePersonToLine(newPerson);
                peopleInLine.Add(newPerson);
            }
            else
            {
                MovePersonToActivePoint(newPerson);
                activePerson = newPerson;
                activePersonBehaviour = activePerson.GetComponent<PersonBehaviour>();
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

            nextSection.ReceivePerson(activePerson);

            RemovePerson();
        }

        public void DisapprovePerson()
        {
            if (isBusy || activePerson == null)
                return;

            //Para o minigame atual
            minigamePlayable?.StopMinigame();

            PersonBehaviour personCopy = activePersonBehaviour;
            OnPersonProcessed?.Invoke(personCopy);
            
            activePersonBehaviour.Die();

            RemovePerson();
        }

        private void RemovePerson()
        {
            if (peopleInLine.Count == 0)
            {
                activePerson = null;
                activePersonBehaviour = null;
            }
            else
                ReorderPeopleInLine();
        }

        private void MovePersonToActivePoint(Transform person)
        {
            isBusy = true;

            person.DOMove(activePoint.position, sectionData.personMoveDuration)
                .SetEase(sectionData.personMoveEase)
                .OnComplete(() =>
                {
                    isBusy = false;

                    if (isSpawnSection)
                        ApprovePerson();

                    else if (isEndSection)
                    {
                        PersonBehaviour personCopy = activePersonBehaviour;
                        OnPersonProcessed?.Invoke(personCopy);
                        Destroy(person.gameObject);
                    }

                    //Chama o minigame vinculado à seção
                    minigamePlayable?.PlayMinigame(activePersonBehaviour);
                });
        }

        private void MovePersonToLine(Transform person)
        {
            Vector2 positionInLine = (Vector2)waitPoint.position + new Vector2(peopleInLine.Count, peopleInLine.Count);

            person.DOMove(positionInLine, sectionData.personMoveDuration)
                .SetEase(sectionData.personMoveEase);
        }

        private void ReorderPeopleInLine()
        {
            //Move a nova pessoa ativa para o ponto ativo e reordena a fila
            Transform nextInLine = peopleInLine[0];
            peopleInLine.RemoveAt(0);

            activePerson = nextInLine;
            activePersonBehaviour = activePerson.GetComponent<PersonBehaviour>();

            MovePersonToActivePoint(activePerson);

            for (int i = 0; i < peopleInLine.Count; i++)
            {
                Vector2 positionInLine = (Vector2)waitPoint.position + new Vector2(i, i);
                peopleInLine[i].DOMove(positionInLine, sectionData.personMoveDuration)
                    .SetEase(sectionData.personMoveEase);
            }
        }
    }
}