using Godot;

namespace Game.StateMachines
{
    public partial class SinglePlayerGameState : GameState
    {
        Player m_MainPlayer;
        Level m_Level;

        public override GameState OnEnterState()
        {
            // INIT SINGLE PLAYER GAME

            // TODO: Implement way to set RNG seed in the UI
            // SET RNG SEED HERE
            // Global.SetRNGSeed(0);
            // ELSE
            Global.s_RandomNumberGenerator.Randomize();

            // LOAD SCENES/PREFABS
            GameManager.SetLevelScene(ResourceLoader.Load<PackedScene>("res://scenes/level.tscn"));
            GameManager.SetPlayerScene(ResourceLoader.Load<PackedScene>("res://scenes/prefabs/player.tscn"));

            return null;
        }

        public override GameState StartGame()
        {
            // SPAWN PLAYER AND LEVEL
            // NOTE: Must first spawn the player, then the level after
            m_MainPlayer = GameManager.SpawnPlayer();
            m_Level = GameManager.SpawnLevel();

            // GENERATE LEVEL
            m_Level.GenerateLevel();

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

        public override GameState Process(float deltaTime)
        {
            return null;
        }

        public override GameState PhysicsProcess(float deltaTime)
        {
            return null;
        }

        public override void OnExitState()
        {
            return;
        }
    }
}
