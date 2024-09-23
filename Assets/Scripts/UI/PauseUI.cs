using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FPSLabyrinth.UI
{
    // Manages the pause menu UI, allowing the player to continue or exit the game
    public class PauseUI : MonoBehaviour
    {
        // Reference to the pause menu UI element
        [SerializeField] private Transform pauseMenu;
        // Reference to the PauseController for managing the game's pause state
        [SerializeField] private PauseController pauseController;
        // Button to continue the game and disable the pause menu
        [SerializeField] private Button continueButton;
        // Button to exit the game
        [SerializeField] private Button exitButton;
        // Input action reference for detecting the escape key press to show the pause menu
        [SerializeField] private InputActionReference escapeAction;

        // Sets up button listeners and input action at the start
        private void Start()
        {
            continueButton.onClick.AddListener(DisablePauseMenu);
            exitButton.onClick.AddListener(QuitGame);
            escapeAction.action.started += ShowPauseMenu;
        }

        // Unbinds the escape action when the object is destroyed
        private void OnDestroy()
        {
            escapeAction.action.started -= ShowPauseMenu;
        }

        // Shows the pause menu when the escape action is triggered
        private void ShowPauseMenu(InputAction.CallbackContext obj) => EnablePauseMenu();
    
        // Activates the pause menu and pauses the game
        public void EnablePauseMenu()
        {
            pauseMenu.gameObject.SetActive(true);
            pauseController.PauseGame();
        }

        // Deactivates the pause menu and resumes the game
        public void DisablePauseMenu()
        {
            pauseMenu.gameObject.SetActive(false);
            pauseController.ResumeGame();
        }

        // Exits the application
        private void QuitGame() => Application.Quit();
    }
}
