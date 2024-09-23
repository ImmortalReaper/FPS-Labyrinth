using FPSLabyrinth.UI;
using StarterAssets;
using UnityEngine;

namespace FPSLabyrinth.Player
{
    public class Player : MonoBehaviour
    {
        // Reference to the player's health component
        [SerializeField] private Health health;
        // Reference to the player's inventory component
        [SerializeField] private Inventory inventory;
        // Reference to input handling for player movement and actions
        [SerializeField] private StarterAssetsInputs starterAssetsInputs;
        // Reference to the pause controller to manage game pause state
        [SerializeField] private PauseController pauseController;
    
        // Property to access the player's health component
        public Health Health => health;
        // Property to access the player's inventory component
        public Inventory Inventory => inventory;

        // Subscribes to pause events to enable or disable camera control
        private void Start()
        {
            pauseController.OnPauseStart += DisableCameraControl;
            pauseController.OnPauseEnd += EnableCameraControl;
        }

        // Unsubscribes from pause events to prevent memory leaks
        private void OnDestroy()
        {
            pauseController.OnPauseStart -= DisableCameraControl;
            pauseController.OnPauseEnd -= EnableCameraControl;
        }

        // Disables camera control when the game is paused
        private void DisableCameraControl()
        {
            starterAssetsInputs.cursorInputForLook = false;
            starterAssetsInputs.look = Vector3.zero;
        }
        // Enables camera control when the game is resumed
        private void EnableCameraControl() => starterAssetsInputs.cursorInputForLook = true;
    }
}
