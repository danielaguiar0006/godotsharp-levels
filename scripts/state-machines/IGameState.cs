namespace Game.StateMachines;

public interface IGameState
{
    public IGameState? OnEnterState();
    public IGameState? StartGame();
    public IGameState? PhysicsProcess(double delta);
    public void OnExitState();
}
