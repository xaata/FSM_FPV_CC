namespace Player
{
    public class PlayerJumpState : PlayerInAirState
    {
        private float _initialJumpVelocity;
        private float _originalStepOffset;
        public PlayerJumpState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement, PlayerData playerData) : base(player, playerInputHandler, playerStateMachine, playerMovement, playerData)
        {
            float timeToApex = playerData.MaxJumpTime / 2;
            playerData.Gravity = (-2 * playerData.MaxJumpHeight) / UnityEngine.Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2 * playerData.MaxJumpHeight) / timeToApex;     
        }
        public override void Enter()
        {
            base.Enter();
            _originalStepOffset = PlayerMovement.CharacterController.stepOffset;
            PlayerMovement.CharacterController.stepOffset = 0f;
            Jump();      
        }
        public override void Exit()
        {
            PlayerMovement.CharacterController.stepOffset = _originalStepOffset;
            base.Exit();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
        private void Jump()
        {
            PlayerMovement.SetJumpVelocity(_initialJumpVelocity);
        }
    }
}