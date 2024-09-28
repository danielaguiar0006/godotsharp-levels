/*
 * This class handles global settings and configurations that are relevant throughout the
 * entire game. It also provides methods to update these settings.
 */

using Godot;

public partial class Global : Node
{
    public static Global Instance { get; private set; }

    public static float m_Gravity { get; private set; }
    public static float m_DeltaTime { get; private set; }

    public static float m_GravityFactor { get; private set; } = 1.0f;
    public static float m_TimeFactor = 1.0f;

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

        m_Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle() * m_GravityFactor;
    }

    public override void _Process(double delta)
    {
        m_DeltaTime = (float)delta * m_TimeFactor;
    }

    // This also updates the m_Gravity value
    public void SetGravityFactor(float gravityFactor)
    {
        m_GravityFactor = gravityFactor;
        m_Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle() * m_GravityFactor;
        GD.Print("Gravity updated to: " + m_Gravity);
    }
}
