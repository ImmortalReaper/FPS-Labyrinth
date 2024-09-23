using FPSLabyrinth.UI;
using UnityEngine;

namespace FPSLabyrinth.Player
{
    public class Inventory : MonoBehaviour
    {
        // Reference to the UI menu for displaying inventory information
        [SerializeField] private MenuUI menuUI;
        // Current number of keys held by the player
        [SerializeField] private int keys = 0;

        // Initializes the inventory and updates the UI to show the current number of keys
        private void Start() => menuUI.GameplayUI.SetKeysAmount(keys);

        // Adds a specified number of keys to the inventory
        public void AddKeys(int amount)
        {
            if (amount > 0)
            {
                keys += amount;
                menuUI.GameplayUI.SetKeysAmount(keys);
            }   
        }

        // Removes a specified number of keys from the inventory if enough keys are available
        public bool RemoveKeys(int amount)
        {
            if (keys >= amount)
            {
                keys -= amount; 
                menuUI.GameplayUI.SetKeysAmount(keys);
                return true;
            }
            return false;
        }
    }
}
