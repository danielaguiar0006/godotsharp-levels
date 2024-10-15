using Godot;

public abstract partial class ComponentBase : Node, IComponent
{
    public abstract bool m_IsActive { get; set; }
    public abstract float m_EffectStrength { get; set; }
    public abstract string m_ComponentDescription { get; set; }
    public abstract double m_EffectDurationSeconds { get; set; }
    public abstract double m_EffectFrequencySeconds { get; set; }

    [Export]
    public double m_TotalElapsedTimeSeconds { get; protected set; }
    public double m_TimeSinceEffectLastAppliedSeconds { get; protected set; }
    public uint m_EffectCount { get; protected set; }

    public ComponentBase()
    {
        m_TotalElapsedTimeSeconds = 0.0f;
        m_TimeSinceEffectLastAppliedSeconds = 0.0f;
        m_EffectCount = 0;
    }

    public abstract void Update(Mob owner, double delta);
}
