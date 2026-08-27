using Person;
using UnityEngine;

namespace PersonObjects
{
    public class SuitcaseGenerator : MonoBehaviour
    {
        [SerializeField] private PersonData personData;
        [SerializeField] private SuitcaseBehaviour baseSuitcase;

        private bool generatedInvalid;

        public bool GenerateSuitcase(PersonBehaviour owner)
        {
            generatedInvalid = false;

            SuitcaseBehaviour suitcase = Instantiate(baseSuitcase, owner.transform);
            Bounds suitcaseBounds = suitcase.GetBounds(); ;

            //Primeiro, randomiza o numero de items
            int numberItems = Random.Range(personData.minNumberItems, personData.maxNumberItems);

            //Depois, randomiza cada item (se é válido ou não)
            for (int i = 0; i < numberItems; i++)
            {
                GameObject itemPrefab;
                float invalidRoll = Random.Range(0f, 1f);

                if (invalidRoll < personData.invalidItemProbability)
                {
                    itemPrefab = personData.GetRandomInvalidItem();
                    generatedInvalid = true;
                }
                else
                    itemPrefab = personData.GetRandomItem();

                float randomRotation = Random.Range(-180, 180);

                GameObject newItem = Instantiate
                    (
                        itemPrefab,
                        Vector3.zero,
                        Quaternion.Euler(0f, 0f, randomRotation),
                        suitcase.transform
                    );

                Bounds itemBounds = newItem.GetComponent<Renderer>().bounds;

                float halfItemWidth = itemBounds.extents.x;
                float halfItemHeight = itemBounds.extents.y;

                float minX = suitcaseBounds.min.x + halfItemWidth;
                float minY = suitcaseBounds.min.y + halfItemHeight;
                float maxX = suitcaseBounds.max.x - halfItemWidth;
                float maxY = suitcaseBounds.max.y - halfItemHeight;

                newItem.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            }

            suitcase.gameObject.SetActive(false);

            owner.SetSuitcase(suitcase.gameObject);

            return generatedInvalid;
        }
    }
}