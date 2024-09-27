using Godot;
using GodotSteam;

public partial class SteamDemo : Node
{
    private const uint AppId = 480;

    public override void _EnterTree()
    {
        OS.SetEnvironment("SteamAppId", AppId.ToString());
        OS.SetEnvironment("SteamGameId", AppId.ToString());
    }

    public override void _Ready()
    {
        Steam.SteamInit();

        var isSteamRunning = Steam.IsSteamRunning();
        if (!isSteamRunning)
        {
            GD.PrintErr("Steam is not running.");
            return;
        }

        var steamId = Steam.GetSteamID();
        var name = Steam.GetFriendPersonaName(steamId);

        GD.Print("Your Steam Name: " + name);
    }
}
