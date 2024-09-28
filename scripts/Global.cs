using Godot;
using GodotSteam;

public partial class Global : Node
{
    private const uint AppId = 480;

    public override void _EnterTree()
    {
    }

    public override void _Ready()
    {
        SteamManager.Instance.InitializeSteam();
    }

    public override void _Process(double delta)
    {
        Steam.RunCallbacks();
    }

    public override void _ExitTree()
    {
        Steam.SteamShutdown();
    }

}
