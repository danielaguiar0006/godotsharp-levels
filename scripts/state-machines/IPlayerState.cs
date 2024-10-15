using Godot;

namespace Game.StateMachines;

public interface IPlayerState<T> where T : Player
{
    public IPlayerState<T>? OnEnterState(T playerOwner);
    public IPlayerState<T>? HandleInput(T playerOwner, InputEvent @event);
    public IPlayerState<T>? HandleKeyboardInput(T playerOwner, InputEvent @event);
    public IPlayerState<T>? Process(T playerOwner, double delta);
    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity);
    public void OnExitState(T playerOwner);
}
