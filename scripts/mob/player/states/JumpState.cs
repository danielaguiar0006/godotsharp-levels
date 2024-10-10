using Godot;
using static InputActions;
using Game.StatsManager;
using Game.StateMachines;


public partial class JumpState : State<Player>
{
    float appliedJumpVelocityTimeSec;  // Time in seconds that the jump velocity will be applied to the player
    float jumpVelocity;                // The velocity that will be applied to the player when jumping
    bool isJumping;                    // If the player is currently jumping - Is the player in this state?


    public override State<Player>? OnEnterState(Player player)
    {
        appliedJumpVelocityTimeSec = 0.1f;
        jumpVelocity = player.m_JumpVelocity;
        isJumping = true;

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

        if (Input.IsActionJustPressed(s_MoveDodge))
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
        appliedJumpVelocityTimeSec -= (float)delta;

        return null;
    }

    public override State<Player>? PhysicsProcess(Player player, double delta, ref Vector3 velocity)
    {
        if (isJumping)
        {
            float jumpSpeedMovementFactor = Input.IsActionPressed(s_MoveSprint) ? player.m_MobStats.m_SpecialStatTypeToAmountFactor[SpecialStatType.SprintSpeedFactor] : 1.0f;
            player.ApplyMovementInputToVector(ref velocity, jumpSpeedMovementFactor);

            if (appliedJumpVelocityTimeSec > 0.0f)
            {
                // Apply the jump velocity to the player's Y velocity
                velocity.Y = jumpVelocity;
            }
        }
        else
        {
            GD.Print("ERROR: Stuck in the JumpState!, transitioning to FallState.");
            return new FallState();
        }

        // Transition to the idle state if the player is not moving
        if (velocity.Length() == 0)
        {
            return new IdleState();
        }

        // Transition to the fall state if the player falling
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

