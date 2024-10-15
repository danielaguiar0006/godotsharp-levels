using Godot;
using static InputActions;
using Game.ActionTypes;
using Game.StatsManager;

namespace Game.StateMachines;

public partial class DodgeState<T> : IPlayerState<T> where T : Player
{
    // How long the dodge will last, animations and all
    private float currentDodgeTimeSec;
    private float totalDodgeTimeSec;

    // Dodge speeds
    private float rollSpeedFactor = 1.75f;
    private float dashSpeedFactor = 10.0f;

    // The target camera transform, used to determine the direction the player is looking
    private Transform3D targetCameraTransform;


    public IPlayerState<T>? OnEnterState(T playerOwner)
    {
        switch (playerOwner.m_DodgeType)
        {
            case DodgeType.Roll:
                // NOTE: This is done here and not in the physics process because physics
                // process is called once every physics tick, not asap, which causes a delay bug
                if (!playerOwner.IsOnFloor())
                {
                    GD.Print("Rolling in the air is not allowed");
                    return new MoveState<T>();
                }

                currentDodgeTimeSec = 0.5f;
                totalDodgeTimeSec = currentDodgeTimeSec;
                rollSpeedFactor *= playerOwner.m_MobStats.m_SpecialStatTypeToAmountFactor[SpecialStatType.DodgeSpeedFactor];

                // Play the roll animation
                break;
            case DodgeType.Dash:
                currentDodgeTimeSec = 0.33f;
                totalDodgeTimeSec = currentDodgeTimeSec;
                dashSpeedFactor *= playerOwner.m_MobStats.m_SpecialStatTypeToAmountFactor[SpecialStatType.DodgeSpeedFactor];

                // Play the dash animation
                break;
        }

        targetCameraTransform = playerOwner.GetNode<Node3D>("CameraPivot/TargetCamera").GlobalTransform;

        return null;
    }

    public IPlayerState<T>? HandleInput(T playerOwner, InputEvent @event)
    {
        return null;
    }

    public IPlayerState<T>? HandleKeyboardInput(T playerOwner, InputEvent @event)
    {
        return null;
    }

    public IPlayerState<T>? Process(T playerOwner, double delta)
    {
        currentDodgeTimeSec -= (float)delta;

        return null;
    }

    // Dodge the player - Affected by player's dodge type, speed factor, regular movement speed, dodge time, and movement direction
    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity)
    {
        if (currentDodgeTimeSec < 0.0f)
        {
            return new MoveState<T>();
        }


        Vector3 wishDirection;
        if (playerOwner.m_MovementDirection != Vector3.Zero) // if player is moving, dodge in the direction the player is moving
        {
            wishDirection = playerOwner.m_MovementDirection; // NOTE: m_MovementDirection is already normalized
        }
        else  // else dodge in the direction the player is looking
        {
            wishDirection = -targetCameraTransform.Basis.Z.Normalized(); // Forward direction of the camera
        }

        switch (playerOwner.m_DodgeType)
        {
            case DodgeType.Roll:
                //rollSpeedFactor *= player.m_Stats.GetSpecialStatAmountFactors()[SpecialStatType.MovementSpeedFactor];
                playerOwner.ApplyMovementDirectionToVector(ref velocity, wishDirection, rollSpeedFactor);
                // NOTE: Vertical velocity is not disabled to enable gravity, letting the player roll off ledges
                break;
            case DodgeType.Dash:
                // ------ Calculate the easing out ------
                // Ease into sprint speed factor if sprint button is pressed or into the regular movement speed factor (usually 1.0f)
                float easeOutTo = Input.IsActionPressed(s_MoveSprint) ? playerOwner.m_MobStats.m_SpecialStatTypeToAmountFactor[SpecialStatType.SprintSpeedFactor] : 1.0f;
                float dashProgress = 1.0f - (currentDodgeTimeSec / totalDodgeTimeSec);
                float easedDashSpeedFactor = Mathf.Lerp(dashSpeedFactor, easeOutTo, 1.0f - Mathf.Pow(1.0f - dashProgress, 2));
                // --------------------------------------

                playerOwner.ApplyMovementDirectionToVector(ref velocity, wishDirection, easedDashSpeedFactor);
                velocity.Y = 0; // Disables vertical movement (including gravity) - IDK: Maybe another dodge type that allows vertical movement
                break;
        }

        return null;
    }

    public void OnExitState(T playerOwner) { }
}
