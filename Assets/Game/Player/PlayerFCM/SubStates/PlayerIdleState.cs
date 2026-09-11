namespace Player
{
    /// <summary>
    /// State when player is standing still on the ground.
    /// Transitions to Walk on movement input, Jump on jump input, Crouch on crouch input.
    /// </summary>
    public class PlayerIdleState : PlayerInGroundState
    {
        public PlayerIdleState(
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
            PlayerMovement.CurrentSpeed = 0f;
        }

        public override void Exit()
        {
            // Set walk speed for smooth transition to air states
            PlayerMovement.CurrentSpeed = PlayerData.WalkSpeed;
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // Check for state transitions
            CrouchCheck();
            JumpCheck();
            
            // Transition to walk if moving
            if (PlayerInputHandler.MoveInput.magnitude > 0f)
            {      
                StateMachine.ChangeState(Player.WalkState);
            }
        }
    }
}
