using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner<T> : Node3D where T : Node3D
{
    public bool m_IsActive = false;
    public bool m_IsOneShot = false;
    public uint m_MaxSpawnCount = 1;
    public float m_SpawnRatePerSecond = 1.0f;
    public Vector3 m_RelativeSpawnPosition;
    public Vector3 m_RelativeSpawnPositionOffset;

    private float m_TimeSinceLastSpawn = 0.0f;
    private string? m_TargetScenePath;
    private List<T> m_SpawnedObjects = new List<T>();
    private PackedScene? m_TargetScene;

    // NOTE: Providing a string path should actually be quite efficient as Godot's resource loader/manager is smart enough to cache the packed scene
    public Spawner(string targetScenePath, Vector3 relativeSpawnPosition = default(Vector3), Vector3 relativeSpawnPositionOffset = default(Vector3))
    {
        this.Name = "Spawner";
        m_TargetScenePath = targetScenePath;
        m_RelativeSpawnPosition = relativeSpawnPosition;
        m_RelativeSpawnPositionOffset = relativeSpawnPositionOffset;
    }

    public override void _Ready()
    {
        try
        {
            m_TargetScene = ResourceLoader.Load<PackedScene>(m_TargetScenePath);
            //GD.Print("Is " + m_TargetScenePath + " cached?: " + ResourceLoader.HasCached(m_TargetScenePath));
        }
        catch (Exception ex)
        {
            GD.PrintErr("Failed to load scene: " + m_TargetScenePath);
            GD.PrintErr(ex.Message);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // CHECK CONDITIONS
        if (!m_IsActive) { return; }
        if (m_TargetScene == null) { return; }
        if (m_IsOneShot && m_SpawnedObjects.Count > 0) { return; }

        // UPDATE TIMER
        m_TimeSinceLastSpawn += (float)delta;

        if (m_TimeSinceLastSpawn >= 1.0f / m_SpawnRatePerSecond && m_SpawnedObjects.Count < m_MaxSpawnCount)
        {
            Spawn(m_RelativeSpawnPosition);
        }
    }

    public T? Spawn(Vector3 relativeSpawnPosition = default(Vector3))
    {
        if (m_TargetScene == null)
        {
            GD.PrintErr("Failed to spawn object: Target object scene is null");
            return default(T);
        }

        // CALCULATE THE GLOBAL SPAWN TRANSFORM
        Vector3 globalSpawnPosition = this.GlobalTransform.Origin + relativeSpawnPosition * m_RelativeSpawnPositionOffset;

        // SPAWN OBJECT
        T? spawnedObject = GameManager.Spawn<T>(m_TargetScene, globalSpawnPosition);

        // CHECK IF SPAWNED
        if (spawnedObject == null)
        {
            GD.PrintErr("Spawner failed to spawn object");
            return default(T);
        }

        // ADD TO LIST
        m_SpawnedObjects.Add(spawnedObject);

        // RESET TIMER
        m_TimeSinceLastSpawn = 0.0f;

        return spawnedObject;
    }

    // public void Despawn(T spawnedObject)
    // {
    //     if (m_SpawnedObjects.Contains(spawnedObject))
    //     {
    //         m_SpawnedObjects.Remove(spawnedObject);
    //         spawnedObject.QueueFree();
    //     }
    // }
}
