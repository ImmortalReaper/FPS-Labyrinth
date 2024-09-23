using UnityEngine;

namespace FPSLabyrinth.UI
{
    // Manages the various user interface elements for the game, including gameplay, pause, and end screens
    public class MenuUI : MonoBehaviour
    {
        // Reference to the gameplay UI component
        [SerializeField] private GameplayUI gameplayUI;
        // Reference to the pause UI component
        [SerializeField] private PauseUI pauseUI;
        // Reference to the UI displayed when the player wins
        [SerializeField] private BaseEndUI winUI;
        // Reference to the UI displayed when the player loses
        [SerializeField] private BaseEndUI loseUI;
    
        // Public property to access the gameplay UI
        public GameplayUI GameplayUI => gameplayUI;
        // Public property to access the pause UI
        public PauseUI PauseUI => pauseUI;
        // Public property to access the win UI
        public BaseEndUI WinUI => winUI;
        // Public property to access the lose UI
        public BaseEndUI LoseUI => loseUI;
    }
}
