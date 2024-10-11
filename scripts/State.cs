using Godot;

public abstract class State<T>
{
    public virtual State<T>? OnEnterState(T owner) { return null; }
    public virtual State<T>? HandleInput(T owner, InputEvent @event) { return null; }
    public virtual State<T>? HandleKeyboardInput(T owner, InputEvent @event) { return null; }
    public virtual State<T>? Process(T owner, double delta) { return null; }
    public virtual State<T>? PhysicsProcess(T owner, double delta) { return null; }
    public virtual State<T>? PhysicsProcess(T owner, double delta, ref Vector3 velocity) { return null; }
    public virtual void OnExitState(T owner) { }

    // GAME MANAGER SPECIFIC
    public virtual void StartGame(T owner) { }
}
