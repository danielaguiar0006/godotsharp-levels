using Godot;
using System.Diagnostics;

namespace Game.StateMachines
{
    public partial class SinglePlayerGameState : State<GameManager>
    {
        Player m_MainPlayer;
        Level m_Level;

        public override State<GameManager>? OnEnterState(GameManager gameManager)
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

        public override void StartGame(GameManager gameManager)
        {
            // SPAWN PLAYER AND LEVEL
            // NOTE: Must first spawn the player, then the level after
            //m_MainPlayer = GameManager.SpawnPlayer();
            //m_MainPlayer = GameManager.Spawn<Player>(GameManager.s_PlayerScene)!;
            //Debug.Assert(m_MainPlayer != null, "Failed to spawn Main player");
            //m_Level = GameManager.SpawnLevel();

            // GENERATE LEVEL
            //m_Level.GenerateLevel();
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
            return null;
        }

        public override State<GameManager>? PhysicsProcess(GameManager gameManager, double deltaTime)
        {
            return null;
        }

        public override void OnExitState(GameManager gameManager)
        {
            return;
        }
    }
}
