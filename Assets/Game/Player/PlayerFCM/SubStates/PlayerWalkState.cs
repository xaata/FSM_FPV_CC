namespace Player
{
    /// <summary>
    /// State when player is walking on the ground.
    /// Transitions to Idle when stopping, Run when sprinting, Jump/Crouch on respective inputs.
    /// </summary>
    public class PlayerWalkState : PlayerInGroundState
    {
        public PlayerWalkState(
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
            PlayerMovement.CurrentSpeed = PlayerData.WalkSpeed;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // Check for state transitions
            JumpCheck();
            CrouchCheck();
            RunCheck();
            
            // Move character
            PlayerMovement.Move(PlayerInputHandler.MoveInput, PlayerMovement.CameraTransform);
            
            // Transition to idle if no movement input
            if (PlayerInputHandler.MoveInput.magnitude == 0f)
                StateMachine.ChangeState(Player.IdleState);
        }
    }
}
