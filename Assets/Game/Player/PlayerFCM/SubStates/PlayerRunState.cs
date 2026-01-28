using UnityEngine.UIElements;

namespace Player
{
    public class PlayerRunState : PlayerInGroundState
    {
        
        public PlayerRunState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement, PlayerData playerData) : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData)
        {
        }
        public override void DoCheck()
        {
            base.DoCheck();
        }
        public override void Enter()
        {
            base.Enter();
            PlayerMovement.CurrentSpeed = PlayerData.RunSpeed;
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            PlayerMovement.Move(PlayerInputHandler);
            CrouchCheck();
            JumpCheck(); 
            if (!PlayerInputHandler.RunPressed)
                StateMachine.ChangeState(Player.WalkState);
            if (PlayerInputHandler.MoveInput.x == 0f && PlayerInputHandler.MoveInput.y == 0f)
                StateMachine.ChangeState(Player.IdleState);      
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }

}