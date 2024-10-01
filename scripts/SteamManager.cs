using Godot;
using GodotSteam;

public partial class SteamManager : Node
{
    public static SteamManager Instance { get; private set; }
    private const uint AppId = 480;

    public override void _EnterTree()
    {
        OS.SetEnvironment("SteamAppId", AppId.ToString());
        OS.SetEnvironment("SteamGameId", AppId.ToString());
    }

    public override void _Ready()
    {
        if (Instance != null)
        {
            const int ERROR_CODE = 1; // Exit code for errors > 0
            GD.PrintErr("Multiple instances of SteamManager detected!");
            GetTree().Quit(ERROR_CODE);
        }
        Instance = this;
        GD.Print("SteamManager ready to initialize Steam");
    }

    public override void _Process(double delta)
    {
        // ONLY RUN STEAM CALLBACKS IF ONLINE
        if (!GameManager.s_IsOnline) { return; }

        Steam.RunCallbacks();
    }

    public override void _ExitTree()
    {
        // SHUTDOWN STEAM
        GD.Print("Shutting down Steam...");
        Steam.SteamShutdown();
    }

    public static bool InitializeSteam()
    {
        var initResult = Steam.SteamInit();
        if (initResult.Status != SteamInitStatus.SteamworksActive)
        {
            GD.PrintErr("[ERROR] Failed to initialize Steam, shutting down...: " + initResult.Verbal);
            return false;
        }
        GD.Print("Steam initialized successfully: " + initResult.Verbal);
        return true;
    }
}
