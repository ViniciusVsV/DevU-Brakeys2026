using System.Collections.Generic;
using Person;
using UnityEngine;

namespace PersonObjects
{
    public class PassportGenerator : MonoBehaviour
    {
        [SerializeField] private PersonData personData;
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

            referencePassport.SetPhoto(owner.GetSprite());
            carriedPassport.SetPhoto(owner.GetSprite());

            int nameIndex = personData.GetRandomNameIndex();
            int countryIndex = personData.GetRandomCountryIndex();
            int genderIndex = personData.GetRandomGenderIndex();

            //Primeiro constrói o passaporte válido
            referencePassport.SetName(personData.possibleNames[nameIndex]);
            referencePassport.SetCountry(personData.possibleCountries[countryIndex]);
            referencePassport.SetGender(personData.possibleGenders[genderIndex]);

            //Segundo, faz uma rolagem para verificar se o segundo passaporte será inválido
            float invalidRoll = Random.Range(0f, 1f);

            //Segundo passaporte é inválido
            if (invalidRoll < personData.invalidPassportProbability)
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
                            carriedPassport.SetName(personData.possibleInvalidNames[nameIndex]);
                            break;
                        case 1:
                            carriedPassport.SetCountry(personData.possibleInvalidCountries[countryIndex]);
                            break;
                        case 2:
                            carriedPassport.SetGender(personData.possibleInvalidGenders[genderIndex]);
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