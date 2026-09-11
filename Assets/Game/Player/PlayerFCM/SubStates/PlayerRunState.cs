namespace Player
{
    /// <summary>
    /// State when player is running/sprinting on the ground.
    /// Transitions to Walk when releasing sprint, Idle when stopping, Jump/Crouch on respective inputs.
    /// </summary>
    public class PlayerRunState : PlayerInGroundState
    {
        public PlayerRunState(
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
            PlayerMovement.CurrentSpeed = PlayerData.RunSpeed;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // Move character
            PlayerMovement.Move(PlayerInputHandler.MoveInput, PlayerMovement.CameraTransform);
            
            // Check for state transitions
            CrouchCheck();
            JumpCheck(); 
            
            // Transition to walk if not sprinting
            if (!PlayerInputHandler.RunPressed)
                StateMachine.ChangeState(Player.WalkState);
                
            // Transition to idle if no movement
            if (PlayerInputHandler.MoveInput.magnitude == 0f)
                StateMachine.ChangeState(Player.IdleState);      
        }
    }
}
