public interface IComponent
{
    public bool m_IsActive { get; set; }
    public float m_EffectStrength { get; set; }
    public string m_ComponentDescription { get; set; }
    public double m_EffectDurationSeconds { get; set; }
    public double m_EffectFrequencySeconds { get; set; }

    // TODO: Add way to check when to remove
    // public bool m_IsDone { get; }
    public double m_TotalElapsedTimeSeconds { get; }
    public double m_TimeSinceEffectLastAppliedSeconds { get; }
    public uint m_EffectCount { get; } // How many times the effect has been applied

    public void Update(Mob owner, double delta);
}
