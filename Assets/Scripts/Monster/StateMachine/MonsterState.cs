using UnityEngine;

namespace FPSLabyrinth.Monster.StateMachine
{
    // Abstract base class for monster states in the state machine
    public abstract class MonsterState 
    {
        // Reference to the monster associated with this state
        protected Monster monster;

        // Constructor that assigns the monster reference
        public MonsterState(Monster monster)
        {
            this.monster = monster;
        }

        // Method to be called when entering this state
        public abstract void EnterState();
        // Method to be called on each frame update while in this state
        public abstract void UpdateState();
        // Method to be called when exiting this state
        public abstract void ExitState();

        // Helper method to smoothly change the monster's speed and animation blend
        protected void ChangeSpeed(string blendNameAgrument, float endBlendState, float endSpeed, float transitionSpeed)
        {
            // Get the current blend value for the animation
            float blend = monster.Animator.GetFloat(blendNameAgrument);
            // Lerp the blend value towards the target blend state for smooth animation transition
            monster.Animator.SetFloat(blendNameAgrument, Mathf.Lerp(blend, endBlendState, Time.deltaTime * transitionSpeed));
            // Lerp the agent's speed towards the target speed for smooth movement transition
            monster.Agent.speed = Mathf.Lerp(monster.Agent.speed, endSpeed, Time.deltaTime * transitionSpeed);
        }
    }
}
