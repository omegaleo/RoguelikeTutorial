using Godot;
using System;

public partial class CameraAnchor : Node3D
{
	[Export] public Node3D camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InputManager.Instance.OnCameraMovement += OnCameraMove;
	}

	private void OnCameraMove(Vector3 cameraMovement)
	{
		this.RotateY(-cameraMovement.X);
		camera.RotateX(-cameraMovement.Y);
	}
}
