using System;
using UnityEngine;

namespace FPSLabyrinth.UI
{
    // Manages the game's pause functionality, including pausing and resuming the game
    public class PauseController : MonoBehaviour
    {
        // Indicates whether the game is currently paused
        private bool isPaused = false;
    
        // Public property to check if the game is paused
        public bool IsPaused => isPaused;

        // Action events triggered when the game is paused or resumed
        public Action OnPauseStart;
        public Action OnPauseEnd;
    
        // Initializes the controller by locking the cursor at the start of the game
        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        // Pauses the game by stopping time and unlocking the cursor
        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            OnPauseStart?.Invoke();
        }
        
        // Resumes the game by restoring time and locking the cursor
        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            OnPauseEnd?.Invoke();
        }
    }
}
