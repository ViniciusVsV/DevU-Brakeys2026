using System;
using System.Collections;
using UnityEngine;

namespace Person
{
    public class PersonSpawner : MonoBehaviour
    {
        [SerializeField] private PersonData personData;
        [SerializeField] private GameObject personPrefab;

        private float elapsedTime;

        public static event Action<Transform> OnPersonSpawn;

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
                GameObject newPerson = Instantiate(personPrefab, transform.position, Quaternion.identity);

                OnPersonSpawn?.Invoke(newPerson.transform);

                float progress = Mathf.Clamp01(elapsedTime / personData.timeToReachMaxDificulty);

                float difficulty = personData.dificultyCurve.Evaluate(progress);
                float spawnCooldown = Mathf.Lerp(personData.initialSpawnCooldown, personData.finalSpawnCooldown, difficulty);

                yield return new WaitForSeconds(spawnCooldown);

                elapsedTime += spawnCooldown;
            }
        }
    }
}