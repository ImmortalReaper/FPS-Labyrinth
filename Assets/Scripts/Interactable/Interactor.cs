using FPSLabyrinth.Interface;
using FPSLabyrinth.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSLabyrinth.Interactable
{
    // Manages the player's ability to interact with interactable objects in the game
    public class Interactor : MonoBehaviour
    {
        // Default sprite for the interactor UI when no interactable object is in reach
        [SerializeField] private Sprite defaultInteractorImage;
        // Reference to the UI manager, specifically for interaction icon
        [SerializeField] private MenuUI menuUI;
        // Maximum distance the player can reach to interact with an object
        [SerializeField] private float playerReachDistance = 2f;
        // Input action for triggering interaction (usually a button press)
        [SerializeField] private InputActionReference interactAction;
        // Reference to the player performing the interaction
        [SerializeField] private Player.Player player;
        // Specifies which layers the object can interact with.
        [SerializeField] private LayerMask interactLayers;
    
        // Reference to the main camera for casting rays to detect interactable objects
        private Camera mainCamera;
        // Keeps track of the current interactable object in focus
        private Interactable currentInteractable;
        // Flag to indicate if the player is currently interacting
        private bool interacting = false;

        // Initializes the interactor and subscribes to interaction input events
        void Awake()
        {
            mainCamera = Camera.main;
            interactAction.action.started += StartInteracting;
            interactAction.action.canceled += StopInteracting;
        }

        // Unsubscribes from interaction input events to prevent memory leaks
        private void OnDestroy()
        {
            interactAction.action.started -= StartInteracting;
            interactAction.action.canceled -= StopInteracting;
        }
    
        // Called when the interaction input starts
        private void StartInteracting(InputAction.CallbackContext obj) => interacting = true;
        // Called when the interaction input is canceled
        private void StopInteracting(InputAction.CallbackContext obj) => interacting = false;
    
        // Updates each frame to check for interactable objects and handle interaction
        void Update()
        {
            // Casts a ray from the main camera forward to detect objects in front of the player
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            Interactable interactable = null;
            IInteractable interactableObject = null;
            // Checks if the ray hits an object within the player's reach
            if (Physics.Raycast(ray, out RaycastHit hit, playerReachDistance, interactLayers))
            {
                // Gets the Interactable and IInteractable components from the hit object
                interactable = hit.collider.GetComponent<Interactable>();
                interactableObject = hit.collider.GetComponent<IInteractable>();
                // If an interactable object is hit, update the UI and handle interaction
                if (interactable)
                {
                    // Updates the interactor image if the object is interactable
                    if (interactable.IsInteractable) { SetInteractorImage(interactable.InteractableSprite); }
                    else { SetInteractorImage(defaultInteractorImage); }
                    // If the player is interacting, trigger the interaction method
                    if (interacting)
                    {
                        interacting = false;
                        interactableObject.Interact(player, interactable);
                    }
                }
            }
            else
            {
                // Resets the UI to the default image if no interactable object is in range
                SetInteractorImage(defaultInteractorImage);
            }
            // Updates the current interactable object for visual effects like outlines
            UpdateCurrentInteractable(interactable);
        }
        
        // Updates the interactor UI image based on the object in focus
        private void SetInteractorImage(Sprite image)
        {
            if(menuUI.GameplayUI.InteractionIcon.sprite != image) menuUI.GameplayUI.SetInteractionIcon(image);
        }

        // Updates the current interactable object and handles enabling/disabling outlines
        private void UpdateCurrentInteractable(Interactable newInteractable)
        {
            // Disables the outline on the previous interactable object if it exists
            if (currentInteractable != null && currentInteractable != newInteractable)
            {
                currentInteractable.DisableOutline();
            }
            // Sets the new interactable object as the current one
            currentInteractable = newInteractable;
            // Enables the outline on the new interactable object
            if (currentInteractable != null)
            {
                currentInteractable.EnableOutline();
            }
        }
    }
}
