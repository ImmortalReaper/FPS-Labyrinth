namespace FPSLabyrinth.Interface
{
    // Defines an interface for interactable objects in the FPS Labyrinth game
    public interface IInteractable
    {
        // Method to handle interaction between the player and an interactable object
        // Parameters:
        // - player: The player who is interacting with the object
        // - interactable: The object being interacted with
        void Interact(Player.Player player, Interactable.Interactable interactable);
    }
}
