using System.Collections.Generic;
using FPSLabyrinth.Interface;
using FPSLabyrinth.UI;
using UnityEngine;

namespace FPSLabyrinth.Door
{
    // Represents a door that is locked and can be interacted with to check if all locks are unlocked
    public class LockedDoor : MonoBehaviour, IInteractable
    {
        // Reference to the UI manager, specifically for displaying the win screen
        [SerializeField] private MenuUI menuUI;
        // List of locks that must be unlocked for the door to be considered fully unlocked
        [SerializeField] private List<Lock.Lock> locks;
    
        // Handles interaction when the player interacts with the locked door
        // If all locks are unlocked, displays the win screen
        public void Interact(Player.Player player, Interactable.Interactable interactable)
        {
            foreach (Lock.Lock Lock in locks)
            {
                // If any lock is still locked, the interaction is canceled
                if (Lock.Locked) { return; }
            }
            // If all locks are unlocked, display the win UI
            menuUI.WinUI.Show();
        }
    }
}
