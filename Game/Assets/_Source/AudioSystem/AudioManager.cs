using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;

        private const string SOUND_NAME = "Sound";

        private void Awake()
        {
            mixer.SetFloat(SOUND_NAME, PlayerPrefs.HasKey(SOUND_NAME) ? PlayerPrefs.GetFloat(SOUND_NAME) : 1f);
        }

        public void ChangeSound(float value)
        {
            float resultValue = Mathf.Log10(value == 0 ? 0.001f : value) * 20;
            mixer.SetFloat(SOUND_NAME, resultValue);
            PlayerPrefs.SetFloat(SOUND_NAME, resultValue);
        }
    }
}
