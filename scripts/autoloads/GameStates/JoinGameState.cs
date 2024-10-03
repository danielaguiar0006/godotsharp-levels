using Godot;

namespace Game.StateMachines
{
    public partial class JoinGameState : GameState
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
            // TODO: Implement join game state
            GD.Print("JoinGameState Process");
            return null;
        }

        public override GameState PhysicsProcess(double deltaTime)
        {
            // TODO: Implement join game state
            GD.Print("JoinGameState PhysicsProcess");
            return null;
        }

        public override void OnExitState()
        {
            // TODO: Implement join game state
            GD.Print("JoinGameState OnExitState");
        }
    }
}
