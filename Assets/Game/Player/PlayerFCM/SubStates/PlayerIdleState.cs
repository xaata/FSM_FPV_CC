namespace Player
{
    public class PlayerIdleState : PlayerInGroundState
    {
        public PlayerIdleState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement, PlayerData playerData) : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData)
        {
        }
        public override void DoCheck()
        {
            base.DoCheck();
        }
        public override void Enter()
        {
            base.Enter();
            PlayerMovement.CurrentSpeed = 0;
        }
        public override void Exit()
        {
            PlayerMovement.CurrentSpeed = PlayerData.WalkSpeed;//Assigns walk speed to curr speed in order to be able move in fall state and jump state
            base.Exit();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            CrouchCheck();
            JumpCheck();
            if (PlayerInputHandler.MoveInput.x != 0f || PlayerInputHandler.MoveInput.y != 0f)
            {      
                StateMachine.ChangeState(Player.WalkState);
            }
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}