public class PlayerStateMachine
{
    private PlayerState currentState;
    private PlayerState lastState;

    public void ChangeState(PlayerState newState)
    {
        lastState = currentState;
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public PlayerState GetLastState()
    {
        return lastState;
    }
}
