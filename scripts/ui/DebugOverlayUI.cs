// NOTE: The ImGui addon breaks aiming when ui is directly in the middle of the screen
// Probably because when the mouse is captured, its probably still in the middle of the
// screen, just not visible. Unlocking the mouse and adjusting the ui transform to be
// out of the way fixes this issue.
// if (@event is InputEventKey keyEvent && keyEvent.Pressed)

using Godot;
using static InputActions;
using Game.StatsManager;
using ImGuiNET;
using System.Collections.Generic;

// The Debug Overlay Shows debug information related to the player's state, stats, and other information
public partial class DebugOverlayUI : Node
{
    [Export]
    public bool m_DisplayDebugInfo { get; private set; } = false;

    [Export]
    protected Label m_CurrentStateDebugLabel;
    [Export]
    protected Label m_IsOnFloorDebugLabel;
    [Export]
    protected Label m_StatsDebugLabel;

    private Player m_Owner;

    public override void _Ready()
    {
        m_Owner = GetParent<Player>();
    }

    public override void _Input(InputEvent @event)
    {
        // TOGGLE DEBUG INFO
        if (Input.IsActionJustPressed(s_ToggleDebugMenu))
        {
            ToggleDebugMenu();
        }
    }

    public override void _Process(double delta)
    {
        if (!Global.s_IsDebugMode) { return; }
        if (!m_DisplayDebugInfo) { return; }

        DisplayPlayerMenu();

        // // Information about the current state on the screen
        // m_CurrentStateDebugLabel.Text = "Current State: " + m_Owner.m_CurrentPlayerState.GetType().Name;
        //
        // UpdateFloorDebugLabel();
        // m_StatsDebugLabel.Text = GetPlayerStatsInfo();

        // Show the player's current velocity
        // TODO: this.GetNode<Label>("VelocityDebugInfo").Text = "Velocity: " + m_CurrentVelocity;
        // Show the player's current movement speed factor
        // TODO: this.GetNode<Label>("MovementSpeedFactorDebugInfo").Text = "Movement Speed Factor: " + m_CurrentMovementSpeedFactor;
    }

