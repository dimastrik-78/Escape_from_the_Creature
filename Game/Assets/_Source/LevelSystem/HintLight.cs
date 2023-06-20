using System.Collections;
using UnityEngine;

namespace LevelSystem
{
    public class HintLight : MonoBehaviour
    {
        [SerializeField] private GameObject hintLight;
        [SerializeField] private int howMuchBlink;
        [SerializeField] private AudioSource sourceOffOn;
        
        private const float TIME_BETWEEN_BLINKS = 0.4f;
        private const float TIME_WAIT_BLINKS = 2f;

        private void Awake()
        {
            StartCoroutine(Blind());
        }

        private IEnumerator Blind()
        {
            while (true)
            {
                int countBlink = 0;
                while (countBlink < howMuchBlink)
                {
                    hintLight.SetActive(false);
                    sourceOffOn.Play();
                    
                    yield return new WaitForSeconds(TIME_BETWEEN_BLINKS);
                    
                    hintLight.SetActive(true);
                    sourceOffOn.Play();
                    
                    yield return new WaitForSeconds(TIME_BETWEEN_BLINKS);
                    
                    countBlink++;
                }
                yield return new WaitForSeconds(TIME_WAIT_BLINKS);
            }
        }
    }
}