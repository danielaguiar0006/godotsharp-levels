using Godot;
using static InputActions;
using Game.StatsManager;

namespace Game.StateMachines;

public partial class SprintState<T> : IPlayerState<T> where T : Player
{
    public IPlayerState<T>? OnEnterState(T playerOwner)
    {
        return null;
    }

    public IPlayerState<T>? HandleInput(T playerOwner, InputEvent @event)
    {
        // Checking mouse button events
        if (@event is InputEventMouseButton mouseButtonEvent && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            // Transition to the attack state if the attack button is pressed
            if (mouseButtonEvent.IsActionPressed(s_AttackLight))
            {
                return new AttackLightState<T>();
            }
        }
        if (Input.IsActionJustPressed(s_MoveJump) && playerOwner.IsOnFloor())
        {
            return new JumpState<T>();
        }

        if (Input.IsActionJustPressed(s_MoveDodge))
        {
            return new DodgeState<T>();
        }

        return null;
    }

    public IPlayerState<T>? HandleKeyboardInput(T playerOwner, InputEvent @event)
    {
        return null;
    }

    public IPlayerState<T>? Process(T playerOwner, double delta)
    {
        if (Input.IsActionJustReleased(s_MoveSprint))
        {
            return new MoveState<T>();
        }

        return null;
    }

    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity)
    {
        playerOwner.ApplyMovementInputToVector(ref velocity, playerOwner.m_MobStats.m_SpecialStatTypeToAmountFactor[SpecialStatType.SprintSpeedFactor]);

        // Transition to the idle state if the player is not moving
        if (velocity.Length() == 0)
        {
            return new IdleState<T>();
        }

        // Transition to the fall state if the player falling
        if (!playerOwner.IsOnFloor() && velocity.Y < 0.0f)
        {
            return new FallState<T>();
        }

        return null;
    }

    public void OnExitState(T playerOwner) { }
}
