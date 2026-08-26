using TMPro;
using UnityEngine;

namespace PersonObjects
{
    public class PassportBehaviour : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI countryText;
        public TextMeshProUGUI genderText;

        public void CopyValues(PassportBehaviour passportBehaviour)
        {
            nameText.text = passportBehaviour.nameText.text;
            countryText.text = passportBehaviour.countryText.text;
            genderText.text = passportBehaviour.genderText.text;
        }
    }
}