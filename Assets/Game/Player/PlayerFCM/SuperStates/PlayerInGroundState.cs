namespace Player
{
    /// <summary>
    /// Super-state for all ground-based player states (Idle, Walk, Run, Crouch).
    /// Handles common ground checks and gravity application.
    /// </summary>
    public class PlayerInGroundState : PlayerState
    { 
        public PlayerInGroundState(
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
            ApplyGroundedGravity();
        }

        public override void LogicUpdate()
        {
            PlayerMovement.HandleCameraRotation(PlayerInputHandler.LookInput, PlayerData);
            CheckIfInAir();         
        }

        /// <summary>
        /// Checks if player should transition to air state
        /// </summary>
        protected void CheckIfInAir()
        {
            if (!PlayerMovement.CheckIfGrounded()) 
                StateMachine.ChangeState(Player.InAirState);        
        }

        /// <summary>
        /// Applies minimal gravity to keep player grounded
        /// </summary>
        protected void ApplyGroundedGravity()
        {
            PlayerMovement.ApplyGroundGravity(-0.1f);
        }

        /// <summary>
        /// Checks for jump input and transitions to jump state
        /// </summary>
        protected void JumpCheck()
        {
            if (PlayerInputHandler.JumpPressed)
                StateMachine.ChangeState(Player.JumpState);
        }

        /// <summary>
        /// Checks for run input and transitions to run state
        /// </summary>
        protected void RunCheck()
        {
            if (PlayerInputHandler.RunPressed)
                StateMachine.ChangeState(Player.RunState);
        }

        /// <summary>
        /// Checks for crouch input and transitions to crouch state
        /// </summary>
        protected void CrouchCheck()
        {
            if (PlayerInputHandler.CrouchPressed)
                StateMachine.ChangeState(Player.CrouchState);
        }
    }
}
