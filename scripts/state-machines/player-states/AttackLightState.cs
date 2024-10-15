using Godot;
using static InputActions;

namespace Game.StateMachines;

// TODO: Finish implementing first version of
public partial class AttackLightState<T> : IPlayerState<T> where T : Player
{
    // TODO: Read from the players equipped weapon to determine the attack time, damage, 
    // animation, etc... and have a seperate state for each specific weapon.  

    // How long the attack will last, animations and all
    //private float attackLightTimeSec;

    public IPlayerState<T>? OnEnterState(T playerOwner)
    {
        // do things
        // do more things
        //
        // call the weapons OnAttackLightStateEnter() or somethings
        return new IdleState<T>();
        // BUG: Attacking while jumping causes the player to return to the sprint state instead of the jumping state
        // return null;
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
        return null;
    }

    public IPlayerState<T>? PhysicsProcess(T playerOwner, double delta, ref Vector3 velocity)
    {
        return null;
    }

    public void OnExitState(T playerOwner)
    {
    }
}
