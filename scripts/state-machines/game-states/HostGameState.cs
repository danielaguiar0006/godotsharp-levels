using Godot;

namespace Game.StateMachines;

public partial class HostGameState : IGameState
{
    public IGameState? OnEnterState()
    {
        return null;
    }

    public IGameState? StartGame()
    {
        return null;
    }

    public IGameState? PhysicsProcess(double delta)
    {
        // TODO: Implement host game state
        GD.Print("HostGameState PhysicsProcess");
        return null;
    }

    public void OnExitState()
    {
        // TODO: Implement host game state
        GD.Print("HostGameState OnExitState");
    }
}
