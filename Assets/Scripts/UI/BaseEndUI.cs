using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPSLabyrinth.UI
{
    // Controls the end game UI, allowing the player to restart the game or quit
    public class BaseEndUI : MonoBehaviour
    {
        // Reference to the UI content container, which holds the UI elements
        [SerializeField] private Transform content;
        // Button to restart the game
        [SerializeField] private Button restartButton;
        // Button to exit the game
        [SerializeField] private Button exitButton;
        // Reference to the pause controller for managing game pause state
        [SerializeField] private PauseController pauseController;
        // The index of the scene to load when restarting the game
        [SerializeField] private int sceneIndex;

        // Sets up listeners for the restart and exit buttons when the script starts
        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
            exitButton.onClick.AddListener(QuitGame);
        }
    
        // Restarts the game by resuming and loading the specified scene
        private void RestartGame()
        {
            pauseController.ResumeGame();
            SceneManager.LoadScene(sceneIndex);
        }

        // Quits the application
        private void QuitGame() => Application.Quit();
        // Displays the end game UI and pauses the game
        public void Show()
        {
            pauseController.PauseGame();
            content.gameObject.SetActive(true);
        }
        // Hides the end game UI and resumes the game
        
        public void Hide()
        {
            pauseController.ResumeGame();
            content.gameObject.SetActive(false);
        }
    }
}
