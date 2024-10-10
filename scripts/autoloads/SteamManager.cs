using Godot;
using GodotSteam;
using System;
using System.IO;

namespace Game.Networking
{
    public partial class SteamManager : Node
    {
        public static SteamManager Instance { get; private set; }

        public static bool s_IsOnline { get; private set; } = false;
        public static bool s_IsOwned { get; private set; } = false;
        public static ulong s_SteamId { get; private set; } = 0;
        public static string s_SteamName { get; private set; } = "";
        public static bool s_IsOnSteamDeck { get; private set; } = false;
        public static SteamLobbyManager s_SteamLobbyManager { get; private set; } = null;

        private static uint AppId; // Set through ReadAppIdFromFile() - (Reads from steam_appid.txt)

        public override void _EnterTree()
        {
            ReadAppIdFromFile();
            OS.SetEnvironment("SteamAppId", AppId.ToString());
            OS.SetEnvironment("SteamGameId", AppId.ToString());
        }

        public override void _Ready()
        {
            if (Instance != null)
            {
                const int ERROR_CODE = 1; // Exit code for errors > 0
                GD.PrintErr("Multiple instances of SteamManager detected!");
                GetTree().Quit(ERROR_CODE);
            }
            Instance = this;
            GD.Print("SteamManager ready to initialize Steam");
        }

        public override void _Process(double delta)
        {
            // ONLY RUN STEAM CALLBACKS IF ONLINE
            if (!GameManager.s_IsOnline) { return; }

            Steam.RunCallbacks();
        }

        public override void _ExitTree()
        {
            // SHUTDOWN STEAM
            Steam.SteamShutdown();
        }

        public static bool InitializeSteam()
        {
            var initResult = Steam.SteamInit();
            if (initResult.Status != SteamInitStatus.SteamworksActive)
            {
                GD.PrintErr("[ERROR] Failed to initialize Steam, shutting down...: " + initResult.Verbal);
                return false;
            }
            GD.Print("Steam initialized successfully: " + initResult.Verbal);

            // GATHER ADDITIONAL STEAM DATA
            s_IsOnSteamDeck = Steam.IsSteamRunningOnSteamDeck();
            s_IsOnline = Steam.LoggedOn();
            s_IsOwned = Steam.IsSubscribed(); // Maybe?: s_IsOwned = Steam.UserHasLicenseForApp();
            s_SteamId = Steam.GetSteamID();
            s_SteamName = Steam.GetPersonaName();

            // CHECK GAME OWNERSHIP
            if (!s_IsOwned)
            {
                GD.PrintErr("Steam User does not own the game");
                return false; // No Multiplayer for U! Filthy Pirate!
            }

            // CREATE STEAM LOBBY OBJECT
            s_SteamLobbyManager = new SteamLobbyManager();
            s_SteamLobbyManager.InitializeLobbyManager();

            return true;
        }

        /// <summary>
        /// Reads the Steam App ID from the "steam_appid.txt" file.
        /// If the file exists and contains a valid unsigned integer, it sets the AppId property.
        /// Logs an error if the file is not found, if the content cannot be parsed, or if an exception occurs.
        /// </summary>
        private void ReadAppIdFromFile()
        {
            try
            {
                string filePath = "steam_appid.txt";
                if (File.Exists(filePath))
                {
                    string fileContent = File.ReadAllText(filePath);
                    if (uint.TryParse(fileContent, out uint appId))
                    {
                        AppId = appId;
                    }
                    else
                    {
                        GD.PrintErr("Failed to parse AppId from steam_appid.txt");
                    }
                }
                else
                {
                    GD.PrintErr("steam_appid.txt file not found");
                }
            }
            catch (Exception ex)
            {
                GD.PrintErr("Error reading steam_appid.txt: " + ex.Message);
            }
        }
    }
}
