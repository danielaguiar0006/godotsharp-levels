/*
 * This class is responsible for managing the game state and handling game-specific logic.
 * Such logic includes managing game states (e.g., start, pause, end), handling level transitions,
 * managing player data and game progress, etc...
 * It interacts with the Global class to apply global settings when necessary.
 */

using Godot;
using System.Collections.Generic;
using System;
using Game.StateMachines;
using Game.Networking;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    public static bool s_ForceOffline { get; private set; } = false; // TODO: REMOVE FOR RELEASE

    public static StateMachine<GameManager> s_StateMachine { get; private set; }
    public static bool s_IsGameActive { get; private set; } = false;
    public static bool s_IsOnline { get; private set; } = false;
    public static ushort s_depthLevel { get; private set; } = 0;
    public static PackedScene s_PlayerScene { get; private set; } = null!; // Initialized in _Ready()
    public static PackedScene s_LevelScene { get; private set; }
    public static List<Player> s_Players { get; private set; } = new List<Player>(4);

    private static Node s_MainNode;
    private static Control s_MainMenuUI;
    private static Level s_Level;

    public override void _Ready()
    {
        if (Instance != null)
        {
            const int ERROR_CODE = 1; // Exit code for errors > 0
            GD.PrintErr("Multiple instances of GameManager detected!");
            GetTree().Quit(ERROR_CODE);
        }
        Instance = this;
        GD.Print("GameManager ready");

        // INIT STATE MACHINE
        s_StateMachine = new StateMachine<GameManager>(this);

        // GET MAIN NODE
        s_MainNode = GetTree().CurrentScene;

        // TRY TO INITIALIZE STEAM
        if (!s_ForceOffline) { s_IsOnline = SteamManager.InitializeSteam(); }

        // LOAD PLAYER SCENE
        s_PlayerScene = ResourceLoader.Load<PackedScene>("res://scenes/prefabs/player.tscn");

        // LOAD MAIN MENU UI
        PackedScene mainMenuUIScene = ResourceLoader.Load<PackedScene>("res://scenes/main_menu_ui.tscn");
        s_MainMenuUI = mainMenuUIScene.Instantiate<Control>();
        s_MainNode.AddChild(s_MainMenuUI);

        // THE MAIN MENU UI IS IN CHARGE OF SETTING THE INITIAL GameState + CALLING StartGame()
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!s_IsGameActive) { return; }

        s_StateMachine.HandleInput(@event);
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (!s_IsGameActive) { return; }

        s_StateMachine.HandleKeyboardInput(@event);
    }

    public override void _Process(double delta)
    {
        if (!s_IsGameActive) { return; }

        s_StateMachine.Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!s_IsGameActive) { return; }

        s_StateMachine.PhysicsProcess(delta);
    }

    public static void StartGame()
    {
        GD.Print("Starting Game");

        s_StateMachine.m_CurrentState.StartGame(s_StateMachine.m_Owner);
        s_IsGameActive = true;
    }

    public static void QuitApplication()
    {
        s_MainNode.GetTree().Quit();
    }

    // public static Player SpawnPlayer()
    // {
    //     Player player = s_PlayerScene.Instantiate<Player>();
    //     s_Players.Add(player);
    //     s_MainNode.AddChild(player);
    //     return player;
    // }

    public static T? Spawn<T>(string targetScenePath, Vector3 globalSpawnPosition = default(Vector3)) where T : Node3D
    {
        try
        {
            PackedScene objectScene = ResourceLoader.Load<PackedScene>(targetScenePath);
            return Spawn<T>(objectScene, globalSpawnPosition);
        }
        catch (Exception ex)
        {
            GD.PrintErr("Failed to spawn object, failed to load scene: " + targetScenePath);
            GD.PrintErr(ex.Message);
            return default(T);
        }
    }

    public static T? Spawn<T>(PackedScene targetScene, Vector3 globalSpawnPosition = default(Vector3)) where T : Node3D
    {
        if (targetScene == null)
        {
            GD.PrintErr("Failed to spawn object: Target object scene is null");
            return default(T);
        }

        try
        {
            // SPAWN OBJECT
            T spawnedObject = targetScene.Instantiate<T>();

            // ADD TO LIST IF APPLICABLE
            if (spawnedObject is Player player)
            {
                GD.Print("Spawned object is a Player, adding to GameManager player list.");
                s_Players.Add(player);

                switch (s_Players.Count)
                {
                    case 1:
                        GD.Print("Spawned Player 1");
                        // FIXME: Why is there two names?
                        player.Name = "PlayerOne";
                        player.m_Name = "PlayerOne";
                        break;
                    case 2:
                        GD.Print("Spawned Player 2");
                        player.Name = "PlayerTwo";
                        player.m_Name = "PlayerTwo";
                        break;
                    case 3:
                        GD.Print("Spawned Player 3");
                        player.Name = "PlayerThree";
                        player.m_Name = "PlayerThree";
                        break;
                    case 4:
                        GD.Print("Spawned Player 4");
                        player.Name = "PlayerFour";
                        player.m_Name = "PlayerFour";
                        break;
                    default:
                        GD.Print("Spawned Player " + s_Players.Count);
                        // FIXME: DESPAWN PLAYER AS THE MAX IS 4
                        break;
                }
            }

            // SET SPAWN POSITION/TRANSFORM
            spawnedObject.GlobalTransform = new Transform3D(Basis.Identity, globalSpawnPosition);

            // ADD TO SCENE
            s_MainNode.AddChild(spawnedObject);

            return spawnedObject;
        }
        catch (Exception ex)
        {
            GD.PrintErr("Failed to spawn object: Failed to instance target object scene");
            GD.PrintErr(ex.Message);
            return default(T);
        }
    }

    public static void DespawnPlayer(Player player)
    {
        if (!s_Players.Contains(player))
        {
            GD.PrintErr("[ERROR] Unable to despawn player, player not found");
            return;
        }
        s_Players.Remove(player);
        s_MainNode.RemoveChild(player);
        player.QueueFree();
    }

    public static Level SpawnLevel()
    {
        if (s_Level != null)
        {
            GD.PrintErr("[ERROR] Unable to spawn level, level already exists");
            return null;
        }
        s_Level = s_LevelScene.Instantiate<Level>(); // INSTANTIATE LEVEL
        s_MainNode.AddChild(s_Level);                // ADD LEVEL TO THE SCENE
        return s_Level;
    }

    public static void DespawnLevel()
    {
        if (s_Level == null)
        {
            GD.PrintErr("[ERROR] Unable to despawn level, level does not exist");
            return;
        }
        s_MainNode.RemoveChild(s_Level);
        s_Level.QueueFree();
    }

    public static void SetPlayerScene(PackedScene playerScene) { s_PlayerScene = playerScene; }
    public static void SetLevelScene(PackedScene levelScene) { s_LevelScene = levelScene; }

    // TODO: public static void QuitToMainMenu()
}
