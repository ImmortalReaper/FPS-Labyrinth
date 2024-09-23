using FPSLabyrinth.Interface;
using UnityEngine;

namespace FPSLabyrinth.Lock
{
    // Represents a lock object in the FPS Labyrinth game, which can be interacted with
    public class Lock : MonoBehaviour, IInteractable
    {
        // References to the GameObjects representing the locked and unlocked states of the lock
        [SerializeField] private GameObject lockedLock;
        [SerializeField] private GameObject unlockedLock;
        // Reference to the Rigidbody component, used to enable physics interaction after unlocking
        [SerializeField] private Rigidbody rigidbody;
    
        private bool locked = true; // Indicates whether the lock is currently locked (true by default)
    
        public bool Locked => locked; // Public property to check if the lock is locked
    
        void Start() => LockAction();
        
        // Sets the lock to the locked state, activating the locked visuals and deactivating the unlocked visuals
        private void LockAction()
        {
            locked = true;
            lockedLock.SetActive(true);
            unlockedLock.SetActive(false);
        }

        // Unlocks the lock, switching visuals and enabling physics interaction
        private void UnlockAction()
        {
            locked = false;
            lockedLock.SetActive(false);
            unlockedLock.SetActive(true);
            transform.SetParent(null);
            rigidbody.isKinematic = false;
            gameObject.layer = LayerMask.NameToLayer("UnlockedLock");
            SoundManager.Instance.PlaySoundClip(SoundManager.Instance.SoundConfig.unlockLockSound, transform, 0.5f);
        }
    
        // Method called when the player interacts with the lock
        // Parameters:
        // - player: The player interacting with the lock
        // - interactable: The interactable object (the lock itself)
        public void Interact(Player.Player player, Interactable.Interactable interactable)
        {
            if (player.Inventory.RemoveKeys(1))
            {
                UnlockAction();
                interactable.SetInteractable(false);
            }
        }
    }
}
