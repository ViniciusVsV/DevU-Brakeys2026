using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PersonObjects
{
    public class PassportBehaviour : MonoBehaviour
    {
        [SerializeField] private Image photo;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI countryText;
        [SerializeField] private TextMeshProUGUI genderText;

        public void SetPhoto(Sprite personPhoto) { photo.sprite = personPhoto; }
        public void SetName(string name) { nameText.text = name; }
        public void SetCountry(string country) { countryText.text = country; }
        public void SetGender(string gender) { genderText.text = gender; }

        public void CopyValues(PassportBehaviour passportBehaviour)
        {
            nameText.text = passportBehaviour.nameText.text;
            countryText.text = passportBehaviour.countryText.text;
            genderText.text = passportBehaviour.genderText.text;
        }
    }
}