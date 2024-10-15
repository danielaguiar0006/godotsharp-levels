using Godot;
using static InputActions;
using Game.StatsManager;

namespace Game.StateMachines;

public partial class JumpState<T> : IPlayerState<T> where T : Player
{
    float appliedJumpVelocityTimeSec;  // Time in seconds that the jump velocity will be applied to the player
    float jumpVelocity;                // The velocity that will be applied to the player when jumping
    bool isJumping;                    // If the player is currently jumping - Is the player in this state?


    public IPlayerState<T>? OnEnterState(T playerOwner)
    {
        appliedJumpVelocityTimeSec = 0.1f;
        jumpVelocity = playerOwner.m_JumpVelocity;
        isJumping = true;

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
        appliedJumpVelocityTimeSec -= (float)delta;

        return null;
    }

    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity)
    {
        if (isJumping)
        {
            float jumpSpeedMovementFactor = Input.IsActionPressed(s_MoveSprint) ? playerOwner.m_MobStats.m_SpecialStatTypeToAmountFactor[SpecialStatType.SprintSpeedFactor] : 1.0f;
            playerOwner.ApplyMovementInputToVector(ref velocity, jumpSpeedMovementFactor);

            if (appliedJumpVelocityTimeSec > 0.0f)
            {
                // Apply the jump velocity to the player's Y velocity
                velocity.Y = jumpVelocity;
            }
        }
        else
        {
            GD.Print("ERROR: Stuck in the JumpState!, transitioning to FallState.");
            return new FallState<T>();
        }

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

