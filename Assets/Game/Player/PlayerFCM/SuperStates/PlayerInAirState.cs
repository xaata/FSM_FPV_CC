namespace Player
{
    public class PlayerInAirState : PlayerState
    {
        public PlayerInAirState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement, PlayerData playerData) : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData) { }
        public override void DoCheck()
        {
            base.DoCheck();
        }
        public override void Enter()
        {
            base.Enter();        
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (PlayerMovement.CheckIfGrounded() && !PlayerInputHandler.JumpPressed)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else
            {
                ApplyGravity();
                PlayerMovement.Move(PlayerInputHandler);  
            }   
            PlayerMovement.HandleCameraRotation(PlayerInputHandler, PlayerData);
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        private void ApplyGravity()
        {
            PlayerMovement.SetInAirGravity(PlayerData.Gravity);
        }
    }
}