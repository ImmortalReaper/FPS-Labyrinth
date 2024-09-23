using FPSLabyrinth.Configs;
using UnityEngine;

namespace FPSLabyrinth
{
    // Manages sound effects in the FPS Labyrinth game
    public class SoundManager : MonoBehaviour
    {
        // Static instance of the SoundManager, allows for global access
        public static SoundManager Instance;
        // Prefab of the AudioSource used for sound effects
        [SerializeField] private AudioSource soundFXPrefab;
        // Configuration file that holds various sound effect settings
        [SerializeField] private SoundConfig soundConfig;
    
        // Public property to access the sound configuration
        public SoundConfig SoundConfig => soundConfig;
    
        // Ensures there is only one instance of SoundManager (singleton pattern)
        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }
        }

        // Plays a sound clip at a given transform's position with a specified volume
        // Parameters:
        // - clip: The sound effect to be played
        // - transform: The position where the sound will be played
        // - volume: The volume of the sound effect
        public void PlaySoundClip(AudioClip clip, Transform transform, float volume)
        {
            // Instantiates an AudioSource at the given position
            AudioSource audioSource = Instantiate(soundFXPrefab, transform.position, Quaternion.identity);
            // Sets the clip and volume for the AudioSource
            audioSource.clip = clip;
            audioSource.volume = volume;
            // Plays the sound effect
            audioSource.Play();
            // Destroys the AudioSource game object after the sound has finished playing
            Destroy(audioSource.gameObject, clip.length);
        }
    }
}
