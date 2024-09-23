using UnityEngine;

namespace FPSLabyrinth.Monster
{
    public class DamageController : MonoBehaviour
    {
        [SerializeField] private Monster monster; // Reference to the associated monster
        
        // Called when another collider enters the trigger collider
        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object has a Player component
            if (other.TryGetComponent(out Player.Player player))
            {
                // Apply damage to the player's health
                player.Health.TakeDamage(monster.Damage);
            }
        }
    }
}
