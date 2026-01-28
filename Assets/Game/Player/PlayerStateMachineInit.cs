using UnityEngine;
namespace Player
{
    public class PlayerStateMachineInit : MonoBehaviour // rename the class???
    {
        //--------------STATE MACHINE-------------------------------
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerInGroundState InGroundState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerWalkState WalkState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerCrouchState CrouchState { get; private set; }    
        //---------------------Player Data-----------------------------
        [SerializeField] private PlayerData _playerData;
        private PlayerMovement _playerMovement;
        private PlayerInputHandler _playerInputHandler;

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            InGroundState = new PlayerInGroundState(this, _playerInputHandler, StateMachine, _playerMovement, _playerData);
            InAirState = new PlayerInAirState(this, _playerInputHandler, StateMachine, _playerMovement, _playerData);
            IdleState = new PlayerIdleState(this, _playerInputHandler, StateMachine, _playerMovement, _playerData);
            WalkState = new PlayerWalkState(this, _playerInputHandler, StateMachine, _playerMovement, _playerData);
            RunState = new PlayerRunState(this, _playerInputHandler, StateMachine, _playerMovement, _playerData);
            JumpState = new PlayerJumpState(this, _playerInputHandler, StateMachine, _playerMovement, _playerData);
            CrouchState = new PlayerCrouchState(this, _playerInputHandler, StateMachine, _playerMovement, _playerData);
        }
        private void Start()
        {
            StateMachine.Init(IdleState);
        }
        private void Update()
        {      
            StateMachine.CurrentState.LogicUpdate(); 
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    }
}