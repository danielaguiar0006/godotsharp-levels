/*
 * This class handles global settings and configurations that are relevant throughout the
 * entire game. It also provides methods to update these settings.
 */

using Godot;

public partial class Global : Node
{
    public static Global Instance { get; private set; }

    public static float s_Gravity { get; private set; }
    public static float s_DeltaTime { get; private set; }

    public static float s_GravityFactor { get; private set; } = 1.0f;
    public static float s_TimeFactor = 1.0f;
    public static RandomNumberGenerator s_RandomNumberGenerator { get; } = new RandomNumberGenerator();

    public override void _Ready()
    {
        if (Instance != null)
        {
            const int ERROR_CODE = 1; // Exit code for errors > 0
            GD.PrintErr("Multiple instances of Global detected!");
            GetTree().Quit(ERROR_CODE);
        }
        Instance = this;
        GD.Print("Global ready");

        s_Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle() * s_GravityFactor;
    }

    public override void _Process(double delta)
    {
        s_DeltaTime = (float)delta * s_TimeFactor;
    }

    // This also updates the m_Gravity value
    public static void SetGravityFactor(float gravityFactor)
    {
        s_GravityFactor = gravityFactor;
        s_Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle() * s_GravityFactor;
        GD.Print("Gravity updated to: " + s_Gravity);
    }

    public static void SetRNGSeed(ulong seed)
    {
        s_RandomNumberGenerator.Seed = seed;
    }
}
