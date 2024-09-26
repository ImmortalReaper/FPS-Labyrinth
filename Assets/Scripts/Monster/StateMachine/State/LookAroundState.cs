using UnityEngine;

namespace FPSLabyrinth.Monster.StateMachine.State
{
    public class LookAroundState : MonsterState
    {
        private float lookTime; // Tracks the elapsed time in this state
        private float lookDuration = 3f; // Duration for how long the monster looks around
        private float speed = 0; // Speed of the monster while looking around (usually stationary)
        private float transitionSpeed = 3.0f; // Speed of blending the animation
        private float blendState = 0f; // Blend state for the animation
        private string blend = "Blend";

        public LookAroundState(Monster monster) : base(monster) {}

        // Called when entering this state
        public override void EnterState()
        {
            lookTime = 0f; // Reset look time when entering the state
        }
        
        // Called every frame while in this state
        public override void UpdateState()
        {
            lookTime += Time.deltaTime; // Increment look time
            ChangeSpeed(blend, blendState, speed, transitionSpeed); // Update animation blend
            SwitchState();// Check for state transitions
        }

        // Determines if the monster should switch to a different state
        private void SwitchState()
        {
            // If an enemy is detected or in attack range, switch to the ChaseState
            if (monster.IsEnemyDetected || monster.IsEnemyInAttackRange)
            {
                monster.UpdateLookAroundTime();
                monster.SwitchState(new ChaseState(monster));
            }
            // If the look time has exceeded the duration, switch back to WalkState
            else if (lookTime >= lookDuration)
            {
                monster.UpdateLookAroundTime();
                monster.SwitchState(new WalkState(monster));
            }
        }
    
        // Called when exiting this state
        public override void ExitState() { }
    }
}
