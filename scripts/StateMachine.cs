using Godot;
using System.Diagnostics;

public class StateMachine<T>
{
    public T m_Owner { get; private set; }
    public State<T> m_CurrentState { get; private set; }

    public StateMachine(T owner)
    {
        m_Owner = owner;
    }

    public void ChangeState(State<T> newState)
    {
        Debug.Assert(newState != null, "New state cannot be null");

        // EXIT CURRENT STATE
        if (m_CurrentState != null)
        {
            m_CurrentState.OnExitState(m_Owner);
        }

        // SET NEW CURRENT STATE
        m_CurrentState = newState;

        // CAPTURE POSSIBLE NEXT STATE
        State<T>? nextState = m_CurrentState.OnEnterState(m_Owner);

        // CHANGE STATE IF NEXT STATE IS NOT NULL
        if (nextState != null)
        {
            ChangeState(nextState);
        }
    }

    // NOTE: These Input functions are the preferred way to handle SUDDEN input in Godot;
    // NOT for continuous input (like moving the player character). For continuous input,
    // use _Process() or _PhysicsProcess().
    //
    // !: Checking for sudden input in PhysicsProcess() specifically can lead to missed input events.
    //
    // Usage ex:
    // When pressing a mouse side button to dodge for example, HandleInput logic/code will be run;
    // If you press a keyboard key to dodge instead, HandleKeyboardInput logic/code will be run.
    // ----------------------------------------------------------------------------------
    // For gameplay input, both keyboard and mouse (+controller)
    // If it is handled by the InputMap system, is should be accessible through the Input singleton.
    // returns the new state if the state changes, otherwise returns null
    public void HandleInput(InputEvent @event)
    {
        if (m_CurrentState != null)
        {
            State<T> newState = m_CurrentState.HandleInput(m_Owner, @event);
            if (newState != null)
            {
                ChangeState(newState);
            }
        }
    }

    // returns the new state if the state changes, otherwise returns null
    // Only called if the input event is a keyboard event.
    // NOTE: To handle keyboard events, use this function instead for performance reasons.
    public void HandleKeyboardInput(InputEvent @event)
    {
        if (m_CurrentState != null)
        {
            State<T> newState = m_CurrentState.HandleKeyboardInput(m_Owner, @event);
            if (newState != null)
            {
                ChangeState(newState);
            }
        }
    }

    // Called during the processing step of the main loop. 
    // Processing happens at every frame and as fast as possible,
    // so the `delta` time since the previous frame is not constant (`delta` is in seconds).
    // returns the new state if the state changes, otherwise returns null
    public void Process(double delta)
    {
        if (m_CurrentState != null)
        {
            State<T> newState = m_CurrentState.Process(m_Owner, delta);
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
    public void PhysicsProcess(double delta)
    {
        if (m_CurrentState != null)
        {
            State<T> newState = m_CurrentState.PhysicsProcess(m_Owner, delta);
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
    public void PhysicsProcess(double delta, ref Vector3 velocity)
    {
        if (m_CurrentState != null)
        {
            State<T> newState = m_CurrentState.PhysicsProcess(m_Owner, delta, ref velocity);
            if (newState != null)
            {
                ChangeState(newState);
            }
        }
    }
}
