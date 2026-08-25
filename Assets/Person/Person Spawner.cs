using System;
using System.Collections;
using UnityEngine;

namespace Person
{
    public class PersonSpawner : MonoBehaviour
    {
        [SerializeField] private PersonData personData;
        [SerializeField] private GameObject personPrefab;

        public static event Action<Transform> OnPersonSpawn;

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(personData.spawnCooldown);

                GameObject newPerson = Instantiate(personPrefab, transform.position, Quaternion.identity);

                OnPersonSpawn?.Invoke(newPerson.transform);
            }
        }
    }
}