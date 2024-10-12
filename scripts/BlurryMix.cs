using Godot;
using Game.StatsManager;
using System;

public partial class BlurryMix : WorldEnvironment
{
    Player m_MainPlayer;
    private float m_CurrentBlurAmount = 0.0f; // 1.0f = Max Blur 0.0f = No Blur

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //m_MainPlayer = GameManager.s_Players[0];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (m_MainPlayer == null && GameManager.s_Players.Count > 0)
        {
            try
            {
                m_MainPlayer = GameManager.s_Players[0];
            }
            catch (Exception e)
            {
                GD.PrintErr("Error accessing main player: " + e.Message);
            }
        }

        if (m_MainPlayer == null)
        {
            return;
        }

        float maxHealth = m_MainPlayer.m_MobStats.m_BaseStatTypeToMaxValue[BaseStatType.Health];
        float currentHealth = m_MainPlayer.m_MobStats.m_BaseStatTypeToCurrentValue[BaseStatType.Health];

        float healthPercentage = currentHealth / maxHealth;
        float blurAmount = 1.0f - healthPercentage;

        if (blurAmount != m_CurrentBlurAmount)
        {
            // Apply blur to the world environment
            //Environment.Sky.SkyMaterial.Set("blur_amount", m_BlurAmount);
            this.Environment.GlowMix = blurAmount;
            m_CurrentBlurAmount = blurAmount;
        }

    }
}
