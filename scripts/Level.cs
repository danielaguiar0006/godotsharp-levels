// Level should deal with managing the current level, including spawning mobs, spawning items, etc...
// Level should also most importantly be responsible for generating the level, including 
// generating the map, generating the spawn points, etc...
// TODO: Read from Global.s_RandomNumberGenerator to generate a random number for the RNG

using Godot;
using System.Collections.Generic;

public partial class Level : Node3D
{
    //public List<Mob> m_ActiveMobs { get; private set; } = new List<Mob>();
    public List<Monster> m_ActiveMonsters { get; private set; } = new List<Monster>();
    public List<NPC> m_ActiveNPCs { get; private set; } = new List<NPC>();
    public List<Boss> m_ActiveBosses { get; private set; } = new List<Boss>();
    // NOTE: There is no list of ActivePlayers instead, loop through GameManager.s_Players and check if the player is alive

    public void GenerateLevel()
    {
        uint randomNumber = Global.s_RandomNumberGenerator.Randi();
        GD.Print("Random Number: " + randomNumber);

        // TODO:
        // Generate the level
        // Generate the spawn points
        // Spawn mobs
        // Spawn items
    }

    public Mob SpawnMob(Mob.MobType mobType, Vector3 position = default(Vector3))
    {
        switch (mobType)
        {
            case Mob.MobType.Player:
                GD.PrintErr("[ERROR] Unable to spawn player, use SpawnPlayer() inside GameManager instead");
                return null;
            default:
                GD.PrintErr("Invalid mob type");
                return null;

                // TODO: implement spawn logic for other mob types
                // case Mob.MobType.Monster:
                //     // TODO: spawn a monster
                //     GD.Print("Spawning Monster");
                //     break;
                // case Mob.MobType.NPC:
                //     // TODO: spawn a NPC
                //     GD.Print("Spawning NPC");
                //     break;
                // case Mob.MobType.Boss:
                //     // TODO: spawn a Boss
                //     GD.Print("Spawning Boss");
                //     break;
        }
    }

    public void DespawnMob(Mob mob)
    {
        if (mob == null)
        {
            GD.PrintErr("Unable to Despawn Mob: Mob is null");
            return;
        }

        switch (mob.m_MobType)
        {
            case Mob.MobType.Player:
                GD.PrintErr("[ERROR] Unable to despawn player, use DespawnPlayer() inside GameManager instead");
                return;
            case Mob.MobType.Monster:
                m_ActiveMonsters.Remove((Monster)mob);
                break;
            case Mob.MobType.NPC:
                m_ActiveNPCs.Remove((NPC)mob);
                break;
            case Mob.MobType.Boss:
                m_ActiveBosses.Remove((Boss)mob);
                break;
        }

        // remove the mob from the level/scene & free it
        this.RemoveChild(mob);
        mob.QueueFree();
    }
}
