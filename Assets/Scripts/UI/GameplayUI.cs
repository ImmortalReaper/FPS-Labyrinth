using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FPSLabyrinth.UI
{
    // Manages the gameplay user interface elements, including key count, interaction icon, and flashlight battery status
    public class GameplayUI : MonoBehaviour
    {
        // Reference to the StopwatchUI component for tracking time
        [SerializeField] private StopwatchUI stopwatch;
        // Text field displaying the amount of keys the player has
        [SerializeField] private TextMeshProUGUI keysAmount;
        // Image representing the interaction icon shown to the player
        [SerializeField] private Image interactionIcon;
        // Image representing the flashlight battery level
        [SerializeField] private Image flashlightBatteryUI;

        // Public property to access the interaction icon
        public Image InteractionIcon => interactionIcon;
        
        // Updates the displayed amount of keys
        public void SetKeysAmount(int amount)
        {
            keysAmount.text = amount.ToString();
        }
        
        // Updates the flashlight battery UI based on current and maximum battery levels
        public void SetFlashlightBattery(float flashlightBattery, float maxFlashlightBattery)
        {
            flashlightBatteryUI.fillAmount = flashlightBattery / maxFlashlightBattery;
        }
        
        // Updates the interaction icon sprite displayed in the UI
        public void SetInteractionIcon(Sprite interactionIcon)
        {
            this.interactionIcon.sprite = interactionIcon;
        }
    }
}
