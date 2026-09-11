namespace Player
{
    /// <summary>
    /// State when player is crouching on the ground.
    /// Reduces character height and movement speed.
    /// Prevents standing up if there's an obstacle above.
    /// </summary>
    public class PlayerCrouchState : PlayerInGroundState
    {
        private bool _canStand;
        
        public PlayerCrouchState(
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
            PlayerMovement.CurrentSpeed = PlayerData.CrouchSpeed;
            PlayerMovement.Crouch(PlayerData);
        }

        public override void Exit()
        {
            PlayerMovement.Uncrouch();
            base.Exit();    
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // Move character
            PlayerMovement.Move(PlayerInputHandler.MoveInput, PlayerMovement.CameraTransform);
            
            // Check for state transitions
            if (!PlayerInputHandler.CrouchPressed && _canStand)
                StateMachine.ChangeState(Player.IdleState);
                
            if (PlayerInputHandler.JumpPressed && _canStand)
                StateMachine.ChangeState(Player.JumpState);
        } 
        
        public override void PhysicsUpdate()
        {       
            base.PhysicsUpdate();
            CheckIfCanStand();
        }
        
        /// <summary>
        /// Checks if there's enough space above to stand up
        /// </summary>
        private void CheckIfCanStand()
        {
            _canStand = PlayerMovement.CanStandUp();
        }
    }
}
