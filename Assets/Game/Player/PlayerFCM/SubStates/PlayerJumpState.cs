using UnityEngine;

namespace Player
{
    /// <summary>
    /// State when player is jumping in the air.
    /// Calculates jump velocity based on desired jump height and time to apex.
    /// Extends PlayerInAirState for gravity and air movement handling.
    /// </summary>
    public class PlayerJumpState : PlayerInAirState
    {
        private float _initialJumpVelocity;
        private float _originalStepOffset;
        
        public PlayerJumpState(
            PlayerStateMachineInit player, 
            PlayerInputHandler playerInputHandler, 
            PlayerStateMachine playerStateMachine, 
            PlayerMovement playerMovement, 
            PlayerData playerData) 
            : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData)
        {
            // Calculate gravity and jump velocity from design parameters
            float timeToApex = playerData.MaxJumpTime / 2f;
            playerData.Gravity = (-2f * playerData.MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2f * playerData.MaxJumpHeight) / timeToApex;     
        }

        public override void Enter()
        {
            base.Enter();
            
            // Store original step offset and disable it for consistent jump behavior
            _originalStepOffset = PlayerMovement.CharacterController.stepOffset;
            PlayerMovement.CharacterController.stepOffset = 0f;
            
            Jump();      
        }

        public override void Exit()
        {
            // Restore original step offset
            PlayerMovement.CharacterController.stepOffset = _originalStepOffset;
            base.Exit();
        }

        /// <summary>
        /// Applies initial jump velocity to the character
        /// </summary>
        private void Jump()
        {
            PlayerMovement.SetJumpVelocity(_initialJumpVelocity);
        }
    }
}
