namespace Player
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }
        public void Init(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();           
            CurrentState = newState;
            UnityEngine.Debug.Log(CurrentState);
            CurrentState.Enter();
        }
    }
}