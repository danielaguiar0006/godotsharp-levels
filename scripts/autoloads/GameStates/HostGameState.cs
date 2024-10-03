using Godot;

namespace Game.StateMachines
{
    public partial class HostGameState : GameState
    {
        public override GameState OnEnterState()
        {
            return null;
        }

        public override GameState HandleInput(InputEvent @event)
        {
            return null;
        }

        public override GameState HandleKeyboardInput(InputEvent @event)
        {
            return null;
        }

        public override GameState Process(double deltaTime)
        {
            // TODO: Implement host game state
            GD.Print("HostGameState Process");
            return null;
        }

        public override GameState PhysicsProcess(double deltaTime)
        {
            // TODO: Implement host game state
            GD.Print("HostGameState PhysicsProcess");
            return null;
        }

        public override void OnExitState()
        {
            // TODO: Implement host game state
            GD.Print("HostGameState OnExitState");
        }
    }
}
