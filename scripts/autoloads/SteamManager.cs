using Godot;
using GodotSteam;
using System;
using System.IO;

public partial class SteamManager : Node
{
    public static SteamManager Instance { get; private set; }
    private static uint AppId;

    public override void _EnterTree()
    {
        ReadAppIdFromFile();
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
        GD.Print("AppId: " + AppId);
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

    private void ReadAppIdFromFile()
    {
        try
        {
            string filePath = "steam_appid.txt";
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                if (uint.TryParse(fileContent, out uint appId))
                {
                    AppId = appId;
                }
                else
                {
                    GD.PrintErr("Failed to parse AppId from steam_appid.txt");
                }
            }
            else
            {
                GD.PrintErr("steam_appid.txt file not found");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr("Error reading steam_appid.txt: " + ex.Message);
        }
    }
}
