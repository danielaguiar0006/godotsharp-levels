using Godot;
using static InputActions;
using Game.StateMachines;


// TODO: Finish implementing first version of
public partial class AttackLightState : State<Player>
{
    // TODO: Read from the players equipped weapon to determine the attack time, damage, 
    // animation, etc... and have a seperate state for each specific weapon.  

    // How long the attack will last, animations and all
    //private float attackLightTimeSec;

    public override State<Player>? OnEnterState(Player player)
    {
        // do things
        // do more things
        //
        // call the weapons OnAttackLightStateEnter() or somethings
        return new IdleState();
        // BUG: Attacking while jumping causes the player to return to the sprint state instead of the jumping state
        return null;
    }

    public override State<Player>? HandleInput(Player player, InputEvent @event)
    {
        return null;
    }

    public override State<Player>? HandleKeyboardInput(Player player, InputEvent @event)
    {
        return null;
    }

    public override State<Player>? PhysicsProcess(Player player, double delta, ref Vector3 velocity)
    {
        return null;
    }

    public override void OnExitState(Player player)
    {
    }
}
