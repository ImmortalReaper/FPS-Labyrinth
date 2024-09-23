using System.Collections;
using FPSLabyrinth.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSLabyrinth.Player
{
    // Manages the flashlight functionality, including battery management and light activation
    public class Flashlight : MonoBehaviour
    {
        // Input action reference for toggling the flashlight
        [SerializeField] private InputActionReference flashlightAction;
        // Audio source for flashlight sound effects
        [SerializeField] private AudioSource flashlightAudio;
        // Light component that represents the flashlight
        [SerializeField] private Light flashlightLightSource;
        // Reference to the MenuUI for updating the flashlight battery display
        [SerializeField] private MenuUI menuUI;
        [Header("Battery")]
        // Maximum amount of battery for the flashlight
        [SerializeField] private float batteryMaxAmount = 100;
        // Rate at which the battery drains per second when the flashlight is on
        [SerializeField] private float batteryDrownSpeedInSecond = 2;
        // Cooldown time before the flashlight can use again
        [SerializeField] private float rechargeCouldown = 10;
        // Rate at which the battery recharges per second
        [SerializeField] private float rechargeInSecond = 5;
        
        // Indicates whether the flashlight is currently on
        private bool flashlightOn = false;
        // Indicates whether the flashlight is in a recharge state
        private bool rechargeState = false;
        // Current amount of battery for the flashlight
        private float flashlightBattery;
        
        // Initializes the flashlight settings and binds the input action
        private void Start()
        {
            flashlightAction.action.started += UseFlashlight;
            if(!flashlightOn) { flashlightLightSource.enabled = flashlightOn; }
            flashlightBattery = batteryMaxAmount;
        }

        // Unbinds the flashlight action when the object is destroyed
        private void OnDestroy()
        {
            flashlightAction.action.started -= UseFlashlight;
        }

        // Updates the flashlight battery status each frame
        private void Update()
        {
            if (flashlightOn)
            {
                // Drain battery while the flashlight is on
                flashlightBattery -= Time.deltaTime * batteryDrownSpeedInSecond;
                menuUI.GameplayUI.SetFlashlightBattery(flashlightBattery, batteryMaxAmount);
                if (flashlightBattery <= 0) { LowBattery(); }
            }
            else if (flashlightBattery < batteryMaxAmount)
            {
                // Recharge battery when the flashlight is off
                flashlightBattery += Time.deltaTime * rechargeInSecond;
                menuUI.GameplayUI.SetFlashlightBattery(flashlightBattery, batteryMaxAmount);
            }
        }

        // Handles the behavior when the flashlight battery is low
        private void LowBattery()
        {
            flashlightOn = false;
            flashlightLightSource.enabled = false;
            StartCoroutine(FlashlightRecharging());
        }

        // Coroutine for managing flashlight recharge delay
        private IEnumerator FlashlightRecharging()
        {
            rechargeState = true;
            yield return new WaitForSeconds(rechargeCouldown);
            rechargeState = false;
        }
        
        // Toggles the flashlight state when the action is triggered
        private void UseFlashlight(InputAction.CallbackContext obj)
        {
            if (rechargeState) { return; }
            flashlightOn = !flashlightOn;
            flashlightLightSource.enabled = flashlightOn;
            flashlightAudio.Play();
        }
    }
}
