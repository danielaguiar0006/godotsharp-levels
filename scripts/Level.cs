using Godot;

public partial class Level : Node3D
{
	// TODO: Read from Global.s_RandomNumberGenerator to generate a random number for the RNG

	// TODO: THE "LEVEL" CLASS SHOULD HANDLE THIS - Level should deal with managing the current level, including spawning mobs, spawning items, etc...
	// Level should also most importantly be responsible for generating the level, including generating the map, generating the spawn points, etc...
	// -----------------------------------------------------------------------------------------------------------------------------------------------
	// initialize a list of mob references
	// public List<Mob> m_ActiveMobs { get; private set; } = new List<Mob>();
	// public Dictionary<string, Player> m_ActivePlayers { get; private set; } = new Dictionary<string, Player>();
	// public List<Monster> m_ActiveMonsters { get; private set; } = new List<Monster>();
	// public List<NPC> m_ActiveNPCs { get; private set; } = new List<NPC>();
	// public List<Boss> m_ActiveBosses { get; private set; } = new List<Boss>();
	// TODO FIX THIS (this was previously in GameManager):
	/*
	
	public Mob SpawnMob(Mob.MobType mobType, Vector3 position, string playerId = "0")
	{
		switch (mobType)
		{
			case Mob.MobType.Player:
				Player playerInstance = m_PlayerScene.Instantiate<Player>();
				m_ActivePlayers.Add(playerId, playerInstance);
				m_Level.AddChild(playerInstance);
				playerInstance.Position = position;
				GD.Print($"Spawning Player at {position}");
				return playerInstance;
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
				GD.PrintErr("[ERROR] Unable to despawn player, use DespawnPlayer() instead");
				break;
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

		// remove the mob from the level/scene
		m_Level.RemoveChild(mob);
	}

	public void DespawnPlayer(string playerId)
	{
		if (m_ActivePlayers.TryGetValue(playerId, out Player player))
		{
			m_ActivePlayers.Remove(playerId);
			m_Level.RemoveChild(player);
		}
		else
		{
			GD.PrintErr("[ERROR] Unable to despawn player, player not found");
		}
	}
*/
}
