using Sections;
using Person;
using UnityEngine;

public class PersonSectionGlue : MonoBehaviour
{
    [SerializeField] private SectionBehaviour spawnSection;

    private void OnEnable()
    {
        Person.PersonSpawner.OnPersonSpawn += SpawnPerson;
    }
    private void OnDisable()
    {
        Person.PersonSpawner.OnPersonSpawn -= SpawnPerson;
    }

    private void SpawnPerson(PersonBehaviour newPerson)
    {
        spawnSection.ReceivePerson(newPerson);
    }
}