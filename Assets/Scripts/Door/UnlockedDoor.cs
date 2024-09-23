using FPSLabyrinth.Interface;
using UnityEngine;

namespace FPSLabyrinth.Door
{
    // Represents a door that can be opened or closed by interacting with it
    public class UnlockedDoor : MonoBehaviour, IInteractable
    {
        // Reference to the Animator component used for controlling door animations
        [SerializeField] private Animator animator;
        // Reference to the AudioSource component for playing sound effects
        [SerializeField] private AudioSource audioSource;
        
        private readonly int openString = Animator.StringToHash("Open");
        // Tracks whether the door is currently open or closed
        private bool open = false;
        
        // Public property to check if the door is open
        public bool Open => open;
        
        // Sets up the initial sound clip for the door open sound on start
        private void Start()
        {
            audioSource.clip = SoundManager.Instance.SoundConfig.doorOpenSound;
        }
        
        // Toggles the door's open/close state and plays the appropriate animation and sound
        private void UseDoor()
        {
            open = !open;
            animator.SetBool(openString, open);
            audioSource.Play();
        }
        
        // Opens the door if it is currently closed
        public void OpenDoor()
        {
            if (!open)
            {
                open = true;
                animator.SetBool(openString, open);
                audioSource.Play();
            }
        }
        
        // Handles interaction from the player, toggling the door's open/close state
        public void Interact(Player.Player player, Interactable.Interactable interactable)
        {
            UseDoor();
        }
    }
}
