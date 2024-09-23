using StarterAssets;
using UnityEngine;

namespace FPSLabyrinth.Player
{
    public class FootstepSoundController : MonoBehaviour
    {
        // AudioSource for playing footstep sounds
        [SerializeField] private AudioSource audioSource;
        // Animator for controlling character animations
        [SerializeField] private Animator animator;
        // Reference to player input actions
        [SerializeField] private StarterAssetsInputs starterAssetsInputs;
        
        // Initializes the audio clip for footsteps
        private void Start()
        {
            audioSource.clip = SoundManager.Instance.SoundConfig.footstepSound;
        }

        // Plays the footstep sound when called
        public void PlayFootstepSound() => audioSource.Play();
        
        // Updates animator parameters based on player input in fixed time steps
        private void FixedUpdate()
        {
            // Sets the "Run" animation based on sprint input
            animator.SetBool("Run", starterAssetsInputs.sprint);
            // Sets the "Walk" animation based on movement input and sprint state
            animator.SetBool("Walk", !starterAssetsInputs.sprint && starterAssetsInputs.move != Vector2.zero);
        }
    }
}
