namespace Player
{
    public class PlayerWalkState : PlayerInGroundState
    {
        public PlayerWalkState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement, PlayerData playerData) : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData)
        {
        }
        public override void DoCheck()
        {
            base.DoCheck();
        }
        public override void Enter()
        {
            base.Enter();
            PlayerMovement.CurrentSpeed = PlayerData.WalkSpeed;
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            JumpCheck();
            CrouchCheck();
            RunCheck();
            PlayerMovement.Move(PlayerInputHandler);
            if (PlayerInputHandler.MoveInput.x == 0f && PlayerInputHandler.MoveInput.y == 0f)
                StateMachine.ChangeState(Player.IdleState);
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}