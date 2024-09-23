using TMPro;
using UnityEngine;

namespace FPSLabyrinth.UI
{
    // Manages a stopwatch UI, tracking elapsed time and displaying it
    public class StopwatchUI : MonoBehaviour
    {
        // Reference to the UI text element for displaying the timer
        public TextMeshProUGUI timerText;
        // Total time elapsed since the timer started
        private float timeElapsed = 0f;
        // Indicates whether the timer is currently running
        private bool isRunning = false;

        // Initializes the stopwatch by starting the timer
        private void Start() => StartTimer();

        // Updates the timer each frame if it is running
        void Update()
        {
            if (isRunning)
            {
                timeElapsed += Time.deltaTime;
                UpdateTimerDisplay(timeElapsed);
            }
        }

        // Starts or resumes the timer
        public void StartTimer()
        {
            isRunning = true;
        }

        // Stops the timer
        public void StopTimer()
        {
            isRunning = false;
        }

        // Resets the timer to zero and updates the display
        public void ResetTimer()
        {
            timeElapsed = 0f;
            UpdateTimerDisplay(timeElapsed);
        }

        // Updates the displayed timer text based on the elapsed time
        private void UpdateTimerDisplay(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60); // Calculate minutes
            float seconds = Mathf.FloorToInt(timeToDisplay % 60); // Calculate seconds
            // Format the timer text as MM:SS
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
