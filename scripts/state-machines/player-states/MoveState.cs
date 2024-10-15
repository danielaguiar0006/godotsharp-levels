using Godot;
using static InputActions;

namespace Game.StateMachines;

public partial class MoveState<T> : IPlayerState<T> where T : Player
{
    public IPlayerState<T>? OnEnterState(T playerOwner)
    {
        // HACK: Perform an immediate physics update to avoid delay in state transition
        // This help alleviate an issue of the player moving at half speed when constantly
        // and immediately transitioning between states in _PhysicsProcess() (This might cause
        // other issues, but it's a trade off for now)
        // NOTE: This has been resolved by forcing player movement every physics tick inside 
        // _PhysicsProcess() in the main Player script
        //
        //player._PhysicsProcess(Engine.GetPhysicsInterpolationFraction());

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

        return null;
    }

    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity)
    {
        playerOwner.ApplyMovementInputToVector(ref velocity);

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

        if (Input.IsActionPressed(s_MoveSprint))
        {
            return new SprintState<T>();
        }

        // TODO: Implement walking state

        return null;
    }

    public void OnExitState(T playerOwner) { }
}
