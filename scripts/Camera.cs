using Godot;

// This script moves the attached camera3d to the target node position and rotation smoothly.
// Originally created because objects tied to parents who update their position in physics_process()
// cause severe jittering.
// NOTE: Currently this script is identical to InterpolateToTargetTransform.cs, but it's here because
// in order to add this script to the camera node, it needs to inheret from a Camera3D node, but this
// gives a chance to add more camera specific functionality in the future.
public partial class Camera : Camera3D
{
    [Export]
    protected Node3D m_TargetNode;
    [Export]
    protected float m_LerpSpeed = 75.0f; // Adjust this value to your liking


    public override void _Process(double delta)
    {
        // Get the target Node3D position
        Transform3D targetNodeTransform = m_TargetNode.GlobalTransform;

        // Interpolate position
        Vector3 newPosition = this.GlobalTransform.Origin.Lerp(targetNodeTransform.Origin, (float)delta * m_LerpSpeed);

        // Interpolate rotation
        Quaternion currentRotation = this.GlobalTransform.Basis.GetRotationQuaternion();
        Quaternion targetRotation = targetNodeTransform.Basis.GetRotationQuaternion();
        Quaternion newRotation = currentRotation.Slerp(targetRotation, (float)delta * m_LerpSpeed);

        // Convert quaternion to basis
        Basis newBasis = new Basis(newRotation);

        // Apply the new transform
        this.GlobalTransform = new Transform3D(newBasis, newPosition);
    }
}
