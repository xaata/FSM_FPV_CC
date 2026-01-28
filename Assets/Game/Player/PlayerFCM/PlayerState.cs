using UnityEngine;
namespace Player
{
    public class PlayerState
    {
        protected PlayerStateMachineInit Player;
        protected PlayerStateMachine StateMachine;
        protected PlayerData PlayerData;
        protected PlayerMovement PlayerMovement;
        protected PlayerInputHandler PlayerInputHandler;

        protected float StartTime;
        protected float TimeInCurrentState;

        public PlayerState(PlayerStateMachineInit player, PlayerInputHandler playerInputHandler, PlayerStateMachine playerStateMachine, PlayerMovement playerMovement,PlayerData playerData)
        {
            Player = player;
            PlayerInputHandler = playerInputHandler;
            StateMachine = playerStateMachine;
            PlayerMovement = playerMovement;    
            PlayerData = playerData ;
        }
        public virtual void Enter()
        {
            DoCheck();
            StartTime = Time.time;
        }
        public virtual void Exit()
        {
        }
        public virtual void LogicUpdate()
        {
        }
        public virtual void PhysicsUpdate()
        {
            DoCheck();
        }
        public virtual void DoCheck()
        {
        }
    }
}