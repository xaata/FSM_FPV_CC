using UnityEngine;

namespace Player
{
    /// <summary>
    /// Main MonoBehaviour that initializes and runs the player state machine.
    /// Attach this to your player GameObject along with PlayerMovement and PlayerInputHandler.
    /// </summary>
    public class PlayerStateMachineInit : MonoBehaviour
    {
        #region State Machine Properties
        
        /// <summary> Core FSM controller </summary>
        public PlayerStateMachine StateMachine { get; private set; }
        
        /// <summary> Super-state: Player is on the ground </summary>
        public PlayerInGroundState InGroundState { get; private set; }
        
        /// <summary> Super-state: Player is in the air </summary>
        public PlayerInAirState InAirState { get; private set; }
        
        /// <summary> Sub-state: Standing still </summary>
        public PlayerIdleState IdleState { get; private set; }
        
        /// <summary> Sub-state: Walking </summary>
        public PlayerWalkState WalkState { get; private set; }
        
        /// <summary> Sub-state: Running/Sprinting </summary>
        public PlayerRunState RunState { get; private set; }
        
        /// <summary> Sub-state: Jumping </summary>
        public PlayerJumpState JumpState { get; private set; }
        
        /// <summary> Sub-state: Crouching </summary>
        public PlayerCrouchState CrouchState { get; private set; }    
        
        #endregion

        #region Serialized Fields
        
        [Header("Player Data")]
        [Tooltip("Reference to PlayerData ScriptableObject with all configurable parameters")]
        [SerializeField] private PlayerData _playerData;
        
        #endregion

        #region Component References
        
        private PlayerMovement _playerMovement;
        private PlayerInputHandler _playerInputHandler;
        
        #endregion

        private void Awake()
        {
            // Initialize state machine
            StateMachine = new PlayerStateMachine();
            
            // Get component references
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            
            // Validate required components
            if (_playerMovement == null)
                Debug.LogError("[PlayerStateMachineInit] PlayerMovement component is missing!");
            
            if (_playerInputHandler == null)
                Debug.LogError("[PlayerStateMachineInit] PlayerInputHandler component is missing!");
            
            if (_playerData == null)
                Debug.LogError("[PlayerStateMachineInit] PlayerData asset is not assigned!");

            // Create all states
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
            // Initialize with Idle state
            StateMachine.Init(IdleState);
        }

        private void Update()
        {      
            // Handle input-based logic and state transitions
            StateMachine.CurrentState.LogicUpdate(); 
        }

        private void FixedUpdate()
        {
            // Handle physics updates
            StateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
