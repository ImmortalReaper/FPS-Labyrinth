using UnityEngine;

namespace FPSLabyrinth.Monster.StateMachine.State
{
    public class WalkState : MonsterState
    {
        private Vector3 targetPosition; // The target position the monster will move towards
        private float transitionSpeed = 1.0f; // Speed of blending animation
        private float blendState = 0.5f; // Blend state for the animation
        private string blend = "Blend";
    
        public WalkState(Monster monster) : base(monster) {}

        // Called when entering this state
        public override void EnterState()
        {
            SetNewDestination(); // Set the initial destination for the monster to walk to
        }

        // Called every frame while in this state
        public override void UpdateState()
        {
            // Check if the agent has reached its destination
            if (!monster.Agent.pathPending && monster.Agent.remainingDistance <= monster.Agent.stoppingDistance)
            {
                // If there is no path or the agent is stationary, set a new destination
                if (!monster.Agent.hasPath || monster.Agent.velocity.sqrMagnitude == 0f)
                {
                    SetNewDestination();
                }
            }
            // Update the monster's animation blend and speed
            ChangeSpeed(blend, blendState, monster.WalkSpeed, transitionSpeed);
            // Check for state transitions
            SwitchState();
        }

        // Determines if the monster should switch to a different state

        private void SwitchState()
        {
            // If an enemy is detected or in attack range, switch to the ChaseState
            if (monster.IsEnemyDetected || monster.IsEnemyInAttackRange)
            {
                monster.SwitchState(new ChaseState(monster));
            }
            // If the monster is ready to look around, switch to the LookAroundState
            else if (monster.IsReadyToLookAround) 
            {
                monster.SwitchState(new LookAroundState(monster));
            }
        }
    
        // Called when exiting this state
        public override void ExitState() {}
    
        // Sets a new random destination for the monster to walk to
        private void SetNewDestination()
        {
            targetPosition = monster.GetRandomPosition(); 
            monster.Agent.SetDestination(targetPosition);
        }
    }
}
