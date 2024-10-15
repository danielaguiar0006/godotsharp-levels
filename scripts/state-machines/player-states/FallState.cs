using Godot;
using static InputActions;
using Game.StatsManager;

namespace Game.StateMachines;

public partial class FallState<T> : IPlayerState<T> where T : Player
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
        return null;
    }

    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity)
    {
        float fallSpeedMovementFactor = Input.IsActionPressed(s_MoveSprint) ? playerOwner.m_MobStats.m_SpecialStatTypeToAmountFactor[SpecialStatType.SprintSpeedFactor] : 1.0f;
        playerOwner.ApplyMovementInputToVector(ref velocity, fallSpeedMovementFactor);

        // Transition to the idle state if the player is not moving
        if (velocity.Length() == 0)
        {
            return new IdleState<T>();
        }

        // Transition to the move state if the player is on the floor and moving
        if (playerOwner.IsOnFloor() && (velocity.X != 0 || velocity.Z != 0))
        {
            return new MoveState<T>();
        }

        return null;
    }

    public void OnExitState(T playerOwner) { }
}
