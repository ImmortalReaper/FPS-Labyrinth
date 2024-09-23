using FPSLabyrinth.Interface;
using UnityEngine;

namespace FPSLabyrinth.Lock
{
    // Represents a key object in the FPS Labyrinth game, which can be interacted with
    public class Key : MonoBehaviour, IInteractable
    {
        // Method that defines what happens when the player interacts with the key
        // Parameters:
        // - player: The player interacting with the key
        // - interactable: The interactable object, though not used directly in this case
        public void Interact(Player.Player player, Interactable.Interactable interactable)
        {
            player.Inventory.AddKeys(1);
            SoundManager.Instance.PlaySoundClip(SoundManager.Instance.SoundConfig.pickUpKeySound, transform, 0.5f);
            gameObject.SetActive(false);
        }
    }
}
