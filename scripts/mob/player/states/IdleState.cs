using Godot;
using static InputActions;
using Game.StateMachines;


public partial class IdleState : State<Player>
{
    public override State<Player>? OnEnterState(Player player)
    {
        return null;
    }

    public override State<Player>? HandleInput(Player player, InputEvent @event)
    {
        // Checking mouse button events
        if (@event is InputEventMouseButton mouseButtonEvent && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            // Transition to the attack state if the attack button is pressed
            if (mouseButtonEvent.IsActionPressed(s_AttackLight))
            {
                return new AttackLightState();
            }
        }

        if (Input.IsActionJustPressed(s_MoveJump))
        {
            return new JumpState();
        }
        else if (Input.IsActionJustPressed(s_MoveDodge))
        {
            return new DodgeState();
        }

        return null;
    }

    public override State<Player>? HandleKeyboardInput(Player player, InputEvent @event)
    {
        return null;
    }

    public override State<Player>? Process(Player player, double delta)
    {
        if (Input.IsActionPressed(s_MoveForward) || Input.IsActionPressed(s_MoveBackward) || Input.IsActionPressed(s_MoveLeft) || Input.IsActionPressed(s_MoveRight))
        {
            return new MoveState();
        }

        return null;
    }

    public override State<Player>? PhysicsProcess(Player player, double delta, ref Vector3 velocity)
    {
        if (!player.IsOnFloor() && velocity.Y < 0.0f)
        {
            return new FallState();
        }

        return null;
    }

    public override void OnExitState(Player player)
    {
    }
}
