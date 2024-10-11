// Main menu ui for the game

using Godot;
using Game.StateMachines;
using Game.Networking;

public partial class MainMenuUI : Control
{
    Button m_StartButton;
    Button m_HostButton;
    Button m_JoinButton;

    public override void _Ready()
    {
        m_StartButton = GetNode<Button>("VBoxContainer/StartButton");
        m_HostButton = GetNode<Button>("VBoxContainer/HostButton");
        m_JoinButton = GetNode<Button>("VBoxContainer/JoinButton");
    }

    public override void _Process(double delta)
    {
        // IF STEAM IS ONLINE, SHOW HOST AND JOIN BUTTONS
        if (GameManager.s_IsOnline)
        {
            m_StartButton.Visible = false;
            m_HostButton.Visible = true;
            m_JoinButton.Visible = true;
        }
        else // OTHERWISE, ONLY SHOW START BUTTON (ASSUMED SINGLE PLAYER)
        {
            m_StartButton.Visible = true;
            m_HostButton.Visible = false;
            m_JoinButton.Visible = false;
        }
    }

    public void _On_Start_Button_Pressed()
    {
        GD.Print("Start Pressed");
        this.Visible = false;
        GameManager.s_StateMachine.ChangeState(new SinglePlayerGameState());
        GameManager.StartGame();
    }

    public void _On_Host_Button_Pressed()
    {
        GD.Print("Host Pressed");
        // TODO: Open Host Menu
        SteamManager.s_SteamLobbyManager.CreateLobby();
    }

    public void _On_Join_Button_Pressed()
    {
        GD.Print("Join Pressed");
        // TODO: Open Join Menu
    }

    public void _On_Options_Button_Pressed()
    {
        GD.Print("Options Pressed");
    }

    public void _On_Quit_Button_Pressed()
    {
        GD.Print("Quit Pressed");
        GameManager.QuitApplication();
    }
}
