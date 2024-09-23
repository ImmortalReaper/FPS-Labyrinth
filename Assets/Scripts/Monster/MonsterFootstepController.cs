using UnityEngine;

namespace FPSLabyrinth.Monster
{
    public class MonsterFootstepController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource; // Reference to the AudioSource for playing footstep sounds
        
        // Assign the footstep sound clip from the SoundManager
        void Start() => audioSource.clip = SoundManager.Instance.SoundConfig.footstepSound; 
        // Method to play the footstep sound
        public void PlayFootstepSound() => audioSource.Play();
    }
}
