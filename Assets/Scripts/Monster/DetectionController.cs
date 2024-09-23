using UnityEngine;

namespace FPSLabyrinth.Monster
{
    public class DetectionController : MonoBehaviour
    {
        [SerializeField] private Monster monster; // Reference to the associated monster
        [SerializeField] private float viewAngle = 90f; // The angle of vision for the monster
        [SerializeField] private LayerMask layerMask; // Layer mask to filter what the monster can see
        [SerializeField] private SphereCollider viewCollider; // Collider defining the detection range
    
        // Calculates the angle between two vectors in the XZ plane
        private float CalculateAngle(Vector3 from, Vector3 to)
        {
            Vector3 vectorA_XZ = new Vector3(from.x, 0, from.z);
            Vector3 vectorB_XZ = new Vector3(to.x, 0, to.z);
            return Vector3.Angle(vectorA_XZ, vectorB_XZ);
        }
    
        // Called when another collider stays within the trigger collider
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Calculate player's position slightly above the ground
                Vector3 playerPosition = new Vector3(other.transform.position.x, other.transform.position.y+1, other.transform.position.z);
                Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
                float angleBetweenMonsterAndPlayer = CalculateAngle(transform.up, directionToPlayer);
                // Check if the player is within the monster's field of view
                if (angleBetweenMonsterAndPlayer < viewAngle / 2f)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
                    // Check if the player is within detection radius
                    if (distanceToPlayer <= viewCollider.radius)
                    {
                        RaycastHit hit;
                        // Raycast to check for obstacles between the monster and the player
                        if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewCollider.radius, layerMask))
                        {
                            if (hit.collider.TryGetComponent(out Player.Player player))
                            {
                                // Player detected
                                monster.OnEnemyDetected(other.gameObject);
                            }
                            else
                            {
                                // Player lost due to obstruction
                                monster.OnEnemyLost();
                            }
                        }
                    }
                }
                // Player is outside the field of view
                else { monster.OnEnemyLost();  }
            }
        }

        // Called when another collider exits the trigger collider
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                // Player lost when they exit the detection area
                monster.OnEnemyLost(); 
            }
        }
    }
}
