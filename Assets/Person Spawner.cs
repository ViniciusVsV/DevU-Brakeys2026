using System.Collections;
using SectionSystem;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private SectionBehaviour spawnSection;
    [SerializeField] private GameObject personPrefab;

    public float spawnTime;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            GameObject newPerson = Instantiate(personPrefab, spawnSection.activePoint.position, Quaternion.identity);

            spawnSection.AddPerson(newPerson.transform);
        }
    }
}
