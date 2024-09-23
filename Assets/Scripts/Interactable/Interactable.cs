using EPOOutline;
using UnityEngine;

namespace FPSLabyrinth.Interactable
{
    [RequireComponent(typeof(Outlinable))]
    // Represents an interactable object in the FPS Labyrinth game, capable of showing outlines and interaction states
    public class Interactable : MonoBehaviour
    {
        // Sprite to be displayed in the UI when the object is interactable
        [SerializeField] private Sprite interactableSprite;
        // If true, the outline will always be enabled, regardless of other conditions
        [SerializeField] private bool alwaysOutline;
        // If true, prevents the outline from being enabled under any conditions
        [SerializeField] private bool disableOutline;
        // Determines whether the object is currently interactable (true by default)
        [SerializeField] private bool isInteractable = true;
    
        // Reference to the Outlinable component, used to control the object's outline effect
        private Outlinable outlinable;
        
        // Public property to check if the object is interactable
        public bool IsInteractable => isInteractable;
        // Public property to get the sprite used when the object is interactable
        public Sprite InteractableSprite => interactableSprite;

        // Initializes the component, setting up the outline and interaction states
        private void Start()
        {
            outlinable = GetComponent<Outlinable>();
            DisableOutline();
            if (alwaysOutline) { outlinable.enabled = true; }
        }
    
        // Enables the outline effect on the object if the object is interactable
        public void EnableOutline()
        {
            if(alwaysOutline || disableOutline) return;
            // Enables the outline only if the object is currently interactable
            if (isInteractable)
            {
                outlinable.enabled = true;
            }
        }
        // Disables the outline effect on the object
        public void DisableOutline()
        {
            if(alwaysOutline || disableOutline) return;
            outlinable.enabled = false;
        }
    
        // Sets the object's interactable state
        // If the object is no longer interactable, disables its outline
        public void SetInteractable(bool value)
        {
            isInteractable = value;
            // If the object is no longer interactable, ensure the outline is disabled
            if (!value)
            {
                DisableOutline();
            }
        }
    }
}
