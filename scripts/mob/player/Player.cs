using Godot;
using Game.ActionTypes;
using Game.StateMachines;

public partial class Player : Mob
{
    // NOTE: Both moveSpeed and moveSpeedFactor affect the speed of most movement related actions
    [ExportCategory("Movement")]
    [Export]
    public DodgeType m_DodgeType { get; private set; } = DodgeType.Dash;

    [ExportCategory("Camera")]
    [Export]
    public float m_MouseSensitivity = 0.1f;
    [Export]
    public Node3D m_CameraPivot { get; private set; }
    [Export]
    public Camera3D m_Camera { get; private set; }
    [Export]
    public RayCast3D m_Raycast { get; private set; }

    [ExportCategory("Misc")]
    [Export]
    public PlayerState m_CurrentPlayerState { get; private set; } = null;

    // Aiming/Camera input
    private float m_YawInput = 0.0f;
    private float m_PitchInput = 0.0f;
    private float m_PitchLowerLimit = 0.0f;
    private float m_PitchUpperLimit = 0.0f;

    public Player()
    {
        m_Name = "Player";
        SetMobType(MobType.Player);
    }

    public override void _Ready()
    {
        // For more accurate mouse input
        Input.UseAccumulatedInput = false;

        // Locks the mouse cursor to the window
        Input.MouseMode = Input.MouseModeEnum.Captured;

        // Set Limits for the camera pitch (up and down rotation)
        m_PitchLowerLimit = Mathf.DegToRad(-89);
        m_PitchUpperLimit = Mathf.DegToRad(89);

        // Make sure the view-port camera is set to the current camera
        m_Camera.Current = true;

        // Set the players initial state here
        TransitionToState(new IdleState());
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // LOCK MOUSE AND HIDE CURSOR
        if (Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            AimCamera(@event);
        }
        else
        {
            if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.Pressed)
            {
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left || mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    Input.MouseMode = Input.MouseModeEnum.Captured;
                }
            }
        }

        // NOTE: newState is null if the state does not change, otherwise it is the new state
        PlayerState newState = m_CurrentPlayerState.HandleInput(this, @event);
        TransitionToState(newState);
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        // UNLOCK MOUSE AND SHOW CURSOR
        // NOTE: The ImGui addon breaks aiming when ui is directly in the middle of the screen
        // Probably because when the mouse is captured, its probably still in the middle of the
        // screen, just not visible. Unlocking the mouse and adjusting the ui transform to be
        // out of the way fixes this issue.
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.Tab)
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
            }
        }

        PlayerState newState = m_CurrentPlayerState.HandleKeyboardInput(this, @event);
        TransitionToState(newState);
    }

    public override void _Process(double delta)
    {
        PlayerState newState = m_CurrentPlayerState.Process(this, delta);
        TransitionToState(newState);

        // TESTING - REMOVE ME:
        ImGuiNET.ImGui.Begin("MainMenu");
        ImGuiNET.ImGui.Text("Hello World!");
        ImGuiNET.ImGui.End();
    }

    public override void _PhysicsProcess(double delta)
    {
        // NOTE: Instead of constantly making calls to this.Velocity cache it for better 
        // performance and work with the new velocity variable instead
        Vector3 velocity = this.Velocity;

        PlayerState newState = m_CurrentPlayerState.PhysicsProcess(this, ref velocity, delta);
        TransitionToState(newState);

        // Apply gravity and movement
        ApplyGravityToVector(ref velocity, delta);
        ApplyMobMovement(ref velocity);
    }

    // Change the player's state
    public void TransitionToState(PlayerState newState)
    {
        if (newState != null)
        {
            if (m_CurrentPlayerState != null) { m_CurrentPlayerState.OnExitState(this); } // Exit current state 
            m_CurrentPlayerState = newState;                                              // Set the new state
            PlayerState nextState = m_CurrentPlayerState.OnEnterState(this);              // Enter the new state

            // If the OnEnterState of the new state returns another state, transition again
            if (nextState != null)
            {
                TransitionToState(nextState);
            }
        }
    }

    // Helper funciton to aim the camera through mouse/controller input
    private void AimCamera(InputEvent @event)
    {
        // Checking if the mouse is active
        if (@event is InputEventMouseMotion mouseInputEvent && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            m_YawInput = -mouseInputEvent.ScreenRelative.X;
            m_PitchInput = -mouseInputEvent.ScreenRelative.Y;
        }
        else
        { // Reset the mouse input if the mouse is not active
          // InputEventMouseMotion.Relative is not reset to 0 when the mouse is not active
            m_YawInput = 0.0f;
            m_PitchInput = 0.0f;
        }

        // Rotate player
        RotateY(Mathf.DegToRad(m_YawInput * m_MouseSensitivity));
        // Rotate camera pivot and clamp the pitch
        m_CameraPivot.RotateX(Mathf.DegToRad(m_PitchInput * m_MouseSensitivity));

        // HACK: This does not work for some reason:
        // m_CameraPivot.Rotation.X = Mathf.Clamp(m_CameraPivot.Rotation.X, m_PitchLowerLimit, m_PitchUpperLimit);

        Vector3 cameraRotation = m_CameraPivot.Rotation;
        cameraRotation.X = Mathf.Clamp(cameraRotation.X, m_PitchLowerLimit, m_PitchUpperLimit);
        m_CameraPivot.Rotation = cameraRotation;
    }
}