    private void DisplayPlayerMenu()
    {
        bool isActive = true;

        ImGui.Begin("Player Debug Menu", ref isActive);

        if (m_Owner.m_ComponentManager != null)
        {
            // FIXME: This is all really janky - I Cant remove a component from a list while traversing it or ImGUI freaks out
            if (ImGui.Button("Add Health Regen Component"))
            {
                m_Owner.m_ComponentManager.AddComponent<HealthRegenComponent>();
            }

            if (ImGui.Button("Remove Health Regen Component"))
            {
                m_Owner.m_ComponentManager.RemoveComponent<HealthRegenComponent>();
            }

            List<ComponentBase> m_ComponentsToRemove = new List<ComponentBase>(m_Owner.m_ComponentManager.m_Components.Count);

            foreach (ComponentBase component in m_Owner.m_ComponentManager.m_Components)
            {
                if (ImGui.Button($"Remove {component.GetType().Name} Component"))
                {
                    m_ComponentsToRemove.Add(component);
                }
            }
            foreach (ComponentBase component in m_ComponentsToRemove)
            {
                m_Owner.m_ComponentManager.RemoveComponent(component);
            }
        }
        else
        {
            ImGui.Text("Missing Options due to missing ComponentManager node");
        }

        if (ImGui.BeginTable("General", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
        {
            // PLAYER NAME
            string name = m_Owner.m_Name;
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            ImGui.Text("Name:");
            ImGui.TableSetColumnIndex(1);
            ImGui.InputText("##Name", ref name, 100);
            m_Owner.m_Name = name;

            // PLAYER STATE
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            ImGui.Text("State:");
            ImGui.TableSetColumnIndex(1);
            ImGui.Text(m_Owner.m_StateMachine.m_CurrentState.GetType().Name);

            ImGui.EndTable();
        }

        // PLAYER STATS
        if (ImGui.CollapsingHeader("Stats"))
        {
            if (ImGui.BeginTable("LevelEffectFactorTable", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
            {
                float levelEffectFactor = m_Owner.m_MobStats.m_LevelEffectFactor;
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text("Level Effect Factor:");
                ImGui.TableSetColumnIndex(1);
                ImGui.InputFloat("##LevelEffectFactor", ref levelEffectFactor);
                ImGui.EndTable();
                m_Owner.m_MobStats.m_LevelEffectFactor = levelEffectFactor;
            }

            if (ImGui.CollapsingHeader("Base Stats Values"))
            {
                if (ImGui.BeginTable("BaseStatsTable", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    foreach (var stat in m_Owner.m_MobStats.m_BaseStatTypeToCurrentValue)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text($"{stat.Key}:");
                        ImGui.TableSetColumnIndex(1);
                        float currentValue = stat.Value;
                        ImGui.InputFloat($"##{stat.Key}", ref currentValue);
                        m_Owner.m_MobStats.m_BaseStatTypeToCurrentValue[stat.Key] = currentValue;
                    }
                    ImGui.EndTable();
                }
            }

            if (ImGui.CollapsingHeader("Attribute Stat Levels"))
            {
                if (ImGui.BeginTable("AttributeStatsTable", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    foreach (var stat in m_Owner.m_MobStats.m_AttributeTypeToCurrentLevel)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text($"{stat.Key}:");
                        ImGui.TableSetColumnIndex(1);
                        int currentLevel = stat.Value;
                        ImGui.InputInt($"##{stat.Key}", ref currentLevel);
                        m_Owner.m_MobStats.m_AttributeTypeToCurrentLevel[stat.Key] = currentLevel;
                    }
                    ImGui.EndTable();
                }
            }

            if (ImGui.CollapsingHeader("Special Stat Factors"))
            {
                if (ImGui.BeginTable("SpecialStatsTable", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    foreach (var stat in m_Owner.m_MobStats.m_SpecialStatTypeToAmountFactor)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text($"{stat.Key}:");
                        ImGui.TableSetColumnIndex(1);
                        float amountFactor = stat.Value;
                        ImGui.InputFloat($"##{stat.Key}", ref amountFactor);
                        m_Owner.m_MobStats.m_SpecialStatTypeToAmountFactor[stat.Key] = amountFactor;
                    }
                    ImGui.EndTable();
                }
            }

            if (ImGui.CollapsingHeader("Damage Resistance Factors"))
            {
                if (ImGui.BeginTable("DamageResistanceTable", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    foreach (var stat in m_Owner.m_MobStats.m_DamageTypeToResistanceAmountFactor)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text($"{stat.Key}:");
                        ImGui.TableSetColumnIndex(1);
                        float resistanceFactor = stat.Value;
                        ImGui.InputFloat($"##{stat.Key}", ref resistanceFactor);
                        m_Owner.m_MobStats.m_DamageTypeToResistanceAmountFactor[stat.Key] = resistanceFactor;
                    }
                    ImGui.EndTable();
                }
            }
        }

        ImGui.End();

        // Update menu visibility if it has changed
        if (isActive != m_DisplayDebugInfo)
        {
            ToggleDebugMenu();
        }
    }

    private void ToggleDebugMenu()
    {
        m_DisplayDebugInfo = !m_DisplayDebugInfo;

        // LOCK/UNLOCK MOUSE
        if (m_DisplayDebugInfo)
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
    }

    // TODO: Continue fleshing out ui
    // private void UpdateFloorDebugLabel()
    // {
    //     if (m_Owner.IsOnFloor())
    //     {
    //         m_IsOnFloorDebugLabel.AddThemeColorOverride("font_color", new Color(0, 1, 0));
    //         m_IsOnFloorDebugLabel.Text = "On Floor: True";
    //     }
    //     else
    //     {
    //         m_IsOnFloorDebugLabel.AddThemeColorOverride("font_color", new Color(1, 0, 0));
    //         m_IsOnFloorDebugLabel.Text = "On Floor: False";
    //     }
    // }
    //
}
