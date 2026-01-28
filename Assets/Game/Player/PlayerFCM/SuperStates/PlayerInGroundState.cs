namespace Player
{
    public class PlayerInGroundState : PlayerState
    { 
        public PlayerInGroundState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement, PlayerData playerData) : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData) 
        {
        }
        public override void DoCheck()
        {
            base.DoCheck();   
        }
        public override void Enter()
        {      
            base.Enter();
            ApplyGroundedGravity();
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            PlayerMovement.HandleCameraRotation(PlayerInputHandler, PlayerData);
            CheckIfInAir();         
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        protected void JumpCheck()
        {
            if (PlayerInputHandler.JumpPressed)
                StateMachine.ChangeState(Player.JumpState);
        }
        private void CheckIfInAir()
        {
            if (!PlayerMovement.CharacterController.isGrounded) 
                StateMachine.ChangeState(Player.InAirState);        
        }
        private void ApplyGroundedGravity()
        {
            PlayerMovement.SetInGroundGravity(-0.05f);
        }
        protected void RunCheck()
        {
            if (PlayerInputHandler.RunPressed)
                StateMachine.ChangeState(Player.RunState);
        }
        protected void CrouchCheck()
        {
            if (PlayerInputHandler.CrouchPressed)
                StateMachine.ChangeState(Player.CrouchState);
        }
    }

}