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

    public StateMachine<Player> m_StateMachine { get; private set; }

    // Aiming/Camera input
    private float m_YawInput = 0.0f;
    private float m_PitchInput = 0.0f;
    private float m_PitchLowerLimit = 0.0f;
    private float m_PitchUpperLimit = 0.0f;

    public Player()
    {
        m_Name = "Player";
        SetMobType(MobType.Player);
        m_StateMachine = new StateMachine<Player>(this);
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
        m_StateMachine.ChangeState(new IdleState());
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

        m_StateMachine.HandleInput(@event);
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        m_StateMachine.HandleKeyboardInput(@event);
    }

    public override void _Process(double delta)
    {
        m_StateMachine.Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        // NOTE: Instead of constantly making calls to this.Velocity cache it for better 
        // performance and work with the new velocity variable instead
        Vector3 velocity = this.Velocity;

        m_StateMachine.PhysicsProcess(delta, ref velocity);

        // Apply gravity and movement
        ApplyGravityToVector(ref velocity, delta);
        ApplyMobMovement(ref velocity);
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
