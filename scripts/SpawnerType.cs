using Godot;
using System;
using System.Collections.Generic;

public partial class SpawnerType<T> : Node3D where T : Node3D
{
    private Node3D? _m_TargetSceneObject;
    [Export]
    public Node3D? m_TargetSceneObject
    {
        get => _m_TargetSceneObject;
        set
        {
            if (_m_TargetSceneObject != value)
            {
                _m_TargetSceneObject = value;
                OnTargetSceneObjectChanged();
            }
        }
    }
    private string? _m_TargetScenePath;
    [Export]
    public string? m_TargetScenePath
    {
        get => _m_TargetScenePath;
        set
        {
            if (_m_TargetScenePath != value)
            {
                _m_TargetScenePath = value;
                OnTargetScenePathChanged();
            }
        }
    }
    [Export]
    public bool m_StartOnGameActive = false;
    [Export]
    public bool m_IsActive = false;
    [Export]
    public bool m_IsOneShot = false;
    [Export]
    public uint m_MaxSpawnCount = 1;
    [Export]
    public float m_SpawnRatePerSecond = 1.0f; // 0.0f <= Instant
    [Export]
    public Vector3 m_RelativeSpawnPosition;
    [Export]
    public Vector3 m_RelativeSpawnPositionOffset;

    private bool m_IsGameActive = false;
    private float m_TimeSinceLastSpawn = 0.0f;
    private List<T> m_SpawnedObjects = new List<T>();
    private PackedScene? m_TargetPackedScene;

    // DO THIS WHEN YOU WANT TO START WHITH AN EMPTY SPAWNER LOOKING TO BE DYNAMICALLY UPDATED WITH A NEW TARGET SCENE
    public SpawnerType() { }

    // DO THIS WHEN YOU WANT TO START WITH A TARGET SCENE
    public SpawnerType(Node3D targetScene, Vector3 relativeSpawnPosition = default(Vector3), Vector3 relativeSpawnPositionOffset = default(Vector3))
    {
        m_TargetSceneObject = targetScene;
        m_RelativeSpawnPosition = relativeSpawnPosition;
        m_RelativeSpawnPositionOffset = relativeSpawnPositionOffset;
    }

    // DO THIS WHEN YOU WANT TO START WITH A TARGET SCENE PATH
    // NOTE: Providing a string path should actually be quite efficient as Godot's resource loader/manager is smart enough to cache the packed scene
    public SpawnerType(string targetScenePath, Vector3 relativeSpawnPosition = default(Vector3), Vector3 relativeSpawnPositionOffset = default(Vector3))
    {
        m_TargetScenePath = targetScenePath;
        m_RelativeSpawnPosition = relativeSpawnPosition;
        m_RelativeSpawnPositionOffset = relativeSpawnPositionOffset;
    }

    public override void _Ready()
    {
        this.Name = "Spawner";

        // NO NEED TO LOAD ANYTHING - SCENE IS ALREADY LOADED IN THE EDITOR (OR IN CODE) 
        // NOTE: This takes precedence over everything else
        if (m_TargetSceneObject != null) { return; }

        // FOR WHEN UPDATING THE TARGET SCENE PATH DYNAMICALLY
        if (m_TargetScenePath == null && m_TargetSceneObject == null) { return; }

        // LOAD TARGET SCENE
        LoadTargetScene();
    }


    public override void _PhysicsProcess(double delta)
    {
        // CHECK CONDITIONS
        if (GameManager.s_IsGameActive && m_StartOnGameActive)
        {
            m_StartOnGameActive = false;
            m_IsActive = true;
            return;
        }
        else if (!m_IsActive) { return; }
        if (m_TargetPackedScene == null) { return; }
        if (m_IsOneShot && m_SpawnedObjects.Count > 0) { return; }

        // UPDATE TIMER
        m_TimeSinceLastSpawn += (float)delta;

        // INSTANT SPAWN
        if (m_SpawnRatePerSecond <= 0.0f && m_SpawnedObjects.Count < m_MaxSpawnCount)
        {
            Spawn(m_RelativeSpawnPosition);
        }
        // TIMED SPAWN
        else if (m_TimeSinceLastSpawn >= 1.0f / m_SpawnRatePerSecond && m_SpawnedObjects.Count < m_MaxSpawnCount)
        {
            Spawn(m_RelativeSpawnPosition);
        }
    }

    public T? Spawn(Vector3 relativeSpawnPosition = default(Vector3))
    {
        // if (m_TargetPackedScene == null)
        // {
        //     GD.PrintErr("Failed to spawn object: Target object scene is null");
        //     return default(T);
        // }

        T? spawnedObject = default(T);

        // CALCULATE THE GLOBAL SPAWN TRANSFORM
        Vector3 globalSpawnPosition = this.GlobalTransform.Origin + relativeSpawnPosition * m_RelativeSpawnPositionOffset;

        if (m_TargetPackedScene != null)
        {
            // SPAWN OBJECT
            spawnedObject = GameManager.Spawn<T>(m_TargetPackedScene, globalSpawnPosition);
        }
        else if (m_TargetSceneObject != null)
        {
            // SPAWN OBJECT
            spawnedObject = GameManager.Spawn<T>(m_TargetSceneObject, globalSpawnPosition);
        }

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

    private void OnTargetSceneObjectChanged()
    {
        m_TargetScenePath = null;
        m_TargetPackedScene = null;
    }

    private void OnTargetScenePathChanged()
    {
        m_TargetSceneObject = null;
        LoadTargetScene();
    }

    private void LoadTargetScene()
    {
        try
        {
            m_TargetPackedScene = ResourceLoader.Load<PackedScene>(m_TargetScenePath);
            //GD.Print("Is " + m_TargetScenePath + " cached?: " + ResourceLoader.HasCached(m_TargetScenePath));
        }
        catch (Exception ex)
        {
            GD.PrintErr("Failed to load scene: " + m_TargetScenePath);
            GD.PrintErr(ex.Message);
        }
    }

    // TODO:
    // public void Despawn(T spawnedObject)
    // {
    //     if (m_SpawnedObjects.Contains(spawnedObject))
    //     {
    //         m_SpawnedObjects.Remove(spawnedObject);
    //         spawnedObject.QueueFree();
    //     }
    // }
    //
}
