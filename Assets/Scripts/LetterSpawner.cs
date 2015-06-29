using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class LetterSpawner : MonoBehaviour
    {
        public float spawnPeriod = 0.75f;
        public float minSpawnPeriod = 0.25f;
        public float freqInc = 0.04f;
        public float periodIncTime = 4;


        public float speedMult = 1;
        public float maxSpeedMult = 2.5f;
        public float speedMultIncTime = 4;
        public float speedMultInc = 0.05f;


        public float neededLettersSpawnFrequence = 2.5f;

        //  spawns falling letters from -letterSpawnrange to letterSpawnrange
        public float letterSpawnrange = 6;
        public float spawnHeight = 12.0f;
        public WordDisplay wordDisplay;

        
        #region MonoBehaviour
        void Start()
        {
            StartCoroutine(SpawnFallingLetters());
            StartCoroutine(SpawnNeededLetters());
            StartCoroutine(GraduallyShortenPeriod());
            StartCoroutine(GraduallyUpSpeed());
        }
        #endregion





        #region Coroutines
        IEnumerator SpawnFallingLetters()
        {
            WaitForSeconds wait = new WaitForSeconds(spawnPeriod);

            while (true)
            {
                yield return wait;
                SpawnLetter();
            }
        }


        IEnumerator SpawnNeededLetters()
        {
            WaitForSeconds wait = new WaitForSeconds(neededLettersSpawnFrequence);

            while (true)
            {
                yield return wait;
                SpawnNeededLetter();
            }
        }

        IEnumerator GraduallyShortenPeriod()
        {
            yield return new WaitForSeconds(5.0f);


            WaitForSeconds wait = new WaitForSeconds(periodIncTime);

            while (spawnPeriod > minSpawnPeriod)
            {
                spawnPeriod -= freqInc;
                yield return wait;
            }
        }

        IEnumerator GraduallyUpSpeed()
        {
            yield return new WaitForSeconds(5.0f);


            WaitForSeconds wait = new WaitForSeconds(speedMultIncTime);

            while (speedMult < maxSpeedMult)
            {
                speedMult += speedMultInc;
                yield return wait;
            }
        }
        #endregion




        public void SpawnLetter()
        {
            GameObject letter = SpawnLetterGO();
            letter.GetComponent<FallingLetter>().Letter = GetLetter();
            letter.GetComponent<FallingLetter>().fallSpeed *= speedMult;
        }


        public void SpawnNeededLetter()
        {
            GameObject letter = SpawnLetterGO();

            var neededLetters = wordDisplay.NeededLetters;
            char ch = neededLetters[Random.Range(0, neededLetters.Count - 1)];
            letter.GetComponent<FallingLetter>().Letter = ch;
        }


        private GameObject SpawnLetterGO()
        {
            GameObject letter = PrefabFactory.FallingLetter;
            float x = Random.Range(-letterSpawnrange, letterSpawnrange);
            letter.transform.position = new Vector3(x, spawnHeight, 0);
            return letter;
        }

        //http://www.dotnetperls.com/random-lowercase-letter
        private static char GetLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusive.
            int num = Random.Range(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }
    }
}
