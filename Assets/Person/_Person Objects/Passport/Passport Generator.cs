using System.Collections.Generic;
using Person;
using UnityEngine;

namespace PersonObjects
{
    public class PassportGenerator : MonoBehaviour
    {
        [SerializeField] private PersonObjectsData personObjectsData;
        [SerializeField] private PassportBehaviour basePassport;

        private bool generatedInvalid;

        public bool GeneratePassport(PersonBehaviour owner)
        {
            generatedInvalid = false;

            //Gera dois passaportes, um para aparecer na televisão e um para ser analisado
            //O que aparece na televisão sempre é o correto
            //O que é mostrado tem chance de ser inválido
            PassportBehaviour referencePassport = Instantiate(basePassport, owner.transform);
            PassportBehaviour carriedPassport = Instantiate(basePassport, owner.transform);

            int nameIndex = personObjectsData.GetRandomNameIndex();
            int countryIndex = personObjectsData.GetRandomCountryIndex();
            int genderIndex = personObjectsData.GetRandomGenderIndex();

            //Primeiro constrói o passaporte válido
            referencePassport.nameText.text = personObjectsData.possibleNames[nameIndex];
            referencePassport.countryText.text = personObjectsData.possibleCountries[countryIndex];
            referencePassport.genderText.text = personObjectsData.possibleGenders[genderIndex];

            //Segundo, faz uma rolagem para verificar se o segundo passaporte será inválido
            float invalidRoll = Random.Range(0f, 1f);

            //Segundo passaporte é inválido
            if (invalidRoll < personObjectsData.invalidPassportProbability)
            {
                carriedPassport.CopyValues(referencePassport);

                List<int> availableFields = new List<int> { 0, 1, 2 }; //0=nome, 1=país, 2=gênero

                int errorCount = Random.Range(1, 4);

                for (int i = 0; i < availableFields.Count; i++)
                {
                    int randomIndex = Random.Range(i, availableFields.Count);
                    (availableFields[i], availableFields[randomIndex]) = (availableFields[randomIndex], availableFields[i]);
                }

                for (int i = 0; i < errorCount; i++)
                {
                    switch (availableFields[i])
                    {
                        case 0:
                            carriedPassport.nameText.text = personObjectsData.possibleInvalidNames[nameIndex];
                            break;
                        case 1:
                            carriedPassport.countryText.text = personObjectsData.possibleInvalidCountries[countryIndex];
                            break;
                        case 2:
                            carriedPassport.genderText.text = personObjectsData.possibleInvalidGenders[genderIndex];
                            break;
                    }
                }

                generatedInvalid = true;
            }
            //Segundo passaporte é válido
            else
                carriedPassport.CopyValues(referencePassport);

            referencePassport.gameObject.SetActive(false);
            carriedPassport.gameObject.SetActive(false);

            owner.SetReferencePassport(referencePassport.gameObject);
            owner.SetCarriedPassport(carriedPassport.gameObject);

            return generatedInvalid;
        }
    }
}