using Godot;
using System;
using System.Collections.Generic;

public partial class ComponentManager : Node
{
    public List<ComponentBase> m_Components { get; private set; } = new List<ComponentBase>();

    private Mob m_Owner = null!;

    public override void _Ready()
    {
        m_Owner = (Mob)this.GetOwner();
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach (Node node in this.GetChildren())
        {
            if (node is ComponentBase component)
            {
                // UPDATE COMPONENTS LIST
                if (!m_Components.Contains(component))
                {
                    m_Components.Add(component);
                }

                // UPDATE THE ACTUAL COMPONENT
                component.Update(m_Owner, delta);
            }
        }
    }

    public T? GetComponent<T>() where T : ComponentBase
    {
        foreach (ComponentBase component in m_Components)
        {
            if (component is T)
            {
                return (T)component;
            }
        }

        return null;
    }

    public T? AddComponent<T>() where T : ComponentBase
    {
        T component;
        PackedScene packedScene;

        // LOAD COMPONENT PACKED SCENE
        switch (typeof(T))
        {
            case Type t when t == typeof(HealthRegenComponent):
                try { packedScene = ResourceLoader.Load<PackedScene>(HealthRegenComponent.s_ScenePath); }
                catch (Exception ex) { GD.PrintErr("Failed to load component scene: " + ex.Message); return null; }
                break;
            default:
                GD.PrintErr("Invalid component type");
                return null;
        }

        // INSTANTIATE COMPONENT
        try
        {
            component = packedScene.Instantiate<T>();
            this.AddChild(component);
            m_Components.Add(component);
            return component;
        }
        catch (Exception ex)
        {
            GD.PrintErr("Failed to instantiate component: " + ex.Message);
            return null;
        }
    }

    public void RemoveComponent<T>() where T : ComponentBase
    {
        if (m_Components.Count == 0)
        {
            GD.PrintErr("Failed to remove component: Components list is empty");
            return;
        }

        T? component = GetComponent<T>();
        if (component == null)
        {
            GD.PrintErr("Failed to remove component: Component type is not in the components list");
            return;
        }

        m_Components.Remove(component);
        component.QueueFree();
    }

    public void RemoveComponent(ComponentBase component)
    {
        if (component == null)
        {
            GD.PrintErr("Failed to remove component: Component is null");
            return;
        }
        if (m_Components.Count == 0)
        {
            GD.PrintErr("Failed to remove component: Components list is empty");
            return;
        }

        if (m_Components.Contains(component))
        {
            m_Components.Remove(component);
            component.QueueFree();
        }
        else
        {
            GD.PrintErr("Failed to remove component: Component is not in the components list");
        }
    }
}

// TODO: The components shouldnt be deleting themselves, the ComponentManager should be responsible for that
