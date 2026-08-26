using System;
using System.Collections;
using PersonObjects;
using UnityEngine;

namespace Person
{
    public class PersonSpawner : MonoBehaviour
    {
        [SerializeField] private PersonData personData;
        [SerializeField] private PersonBehaviour personPrefab;
        [SerializeField] private PassportGenerator passportGenerator;
        [SerializeField] private SuitcaseGenerator suitcaseGenerator;

        private float elapsedTime;

        public static event Action<PersonBehaviour> OnPersonSpawn;

        private void Awake()
        {
            passportGenerator = Instantiate(passportGenerator, transform);
            suitcaseGenerator = Instantiate(suitcaseGenerator, transform);
        }

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(personData.initialDelay);

            elapsedTime = 0;

            while (true)
            {
                PersonBehaviour newPerson = Instantiate(personPrefab, transform.position, Quaternion.identity);

                bool invalidPassport = passportGenerator.GeneratePassport(newPerson);
                newPerson.isInvalid = invalidPassport;

                bool invalidSuitcase = suitcaseGenerator.GenerateSuitcase(newPerson);
                newPerson.isInvalid = invalidSuitcase;

                OnPersonSpawn?.Invoke(newPerson);

                float progress = Mathf.Clamp01(elapsedTime / personData.timeToReachMaxDificulty);

                float difficulty = personData.dificultyCurve.Evaluate(progress);
                float spawnCooldown = Mathf.Lerp(personData.initialSpawnCooldown, personData.finalSpawnCooldown, difficulty);

                yield return new WaitForSeconds(spawnCooldown);

                elapsedTime += spawnCooldown;
            }
        }
    }
}