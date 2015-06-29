using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LetterDisplay : MonoBehaviour
    {
        [SerializeField]
        private Text letter;

        [SerializeField]
        private Text questionMark;


        
        public bool IsHidden
        {
            get
            {
                return questionMark.gameObject.activeSelf;
            }
            set
            {
                questionMark.gameObject.SetActive(value);
                letter.gameObject.SetActive(!value);
            }
        }

        public char Letter
        {
            get
            {
                return letter.text.Length == 0 ? '\0' : letter.text[0];
            }
            set
            {
                letter.text = value.ToString();
            }
        }
    }
}
