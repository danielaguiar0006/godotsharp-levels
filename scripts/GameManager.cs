/*
 * This class is responsible for managing the game state and handling game-specific logic.
 * Such logic includes managing game states (e.g., start, pause, end), handling level transitions,
 * managing player data and game progress, etc...
 * It interacts with the Global class to apply global settings when necessary.
 */

using Godot;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    public static bool s_IsOnline { get; private set; } = false;
    public static ushort s_depthLevel { get; private set; } = 0;

    private static Node s_MainNode;
    private static Level s_Level;
    private static PackedScene s_LevelScene;
    private static Control s_MainMenuUI;

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

        // THE MAIN MENU UI IS IN CHARGE OF CALLING STARTGAME()
        // (OR ITS VARIATIONS)
    }

    public override void _PhysicsProcess(double delta)
    {
        //if (m_IsOnline) { NetworkManager.ServerUpdate(delta); }
    }

    public override void _ExitTree()
    {
    }

    public static void StartSinglePlayerGame()
    {
        // TODO: Implement way to set RNG seed in the UI
        // SET RNG SEED HERE
        // Global.SetRandomSeed(0);
        // ELSE
        Global.s_RandomNumberGenerator.Randomize();

        s_LevelScene = ResourceLoader.Load<PackedScene>("res://scenes/level.tscn");
        s_Level = s_LevelScene.Instantiate<Level>();
        s_MainNode.AddChild(s_Level);
    }

    // TODO: public static void QuitToMainMenu()

    public static void QuitApplication()
    {
        s_MainNode.GetTree().Quit();
    }
}
