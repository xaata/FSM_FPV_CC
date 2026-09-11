using UnityEngine;

namespace Player
{
    /// <summary>
    /// Base class for all player states in the FSM.
    /// Provides common references and lifecycle methods (Enter, Exit, LogicUpdate, PhysicsUpdate).
    /// </summary>
    public abstract class PlayerState
    {
        protected PlayerStateMachineInit Player { get; private set; }
        protected PlayerStateMachine StateMachine { get; private set; }
        protected PlayerData PlayerData { get; private set; }
        protected PlayerMovement PlayerMovement { get; private set; }
        protected PlayerInputHandler PlayerInputHandler { get; private set; }

        protected float StartTime { get; private set; }
        protected float TimeInCurrentState { get; private set; }

        public PlayerState(
            PlayerStateMachineInit player, 
            PlayerInputHandler playerInputHandler, 
            PlayerStateMachine playerStateMachine, 
            PlayerMovement playerMovement,
            PlayerData playerData)
        {
            Player = player;
            PlayerInputHandler = playerInputHandler;
            StateMachine = playerStateMachine;
            PlayerMovement = playerMovement;
            PlayerData = playerData;
        }

        /// <summary>
        /// Called when entering this state. Initialize variables here.
        /// </summary>
        public virtual void Enter()
        {
            DoCheck();
            StartTime = Time.time;
        }

        /// <summary>
        /// Called when exiting this state. Clean up here.
        /// </summary>
        public virtual void Exit()
        {
        }

        /// <summary>
        /// Called every frame in Update. Handle input and state transitions here.
        /// </summary>
        public virtual void LogicUpdate()
        {
            TimeInCurrentState = Time.time - StartTime;
        }

        /// <summary>
        /// Called every frame in FixedUpdate. Handle physics here.
        /// </summary>
        public virtual void PhysicsUpdate()
        {
            DoCheck();
        }

        /// <summary>
        /// Perform checks for state transitions. Override in derived classes.
        /// </summary>
        public virtual void DoCheck()
        {
        }
    }
}
