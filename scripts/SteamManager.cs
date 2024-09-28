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
        const int ERROR_CODE = 1; // Exit code for errors > 0

        if (Instance != null)
        {
            GD.PrintErr("Multiple instances of SteamManager detected!");
            GetTree().Quit(ERROR_CODE);
        }

        GD.Print("SteamManager ready to initialize Steam");
        Instance = this;
    }

    public void InitializeSteam()
    {
        var initResult = Steam.SteamInit();
        if (initResult.Status != SteamInitStatus.SteamworksActive)
        {
            GD.PrintErr("[ERROR] Failed to initialize Steam, shutting down...: " + initResult.Verbal);
            GetTree().Quit(((int)initResult.Status));
            return;
        }

        GD.Print("Steam initialized successfully: " + initResult.Verbal);
    }
}
