using System.Collections;
using FPSLabyrinth.Monster.StateMachine;
using FPSLabyrinth.Monster.StateMachine.State;
using UnityEngine;
using UnityEngine.AI;

namespace FPSLabyrinth.Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent; // The NavMeshAgent for pathfinding
        [SerializeField] private Animator animator; // Animator for handling animations
        [SerializeField] private AudioSource attackStateAudioSource; // Audio source for attack sounds
        [Header("Stats")]
        [SerializeField] private float damage; //Damage dealt by the monster
        [SerializeField] private float runSpeed = 3f; // Speed when the monster is running
        [SerializeField] private float walkSpeed = 1.5f; // Speed when the monster is walking
    
        private bool isEnemyDetected = false; // Indicates if an enemy is detected
        private bool isEnemyInAttackRange = false; // Indicates if the enemy is within attack range
        private bool isReadyToLookAround = false; // Indicates if the monster is ready to look around
        private GameObject targetPlayer; // Reference to the detected player
        private MonsterState currentState; // Current state of the monster

        // Public properties to expose certain fields
        public float RunSpeed => runSpeed;
        public float WalkSpeed => walkSpeed;
        public AudioSource AttackStateAudioSource => attackStateAudioSource;
        public float Damage => damage;
        public bool IsReadyToLookAround => isReadyToLookAround;
        public bool IsEnemyInAttackRange => isEnemyInAttackRange;
        public bool IsEnemyDetected => isEnemyDetected;
        public GameObject TargetPlayer => targetPlayer;
        public Animator Animator => animator;
        public NavMeshAgent Agent => agent;

        void Start()
        {
            // Initialize the audio clip for the attack sound
            attackStateAudioSource.clip = SoundManager.Instance.SoundConfig.monsterRoarSound;
            // Set the initial state of the monster to WalkState
            currentState = new WalkState(this);
            currentState.EnterState();
            // Start coroutine for looking around
            StartCoroutine(WaitToLookAround());
        }

        void Update()
        {
            // Update the current state each frame
            currentState.UpdateState();
        }

        // Switches the monster's state to a new state
        public void SwitchState(MonsterState newState)
        {
            currentState.ExitState(); 
            currentState = newState;  
            currentState.EnterState(); 
        }
    
        // Gets a random position within a certain range
        public Vector3 GetRandomPosition()
        {
            NavMeshHit hit;
            Vector3 randomDirection;
            do {
                randomDirection = Random.insideUnitSphere * 20f; 
                randomDirection += transform.position;
            } while (!NavMesh.SamplePosition(randomDirection, out hit, 10f, 1));
            return hit.position; 
        }
    
        // Called when an enemy is detected
        public void OnEnemyDetected(GameObject enemy)
        {
            isEnemyDetected = true;
            targetPlayer = enemy;
        }

        // Called when the enemy is lost
        public void OnEnemyLost()
        {
            isEnemyDetected = false;
        }

        // Called when an enemy is within attack range
        public void OnEnemyInRangeForAttack(GameObject enemy)
        {
            isEnemyInAttackRange = true;
            targetPlayer = enemy;
        }

        // Called when the enemy goes out of attack range
        public void OnEnemyOutOfRange()
        {
            isEnemyInAttackRange = false;
        }

        // Coroutine to wait before the monster can look around again
        private IEnumerator WaitToLookAround()
        {
            isReadyToLookAround = false;
            yield return new WaitForSeconds(Random.Range(30, 120));
            isReadyToLookAround = true;
        }

        // Public method to reset the look around timer
        public void UpdateLookAroundTime() => StartCoroutine(WaitToLookAround());
    }
}
