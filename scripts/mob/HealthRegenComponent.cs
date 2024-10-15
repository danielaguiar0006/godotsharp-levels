using Godot;
using Game.StatsManager;

public partial class HealthRegenComponent : ComponentBase
{
    [Export]
    public override bool m_IsActive { get; set; } = true;
    [Export]
    public override float m_EffectStrength { get; set; } = 5.0f; // Health Regen Amount Per Second
    [Export]
    public override string m_ComponentDescription { get; set; } = "Health Regen";
    [Export]
    public override double m_EffectDurationSeconds { get; set; } = 15.0f;
    [Export]
    public override double m_EffectFrequencySeconds { get; set; } = 1.0f;

    // TODO: Find a way to enforce for every Compononent to have its own static string path
    public static string s_ScenePath = "res://scenes/prefabs/components/health_regen_component.tscn";

    public override void Update(Mob owner, double delta)
    {
        m_TotalElapsedTimeSeconds += delta;
        m_TimeSinceEffectLastAppliedSeconds += delta;

        if (!m_IsActive) { return; }
        if (m_EffectCount <= 0)
        {
            InitialEffect(owner);
            return;
        }
        if (m_TotalElapsedTimeSeconds >= m_EffectDurationSeconds)
        {
            FinishEffect(owner);
            return;
        }

        if (m_TimeSinceEffectLastAppliedSeconds >= m_EffectFrequencySeconds)
        {
            owner.m_MobStats.m_BaseStatTypeToCurrentValue[BaseStatType.Health] += m_EffectStrength;
            m_TimeSinceEffectLastAppliedSeconds = 0.0f;
            m_EffectCount++;
        }
    }

    private void InitialEffect(Mob owner)
    {
        GD.Print("HealthRegenComponent InitialEffect");

        owner.m_MobStats.m_BaseStatTypeToCurrentValue[BaseStatType.Health] += m_EffectStrength;
        m_EffectCount = 1;
        m_TimeSinceEffectLastAppliedSeconds = 0.0f;
    }

    private void FinishEffect(Mob owner)
    {
        GD.Print("HealthRegenComponent FinishEffect");

        // TODO: 
        // owner.m_ComponentManager.RemoveChild(this);
        // this.QueueFree();
    }
}
