using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.UI
{
    public class WordDisplay : MonoBehaviour
    {
        public GameObject letterDisplayPrefab;

        private List<LetterDisplay> _letterDisplays;
        private string _currentWord;
        
        public string CurrentWord
        {
            get
            {
                return _currentWord;
            }                       
        }

        public bool AreAnyHidden
        {
            get
            {
                return _letterDisplays.Any(d => d.IsHidden);
            }
        }

        public List<char> NeededLetters
        {
            get
            {
                return _letterDisplays.Where(l => l.IsHidden).Select(d => d.Letter).ToList();
            }
        }


        #region MonoBehaviour
        void Awake()
        {
            _letterDisplays = new List<LetterDisplay>();
        }

        void Start()
        {
            GenerateNewWord();
        }


        #endregion



        #region public methods
        public void GenerateNewWord()
        {
            string word = RandomWord;
            int len = word.Length;

            _currentWord = word;    


            //for now, just destroy old displays, and replace with new ones. can 
            //optimize via pool later
            foreach (var d in _letterDisplays) Destroy(d);
            for (int i = 0; i < len; i++)
            {
                GameObject displayGO = PrefabFactory.LetterDisplay;
                LetterDisplay display = displayGO.GetComponent<LetterDisplay>();

                display.transform.SetParent(transform);

                display.Letter = word[i];
                _letterDisplays.Add(display);
            }
        }
        
        /// <summary>
        /// Gets a guessed letter, and flips matching letter displays
        /// </summary>
        /// <param name="letter">Guessed letter</param>
        /// <returns>true if guess was correct. false if not.</returns>
        public bool MakeGuess(char letter)
        {
            bool foundLetter = false;

            foreach(var ch in CurrentWord)
            {
                if (ch == letter)
                {
                    foundLetter = true;

                    foreach(var d in _letterDisplays)
                        if(d.Letter == letter)
                        {
                            d.IsHidden = false;
                        }
                }                
            }

            return foundLetter;
        }        
        #endregion


        #region private methods
        private string RandomWord
        {
            get
            {
                int ind = Random.Range(0, Words.WordList.Count - 1);
                return Words.WordList[ind];
            }
        }
        #endregion
    }
}
