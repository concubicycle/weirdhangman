using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : NonPersistentSingleton<GameController>
    {        
        [SerializeField]        
        private WordDisplay _wordDisplay;

        [SerializeField]
        private Hangman _hangMan;


        public AudioClip wrong;
        public AudioClip right;

        void Start()
        {
            Time.timeScale = 1;
        }

        public void MakeGuess(char letter)
        {
            bool correct = _wordDisplay.MakeGuess(letter);

            if(!correct)
            {
                _hangMan.WrongCount++;

                AudioSource.PlayClipAtPoint(wrong, Camera.main.transform.position);

                if(_hangMan.IsDead)
                {
                    Time.timeScale = 0;

                    ModalDialog.Instance.Show("Game over. You lost.", null, null,
                        () =>
                        {
                            Application.LoadLevel(0);
                        },
                        null);
                }
            }
            else
            {
                AudioSource.PlayClipAtPoint(right, Camera.main.transform.position);
            }

            if(!_wordDisplay.AreAnyHidden)
            {
                Time.timeScale = 0;

                ModalDialog.Instance.Show("You win! We are all impressed by how much you've won.", null, null,
                        () =>
                        {
                            Application.LoadLevel(0);
                        },
                        null);
            }
        }
    }
}
