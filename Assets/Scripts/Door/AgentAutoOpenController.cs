using UnityEngine;

namespace FPSLabyrinth.Door
{
    // This class automatically opens a door when a specific agent (e.g., a monster) enters the trigger zone
    public class AgentAutoOpenController : MonoBehaviour
    {
        // Reference to the UnlockedDoor component that controls the door
        [SerializeField] private UnlockedDoor unlockedDoor;

        // Trigger event that opens the door when a monster enters the trigger zone
        private void OnTriggerEnter(Collider other)
        {
            // Check if the object entering the trigger zone has a Monster component
            if (other.TryGetComponent(out Monster.Monster monster))
            {
                unlockedDoor.OpenDoor();
            }
        }
    }
}
