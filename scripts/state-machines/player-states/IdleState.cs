using Godot;
using static InputActions;

namespace Game.StateMachines;

public partial class IdleState<T> : IPlayerState<T> where T : Player
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

        if (Input.IsActionJustPressed(s_MoveJump))
        {
            return new JumpState<T>();
        }
        else if (Input.IsActionJustPressed(s_MoveDodge))
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
        if (Input.IsActionPressed(s_MoveForward) || Input.IsActionPressed(s_MoveBackward) || Input.IsActionPressed(s_MoveLeft) || Input.IsActionPressed(s_MoveRight))
        {
            return new MoveState<T>();
        }

        return null;
    }

    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity)
    {
        if (!playerOwner.IsOnFloor() && velocity.Y < 0.0f)
        {
            return new FallState<T>();
        }

        return null;
    }

    public void OnExitState(T playerOwner)
    {
    }
}
