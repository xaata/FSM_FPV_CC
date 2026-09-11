namespace Player
{
    /// <summary>
    /// Super-state for all air-based player states (Jump, Fall).
    /// Handles gravity application and air movement.
    /// </summary>
    public class PlayerInAirState : PlayerState
    {
        public PlayerInAirState(
            PlayerStateMachineInit player, 
            PlayerInputHandler playerInputHandler, 
            PlayerStateMachine playerStateMachine, 
            PlayerMovement playerMovement, 
            PlayerData playerData) 
            : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData) 
        { 
        }

        public override void Enter()
        {
            base.Enter();        
        }

        public override void LogicUpdate()
        {
            // Check if landed on ground
            if (PlayerMovement.CheckIfGrounded() && !PlayerInputHandler.JumpPressed)
            {
                StateMachine.ChangeState(Player.IdleState);
                return;
            }
            
            // Apply gravity and move
            ApplyGravity();
            PlayerMovement.Move(PlayerInputHandler.MoveInput, PlayerMovement.CameraTransform);
            
            // Handle camera rotation
            PlayerMovement.HandleCameraRotation(PlayerInputHandler.LookInput, PlayerData);
        }

        /// <summary>
        /// Applies gravity force while in air
        /// </summary>
        protected virtual void ApplyGravity()
        {
            PlayerMovement.ApplyAirGravity(PlayerData.Gravity);
        }
    }
}
