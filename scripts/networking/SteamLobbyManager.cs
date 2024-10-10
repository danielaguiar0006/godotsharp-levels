using Godot;
using GodotSteam;
using System.Diagnostics;

namespace Game.Networking
{
    public partial class SteamLobbyManager : Node
    {
        //public var s_LobbyData { get; private set; } = null;
        public ulong m_LobbyId { get; private set; } = 0;
        //public lobbymemeers[]
        //public LOBBYinviteargs
        public const int MAX_LOBBY_MEMBERS = 4;
        // Misc

        public void InitializeLobbyManager()
        {
            GD.Print("SteamLobbyManager ready");
            // SET UP STEAM EVENTS
            // Steam.JoinRequested += OnLobbyJoinRequested;
            Steam.LobbyCreated += OnLobbyCreated;
            // Steam.LobbyCreated += OnLobbyCreated;
            // Steam.LobbyDataUpdate += OnLobbyDataUpdate;
            // Steam.LobbyInvite += OnLobbyInvite;
            // Steam.LobbyJoined += OnLobbyJoined;
            // Steam.LobbyMatchList += OnLobbyMatchList;
            // Steam.PersonaStateChange += OnPersonaStateChange;

            // CHECK COMMAND LINE ARGUMENTS
            CheckCommandLineArgs();
        }

        public void CreateLobby()
        {
            if (m_LobbyId != 0)
            {
                GD.PrintErr("Lobby already exists");
                return;
            }

            Steam.CreateLobby(Steam.LobbyType.Private, MAX_LOBBY_MEMBERS);
        }

        private void OnLobbyCreated(long connect, ulong lobbyId)
        {
            Debug.Assert(lobbyId != 0, "Invalid lobby ID: " + lobbyId);

            if (connect == 1)
            {
                m_LobbyId = lobbyId;
                GD.Print("Created lobby: " + m_LobbyId);

                // Set lobby data
                Steam.SetLobbyData(m_LobbyId, "name", "test_lobby_name");
            }
        }

        /// <summary>
        /// Checks the command line arguments for specific commands such as joining a player's lobby on launch.
        /// If the first argument is "+connect_lobby", it attempts to parse the second argument as a lobby ID.
        /// If the lobby ID is valid and greater than 0, it prints the lobby ID.
        /// Logs an error if the lobby ID format is invalid.
        /// </summary>
        private void CheckCommandLineArgs()
        {
            string[] args = OS.GetCmdlineArgs();
            if (args.Length > 0)
            {
                if (args[0] == "+connect_lobby")
                {
                    // Parse lobby id
                    if (!ulong.TryParse(args[1], out ulong lobbyId))
                    {
                        GD.PrintErr("Invalid lobby ID Format");
                        return;
                    }

                    // Check if lobby id is valid
                    if (lobbyId > 0)
                    {
                        // TODO: CHANGE TO LOADING SCREEN OR SOMETHING
                        GD.Print("Command line lobby ID: " + args[1]);
                        //TODO: joinLobby(lobbyId);
                    }
                }
            }
        }
    }
}

// NOTE: Additionally, you'll need to add the appropriate scene name to your Steamworks
// launch options on the Steamworks website. You'll want to add the full scene path
// (res://your-scene.tscn) on the Arguments line in your launch option. You can read more
// about that, with details, in this link. Big thanks to Antokolos for answering this issue
// and providing a solid example.
