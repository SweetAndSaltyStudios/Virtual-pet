using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Sweet_And_Salty_Studios
{
    public class AudioManager : Singelton<AudioManager>
    {
        #region VARIABLES

        [Header("References")]
        public AudioMixer AudioMixer;
        public AudioClip MissinAudioClip;
        public AudioClip[] AudioClips;
        public AudioClip[] MusicTracks;

        private AudioSource audioSource;

        #endregion

        #region PROPERTIES

        public AudioMixerUpdateMode AudioMixerUpdateMode
        {
            get;
            private set;
        }
        public AudioMixerGroup[] AudioMixerGroups
        {
            get;
            private set;
        }

        public bool IsFading
        {
            get;
            private set;
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            AudioMixerUpdateMode = AudioMixerUpdateMode.UnscaledTime;
            AudioMixerGroups = AudioMixer.FindMatchingGroups(string.Empty);
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private float DecibelToLinearValue(float decibelValue)
        {
            return Mathf.Pow(10.0f, decibelValue / 20.0f);
        }

        private float LinearToDecibelValue(float linearValue)
        {
            return linearValue != 0 ? 20.0f * Mathf.Log10(linearValue) : -80f;
        }

        public AudioMixerGroup GetChannelOutput(string outputName)
        {
            foreach(AudioMixerGroup output in AudioMixerGroups)
            {
                if(output.name == outputName)
                {
                    return output;
                }
            }

            return null;
        }

        public void SetLowPassValue(float newValueInHertz)
        {
            AudioMixer.SetFloat("LowPassValue", newValueInHertz);
        }

        public float GetChannelValue(string channelName)
        {
            AudioMixer.GetFloat(channelName, out float value);
            return value;
        }

        public void SetAudioMixerChannelValue(string channelParameterName, float value)
        {
            float valueInDecibel = LinearToDecibelValue(value);
            AudioMixer.SetFloat(channelParameterName, valueInDecibel);
        }

        public void FadeChannelVolume(string channelParameterName, float targetVolume, float fadeTime)
        {
            StartCoroutine(IFadeVolume(channelParameterName, targetVolume, fadeTime));        
        }

        private IEnumerator IFadeVolume(string channelParameterName, float targetVolume, float fadeDuration)
        {
            yield return new WaitUntil(() => IsFading == false);
            float startChannelVolume = GetChannelValue(channelParameterName);

            float startLerpTime = Time.unscaledTime;
            float timeSinceStarted = Time.unscaledTime - startLerpTime;
            float percentToComplete = timeSinceStarted / fadeDuration;

            targetVolume = LinearToDecibelValue(targetVolume);

            if(targetVolume != startChannelVolume)
            {
                IsFading = true;

                while(true)
                {
                    timeSinceStarted = Time.unscaledTime - startLerpTime;
                    percentToComplete = timeSinceStarted / fadeDuration;

                    float currentVolume = Mathf.Lerp(startChannelVolume, targetVolume, percentToComplete);
                    AudioMixer.SetFloat(channelParameterName, currentVolume);

                    if(percentToComplete > 1f)
                    {
                        IsFading = false;
                        break;
                    }

                    yield return new WaitForEndOfFrame();
                }
            }
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
