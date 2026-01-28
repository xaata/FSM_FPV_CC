namespace Player
{
    public class PlayerCrouchState : PlayerInGroundState
    {
        private bool _canStand;
        public PlayerCrouchState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement, PlayerData playerData) : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData)
        {
        }
        public override void DoCheck()
        {
            base.DoCheck();
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
            PlayerMovement.Move(PlayerInputHandler);
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
        private void CheckIfCanStand()
        {
            _canStand = PlayerMovement.CanStandUp();
        }
    }
}