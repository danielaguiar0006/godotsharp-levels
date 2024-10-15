using System.Diagnostics;

namespace Game.StateMachines;

public class GameStateMachine
{
    public IGameState? m_CurrentState { get; private set; }

    public GameStateMachine() { }

    public void ChangeState(IGameState newState)
    {
        Debug.Assert(newState != null, "New state cannot be null");

        // EXIT CURRENT STATE
        if (m_CurrentState != null)
        {
            m_CurrentState.OnExitState();
        }

        // SET NEW CURRENT STATE
        m_CurrentState = newState;

        // CAPTURE POSSIBLE NEXT STATE
        IGameState? nextState = m_CurrentState.OnEnterState();

        // CHANGE STATE IF NEXT STATE IS NOT NULL
        if (nextState != null)
        {
            ChangeState(nextState);
        }
    }

    public void StartGame()
    {
        if (m_CurrentState != null)
        {
            IGameState? newState = m_CurrentState.StartGame();
            if (newState != null)
            {
                ChangeState(newState);
            }
        }
    }

    // Called during the physics processing step of the main loop.
    // Physics processing means that the frame rate is synced to the physics,
    // i.e. the `delta` variable should be constant (`delta` is in seconds).
    // returns the new state if the state changes, otherwise returns null
    // ?: This one may be unnecessary
    public void PhysicsProcess(double delta)
    {
        if (m_CurrentState != null)
        {
            IGameState? newState = m_CurrentState.PhysicsProcess(delta);
            if (newState != null)
            {
                ChangeState(newState);
            }
        }
    }
}
