using Godot;

namespace Game.StateMachines
{
    public partial class JoinGameState : State<GameManager>
    {
        public override State<GameManager>? OnEnterState(GameManager gameManager)
        {
            return null;
        }

        public override State<GameManager>? HandleInput(GameManager gameManager, InputEvent @event)
        {
            return null;
        }

        public override State<GameManager>? HandleKeyboardInput(GameManager gameManager, InputEvent @event)
        {
            return null;
        }

        public override State<GameManager>? Process(GameManager gameManager, double deltaTime)
        {
            // TODO: Implement join game state
            GD.Print("JoinGameState Process");
            return null;
        }

        public override State<GameManager>? PhysicsProcess(GameManager gameManager, double deltaTime)
        {
            // TODO: Implement join game state
            GD.Print("JoinGameState PhysicsProcess");
            return null;
        }

        public override void OnExitState(GameManager gameManager)
        {
            // TODO: Implement join game state
            GD.Print("JoinGameState OnExitState");
        }
    }
}
