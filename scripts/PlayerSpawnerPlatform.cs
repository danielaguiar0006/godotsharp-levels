using Godot;

public partial class PlayerSpawnerPlatform : Node3D
{
    Spawner<Player> m_Spawner = new Spawner<Player>("res://scenes/prefabs/player.tscn");

    public override void _EnterTree()
    {
        m_Spawner.m_IsActive = false;
        m_Spawner.m_IsOneShot = true;
        m_Spawner.m_MaxSpawnCount = 1;
        m_Spawner.m_SpawnRatePerSecond = 0.0f; // <= 0 Is instant
        m_Spawner.m_RelativeSpawnPosition = new Vector3(0, 0, 0);
        m_Spawner.m_RelativeSpawnPositionOffset = new Vector3(0, 0.005f, 0);

        // Add the spawner node to the scene tree
        AddChild(m_Spawner);
    }

    public override void _Process(double delta)
    {
        if (!m_Spawner.m_IsActive && GameManager.s_IsGameActive)
        {
            m_Spawner.m_IsActive = true;
        }
    }
}
