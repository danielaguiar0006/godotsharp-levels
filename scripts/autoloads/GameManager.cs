/*
 * This class is responsible for managing the game state and handling game-specific logic.
 * Such logic includes managing game states (e.g., start, pause, end), handling level transitions,
 * managing player data and game progress, etc...
 * It interacts with the Global class to apply global settings when necessary.
 */

using Godot;
using System.Collections.Generic;
using Game.StateMachines;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    public static GameState s_CurrentGameState { get; private set; } = null;
    public static bool s_GameActive { get; private set; } = false;
    public static bool s_IsOnline { get; private set; } = false;
    public static ushort s_depthLevel { get; private set; } = 0;
    public static List<Player> s_Players { get; private set; } = new List<Player>();
    public static PackedScene s_PlayerScene { get; private set; }
    public static PackedScene s_LevelScene { get; private set; }

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
        GD.Print("GameManager Ready");

        // GET MAIN NODE
        s_MainNode = GetTree().CurrentScene;

        // TRY TO INITIALIZE STEAM
        s_IsOnline = SteamManager.InitializeSteam();

        // LOAD MAIN MENU UI
        PackedScene mainMenuUIScene = ResourceLoader.Load<PackedScene>("res://scenes/main_menu_ui.tscn");
        s_MainMenuUI = mainMenuUIScene.Instantiate<Control>();
        s_MainNode.AddChild(s_MainMenuUI);

        // THE MAIN MENU UI IS IN CHARGE OF SETTING THE INITIAL GameState + CALLING StartGame()
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!s_GameActive) { return; }

        // NOTE: newState is null if the state does not change, otherwise it is the new state
        GameState newState = s_CurrentGameState.HandleInput(@event);
        TransitionToState(newState);
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (!s_GameActive) { return; }

        // NOTE: newState is null if the state does not change, otherwise it is the new state
        GameState newState = s_CurrentGameState.HandleKeyboardInput(@event);
        TransitionToState(newState);
    }

    public override void _Process(double delta)
    {
        if (!s_GameActive) { return; }

        // Process the current game state
        GameState newState = s_CurrentGameState.Process(delta);
        TransitionToState(newState);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!s_GameActive) { return; }

        // Process the current game state
        GameState newState = s_CurrentGameState.PhysicsProcess(delta);
        TransitionToState(newState);
    }

    public static void StartGame()
    {
        GD.Print("Starting Game");

        GameState newState = s_CurrentGameState.StartGame();
        TransitionToState(newState);
        s_GameActive = true;
    }

    public static void QuitApplication()
    {
        s_MainNode.GetTree().Quit();
    }

    public static Player SpawnPlayer()
    {
        Player player = s_PlayerScene.Instantiate<Player>();
        s_Players.Add(player);
        s_MainNode.AddChild(player);
        return player;
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

    // Change the game's state
    public static void TransitionToState(GameState newState)
    {
        if (newState != null)
        {
            if (s_CurrentGameState != null) { s_CurrentGameState.OnExitState(); } // Exit current state 
            s_CurrentGameState = newState;                                        // Set the new state
            GameState nextState = s_CurrentGameState.OnEnterState();              // Enter the new state

            // If the OnEnterState of the new state returns another state, transition again
            if (nextState != null)
            {
                TransitionToState(nextState);
            }
        }
    }

    public static void SetPlayerScene(PackedScene playerScene) { s_PlayerScene = playerScene; }
    public static void SetLevelScene(PackedScene levelScene) { s_LevelScene = levelScene; }

    // TODO: public static void QuitToMainMenu()
}
