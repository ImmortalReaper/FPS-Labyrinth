using UnityEngine;

namespace FPSLabyrinth.Monster
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private Monster monster; // Reference to the monster controlling the attack
        [SerializeField] private Collider damageCollider; // Collider used to detect players in attack range
        
        // Enable the damage collider to detect player hits
        public void EnableAttackCollider() => damageCollider.enabled = true;
        // Disable the damage collider to stop detecting hits
        public void DisableAttackCollider() => damageCollider.enabled = false;
        
        // Triggered when another collider enters the damage collider
        private void OnTriggerEnter(Collider other)
        {
            // Check if the collider belongs to a player
            if (other.TryGetComponent(out Player.Player player)) 
            {
                // Notify the monster that the player is in range for an attack
                monster.OnEnemyInRangeForAttack(other.gameObject);
            }
        }

        // Triggered when another collider exits the damage collider
        private void OnTriggerExit(Collider other)
        {
            // Check if the collider belongs to a player
            if (other.TryGetComponent(out Player.Player player))
            {
                // Notify the monster that the player is out of attack range
                monster.OnEnemyOutOfRange();
            }
        }
    }
}
