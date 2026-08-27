using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PersonObjects
{
    public class PassportBehaviour : MonoBehaviour
    {
        [SerializeField] private Image photo;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI countryText;
        public TextMeshProUGUI genderText;
        
        public void SetPhoto(Sprite personPhoto)
        {
            photo.sprite = personPhoto;
        }

        public void CopyValues(PassportBehaviour passportBehaviour)
        {
            nameText.text = passportBehaviour.nameText.text;
            countryText.text = passportBehaviour.countryText.text;
            genderText.text = passportBehaviour.genderText.text;
        }
    }
}