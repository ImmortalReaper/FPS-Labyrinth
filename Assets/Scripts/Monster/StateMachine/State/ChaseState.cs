using UnityEngine;

namespace FPSLabyrinth.Monster.StateMachine.State
{
    public class ChaseState : MonsterState
    {
        private float transitionSpeed = 3.0f; // Controls the speed at which the blend and agent speed transition
        private float blendState = 1f; // Full blend to chase animation
        private float rotationSpeed = 2f; // Speed at which the monster rotates toward the player
        private bool isAttacking = false; // Tracks whether the monster is currently attacking
        private Vector3 lastPlayerPosition; // Stores the player's last known position
        private string blend = "Blend";
        private string attack = "Attack";
    
        public ChaseState(Monster monster) : base(monster) {}

        public override void EnterState()
        {
            // Enable the audio for attack state (e.g., roar sound)
            monster.AttackStateAudioSource.enabled = true;
        }

        public override void UpdateState()
        {
            // Set the destination to the player’s position if detected, otherwise use last known position
            if (monster.TargetPlayer && (monster.IsEnemyDetected || monster.IsEnemyInAttackRange))
            {
                lastPlayerPosition = monster.TargetPlayer.transform.position;
                monster.Agent.SetDestination(monster.TargetPlayer.transform.position);
            }
            else
            {
                monster.Agent.SetDestination(lastPlayerPosition);
            }
            
            // Rotate towards the player when the enemy is detected or in attack range
            if (monster.IsEnemyDetected || monster.IsEnemyInAttackRange)
            {
                LookOnPlayer();
            }
            
            // Update movement speed and transition to attacking if in range
            UpdateMovement();
            UpdateAttack();
            // Check for state transitions
            SwitchState();
        }

        private void UpdateAttack()
        {
            // Start attacking if the player is in range and the monster isn't already attacking
            if (monster.IsEnemyInAttackRange && !isAttacking)
            {
                isAttacking = true;
                monster.Animator.SetBool(attack, true);
            }
            // Stop attacking when the player is out of range
            else if (!monster.IsEnemyInAttackRange && isAttacking)
            {
                isAttacking = false;
                monster.Animator.SetBool(attack, false);
            }
        }
    
        private void UpdateMovement()
        {
            // If the player is close, reduce speed to slow approach
            if (monster.TargetPlayer && Vector3.Distance(monster.transform.position, monster.TargetPlayer.transform.position) < 1.5f)
            {
                ChangeSpeed(blend, 0, monster.RunSpeed, transitionSpeed);
            }
            else
            {
                // Otherwise, run at full speed
                ChangeSpeed(blend, blendState, monster.RunSpeed, transitionSpeed);
            }
        }

        private void LookOnPlayer()
        {
            // Rotate smoothly towards the player's position
            Vector3 directionToPlayer = (monster.TargetPlayer.transform.position - monster.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    
        private void SwitchState()
        {
            // Switch back to LookAroundState if player is lost and monster has reached the last known position
            if (!monster.IsEnemyDetected && !monster.IsEnemyInAttackRange &&
                Vector3.Distance(monster.transform.position, lastPlayerPosition) <= 1.5f)
            {
                monster.SwitchState(new LookAroundState(monster));
            }
        }
    
        public override void ExitState()
        {
            // Reset attacking state and stop the attack animation if not in range
            if (!monster.IsEnemyInAttackRange && isAttacking)
            {
                isAttacking = false;
                monster.Animator.SetBool(attack, false);
            }
            // Disable the attack sound
            monster.AttackStateAudioSource.enabled = false;
        }
    }
}
