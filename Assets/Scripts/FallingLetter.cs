using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FallingLetter : MonoBehaviour
    {
        public float fallSpeed = 1.0f;
        
        
        [SerializeField]
        private Text _letterText;


        #region MonoBehaviour
        private void OnCollisionEnter2D(Collision2D coll)
        {
            if(coll.gameObject.tag == "Player")
            {
                GameController.Instance.MakeGuess(Letter);
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * fallSpeed;
        }
        #endregion

        public char Letter
        {
            get
            {
                return _letterText.text.Length == 0 ? '\0' : _letterText.text[0];
            }
            set
            {
                _letterText.text = value.ToString();
            }
        }
    }
}
